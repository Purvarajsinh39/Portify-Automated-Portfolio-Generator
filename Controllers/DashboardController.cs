using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using Portify.Models;
using System.Linq;
using Hangfire;

namespace Portify.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard/Explore (Explore Templates)
        public ActionResult Explore()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                return RedirectToAction("Admin");
            }

            PortifyDbContext db = new PortifyDbContext();
            var templates = db.GetAllTemplates().Where(t => t.IsActive).ToList();

            return View("~/Views/User/Explore.cshtml", templates);
        }

        // GET: Dashboard/UploadTemplate
        public ActionResult UploadTemplate()
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }

            return View("~/Views/Admin/UploadTemplate.cshtml", new Template());
        }

        [HttpPost]
        public ActionResult UploadTemplate(Template template, HttpPostedFileBase TemplateFile)
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }

            if (TemplateFile != null && TemplateFile.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(TemplateFile.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();
                    string templateDir = Server.MapPath("~/Templates/" + template.Name);
                    
                    if (!Directory.Exists(templateDir))
                    {
                        Directory.CreateDirectory(templateDir);
                    }

                    if (extension == ".zip")
                    {
                        using (var archive = new ZipArchive(TemplateFile.InputStream))
                        {
                            archive.ExtractToDirectory(templateDir);
                        }
                        // Assume index.html inside
                        template.FilePath = "~/Templates/" + template.Name + "/index.html"; 
                    }
                    else if (extension == ".html")
                    {
                        string savePath = Path.Combine(templateDir, fileName);
                        TemplateFile.SaveAs(savePath);
                        template.FilePath = "~/Templates/" + template.Name + "/" + fileName;
                    }
                    
                    template.IsActive = true;

                    PortifyDbContext db = new PortifyDbContext();
                    db.AddTemplate(template);

                    // Trigger notifications for users
                    TriggerTemplateNotifications(template);

                    TempData["SuccessMessage"] = "Template uploaded successfully!";
                    return RedirectToAction("ManageTemplates");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error uploading template: " + ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please select a valid file.";
            }

            return View("~/Views/Admin/UploadTemplate.cshtml", template);
        }

        // GET: Dashboard/ManageTemplates
        public ActionResult ManageTemplates()
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }

            PortifyDbContext db = new PortifyDbContext();
            var templates = db.GetAllTemplates();

            return View("~/Views/Admin/ManageTemplates.cshtml", templates);
        }

        [HttpPost]
        public ActionResult DeleteTemplate(int templateId)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                var template = db.GetTemplateById(templateId);
                if (template != null)
                {
                    // Try to delete the template folder from disk
                    try
                    {
                        string templateDir = Server.MapPath("~/Templates/" + template.Name);
                        if (Directory.Exists(templateDir))
                        {
                            Directory.Delete(templateDir, true);
                        }
                    }
                    catch { /* Ignore file system errors */ }

                    db.DeleteTemplate(templateId);
                    TempData["SuccessMessage"] = "Template deleted successfully.";
                }
            }
            return RedirectToAction("ManageTemplates");
        }

        [HttpPost]
        public ActionResult ToggleTemplateStatus(int templateId)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                var template = db.GetTemplateById(templateId);
                if (template != null)
                {
                    template.IsActive = !template.IsActive;
                    db.UpdateTemplate(template);
                }
            }
            return RedirectToAction("ManageTemplates");
        }

        // GET: Dashboard/Index (User Dashboard)
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                return RedirectToAction("Admin");
            }

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            var portfolios = db.GetPortfoliosByUserId(userId);

            // Build a dictionary of templateId -> templateName for display
            var templates = db.GetAllTemplates();
            var templateNames = new System.Collections.Generic.Dictionary<int, string>();
            foreach (var t in templates)
            {
                templateNames[t.Id] = t.Name;
            }
            ViewBag.TemplateNames = templateNames;

            return View("~/Views/User/UserDashboard.cshtml", portfolios);
        }

        [HttpPost]
        public ActionResult DeletePortfolio(int portfolioId)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            var portfolio = db.GetPortfolioById(portfolioId);

            // Only allow deletion if the portfolio belongs to this user
            if (portfolio != null && portfolio.UserId == userId)
            {
                db.DeletePortfolio(portfolioId);
            }

            return RedirectToAction("Index");
        }

        // GET: Dashboard/MyProfile
        public ActionResult MyProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                return RedirectToAction("Admin"); // Prevent Admin from accessing User profile
            }

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            var user = db.GetUserById(userId);

            return View("~/Views/User/MyProfile.cshtml", user);
        }

        [HttpPost]
        public ActionResult UpdateProfile(string FullName, string Email, string CurrentPassword, string NewPassword)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                return RedirectToAction("Admin"); // Prevent Admin from executing User action
            }

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            var user = db.GetUserById(userId);

            // Verify Current Password
            if (user.PasswordHash != CurrentPassword)
            {
                ViewBag.Error = "Incorrect current password.";
                return View("~/Views/User/MyProfile.cshtml", user);
            }

            // Update Password if NewPassword is provided
            string passwordToSave = user.PasswordHash;
            if (!string.IsNullOrEmpty(NewPassword))
            {
                passwordToSave = NewPassword;
            }

            // Update User
            bool receiveNotifications = Request.Form["ReceiveNotifications"] == "on";
            db.UpdateUserProfile(userId, FullName, passwordToSave, receiveNotifications);
            
            // Update Session Name if changed
            Session["UserName"] = FullName;

            ViewBag.Message = "Profile updated successfully.";
            user.FullName = FullName; // Update model for view
            user.ReceiveNotifications = receiveNotifications;
            return View("~/Views/User/MyProfile.cshtml", user);
        }

        // GET: Dashboard/Admin (Admin Dashboard)
        public ActionResult Admin()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                return RedirectToAction("Index");
            }

            PortifyDbContext db = new PortifyDbContext();
            var model = db.GetAdminDashboardStats();

            return View("~/Views/Admin/AdminDashboard.cshtml", model);
        }

        // GET: Dashboard/UserManagement
        public ActionResult UserManagement()
        {
            if (Session["UserId"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }

            PortifyDbContext db = new PortifyDbContext();
            var users = db.GetAllUsers().Where(u => u.Role != "Admin").ToList();

            return View("~/Views/Admin/UserManagement.cshtml", users);
        }

        [HttpPost]
        public ActionResult BlockUser(int userId, string reason)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                var user = db.GetUserById(userId);
                if (user != null)
                {
                    db.UpdateUserStatus(userId, true, reason);
                    
                    try
                    {
                        BackgroundJob.Enqueue<EmailService>(x => x.SendBlockNotification(user.Email, user.FullName, reason));
                        TempData["SuccessMessage"] = $"User {user.FullName} has been blocked. Notification enqueued.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "User blocked, but failed to enqueue notification: " + ex.Message;
                    }
                }
            }
            return RedirectToAction("UserManagement");
        }

        [HttpPost]
        public ActionResult UnblockUser(int userId)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                var user = db.GetUserById(userId);
                if (user != null)
                {
                    db.UpdateUserStatus(userId, false, null); // Clear reason on unblock
                    
                    try
                    {
                        BackgroundJob.Enqueue<EmailService>(x => x.SendUnblockNotification(user.Email, user.FullName));
                        TempData["SuccessMessage"] = $"User {user.FullName} has been unblocked. Notification enqueued.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "User unblocked, but failed to enqueue notification: " + ex.Message;
                    }
                }
            }
            return RedirectToAction("UserManagement");
        }

        // GET: Dashboard/AdminFeedback
        public ActionResult AdminFeedback()
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }

            PortifyDbContext db = new PortifyDbContext();
            var feedbackList = db.GetAllFeedback();

            return View("~/Views/Admin/AdminFeedback.cshtml", feedbackList);
        }

        // --- Notification Actions ---

        public ActionResult Notifications()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            
            // Mark all as read when viewing the page
            db.MarkAllNotificationsAsRead(userId);
            
            var notifications = db.GetNotificationsByUserId(userId);
            
            return View("~/Views/User/Notifications.cshtml", notifications);
        }

        [HttpPost]
        public JsonResult GetUnreadCount()
        {
            if (Session["UserId"] == null) return Json(0);
            
            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            int count = db.GetUnreadNotificationCount(userId);
            
            return Json(count);
        }

        private void TriggerTemplateNotifications(Template template)
        {
            try
            {
                PortifyDbContext db = new PortifyDbContext();
                var users = db.GetAllUsers().Where(u => u.Role != "Admin").ToList();
                
                foreach (var user in users)
                {
                    // 1. Add In-App Notification
                    db.AddNotification(new Notification
                    {
                        UserId = user.Id,
                        Message = $"A new template '{template.Name}' is now available! Explore it to give your portfolio a fresh look.",
                        IsRead = false,
                        CreatedAt = DateTime.Now
                    });

                    // 2. Enqueue Email if preferred
                    if (user.ReceiveNotifications)
                    {
                        BackgroundJob.Enqueue<EmailService>(x => x.SendTemplateNotification(user.Email, user.FullName, template.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle notification failure (non-critical to upload)
            }
        }
    }
}
