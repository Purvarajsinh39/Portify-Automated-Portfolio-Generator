using Hangfire.Dashboard;
using System.Web;

namespace Portify.Models
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow all local requests
            if (HttpContext.Current.Request.IsLocal)
            {
                return true;
            }

            // For remote requests, check if the user is logged in as an Admin
            // We use the ASP.NET Session to check for the UserRole
            var session = HttpContext.Current.Session;
            
            if (session != null && session["UserRole"] != null && session["UserRole"].ToString() == "Admin")
            {
                return true;
            }

            // Denial of access
            return false;
        }
    }
}
