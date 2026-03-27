using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using Portify.Models;
using System.Linq;

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
            db.UpdateUserProfile(userId, FullName, passwordToSave);
            
            // Update Session Name if changed
            Session["UserName"] = FullName;

            ViewBag.Message = "Profile updated successfully.";
            user.FullName = FullName; // Update model for view
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

            return View("~/Views/Admin/AdminDashboard.cshtml");
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
        public ActionResult BlockUser(int userId)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                db.UpdateUserStatus(userId, true);
            }
            return RedirectToAction("UserManagement");
        }

        [HttpPost]
        public ActionResult UnblockUser(int userId)
        {
            if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
            {
                PortifyDbContext db = new PortifyDbContext();
                db.UpdateUserStatus(userId, false);
            }
            return RedirectToAction("UserManagement");
        }
    }
}
