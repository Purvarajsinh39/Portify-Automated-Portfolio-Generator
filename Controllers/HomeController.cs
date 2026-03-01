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

            var newUser = new Models.User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = Password, // Plain text
                Role = "User",
                IsBlocked = false,
                CreatedAt = DateTime.Now
            };

            db.AddUser(newUser);

            ViewBag.Message = "Registration successful! Please login.";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}