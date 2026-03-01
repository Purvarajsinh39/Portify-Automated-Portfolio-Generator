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

            string templatesPath = Server.MapPath("~/Templates");
            var templates = new System.Collections.Generic.List<string>();

            if (System.IO.Directory.Exists(templatesPath))
            {
                string[] files = System.IO.Directory.GetFiles(templatesPath, "*.html");
                foreach (string file in files)
                {
                    templates.Add(System.IO.Path.GetFileName(file));
                }
            }

            return View("~/Views/User/Explore.cshtml", templates);
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

            return View("~/Views/User/UserDashboard.cshtml");
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
