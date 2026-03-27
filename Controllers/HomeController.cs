using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portify.Models;

namespace Portify.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            PortifyDbContext db = new PortifyDbContext();
            var user = db.GetUserByEmail(Email);

            if (user != null && user.PasswordHash == Password) // Plain text check
            {
                if (user.IsBlocked)
                {
                    ViewBag.Error = "You are blocked from admin.";
                    return View();
                }

                // Set Session
                Session["UserId"] = user.Id;
                Session["UserRole"] = user.Role;
                Session["UserName"] = user.FullName;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Email or Password.";
            return View();
        }

        [HttpPost]
        public ActionResult Register(string FullName, string Email, string Password, string ConfirmPassword)
        {
            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("Login");
            }

            PortifyDbContext db = new PortifyDbContext();
            var existingUser = db.GetUserByEmail(Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already exists.";
                return View("Login");
            }

            // Save to PendingRegistrations
            db.AddPendingRegistration(FullName, Email, Password);

            // Generate OTP
            string otpCode = new Random().Next(100000, 999999).ToString();
            string purpose = "Registration";
            db.SaveOtp(Email, otpCode, purpose);

            // Send Email via Hangfire
            Hangfire.BackgroundJob.Enqueue(() => EmailService.SendOtpEmailBackgroundJob(Email, otpCode, purpose));

            return RedirectToAction("VerifyOtp", new { email = Email, purpose = purpose });
        }

        // --- OTP / Forgot Password logic ---

        [HttpGet]
        public ActionResult VerifyOtp(string email, string purpose)
        {
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");
            
            ViewBag.Email = email;
            ViewBag.Purpose = purpose;
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOtp(string email, string code, string purpose)
        {
            PortifyDbContext db = new PortifyDbContext();
            
            bool isValid = db.VerifyOtp(email, code, purpose);
            if (isValid)
            {
                if (purpose == "Registration")
                {
                    var pending = db.GetLatestPendingRegistration(email);
                    if (pending != null)
                    {
                        var newUser = new Models.User
                        {
                            FullName = pending.FullName,
                            Email = pending.Email,
                            PasswordHash = pending.PasswordHash, // Plain text as previously done
                            Role = "User",
                            IsBlocked = false,
                            CreatedAt = DateTime.Now
                        };
                        db.AddUser(newUser);

                        // Optionally delete pending registration or flag them
                        db.DeletePendingRegistration(pending.Id);

                        ViewBag.Message = "Registration successful! Please login.";
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Registration session expired. Please register again.";
                        return View("Login");
                    }
                }
                else if (purpose == "ForgotPassword")
                {
                    // Allow the user to reset the password securely via session state
                    Session["ResetPasswordEmail"] = email;
                    return RedirectToAction("ResetPassword");
                }
            }

            ViewBag.Email = email;
            ViewBag.Purpose = purpose;
            ViewBag.Error = "Invalid or expired OTP code.";
            return View();
        }

        [HttpPost]
        public ActionResult ResendOtp(string email, string purpose)
        {
            PortifyDbContext db = new PortifyDbContext();
            string otpCode = new Random().Next(100000, 999999).ToString();
            
            db.SaveOtp(email, otpCode, purpose);
            Hangfire.BackgroundJob.Enqueue(() => EmailService.SendOtpEmailBackgroundJob(email, otpCode, purpose));

            return RedirectToAction("VerifyOtp", new { email = email, purpose = purpose });
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            PortifyDbContext db = new PortifyDbContext();
            var existingUser = db.GetUserByEmail(Email);

            if (existingUser != null)
            {
                string otpCode = new Random().Next(100000, 999999).ToString();
                string purpose = "ForgotPassword";
                
                db.SaveOtp(Email, otpCode, purpose);
                Hangfire.BackgroundJob.Enqueue(() => EmailService.SendOtpEmailBackgroundJob(Email, otpCode, purpose));
                
                return RedirectToAction("VerifyOtp", new { email = Email, purpose = purpose });
            }

            // We don't want to expose if a user exists or not for security reasons ideally,
            // but for a simple project we can show message or just redirect to same page.
            ViewBag.Error = "If that email exists, an OTP has been sent.";
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            if (Session["ResetPasswordEmail"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string Password, string ConfirmPassword)
        {
            if (Session["ResetPasswordEmail"] == null) return RedirectToAction("Login");
            
            string email = Session["ResetPasswordEmail"].ToString();

            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            PortifyDbContext db = new PortifyDbContext();
            var user = db.GetUserByEmail(email);

            if (user != null)
            {
                // Note: user requires updating, Add method UpdateUserProfile in PortifyDbContext if not present
                // Luckily it exists UpdateUserProfile(userId, fullName, passwordHash )
                db.UpdateUserProfile(user.Id, user.FullName, Password);
                
                Session.Remove("ResetPasswordEmail");
                
                ViewBag.Message = "Password reset successfully. Please login.";
                return View("Login");
            }
            
            ViewBag.Error = "User not found.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}