using System;
using System.Linq;
using System.Web.Mvc;
using Portify.Models;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace Portify.Controllers
{
    public class PortfolioController : Controller
    {
        private bool GuardUser()
        {
            return Session["UserId"] != null;
        }

        // GET: Portfolio/Editor?templateId=5
        public ActionResult Editor(int templateId)
        {
            if (!GuardUser()) return RedirectToAction("Login", "Home");

            PortifyDbContext db = new PortifyDbContext();
            var template = db.GetTemplateById(templateId);
            if (template == null || !template.IsActive)
            {
                return RedirectToAction("Explore", "Dashboard");
            }

            ViewBag.TemplateId = template.Id;
            ViewBag.TemplateName = template.Name;

            return View();
        }

        // POST: Portfolio/Save
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(int templateId, string portfolioTitle, string jsonData)
        {
            if (!GuardUser()) return RedirectToAction("Login", "Home");

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();

            // Parse the JSON from the editor
            JavaScriptSerializer js = new JavaScriptSerializer();
            PortfolioData data = new PortfolioData();
            try
            {
                data = js.Deserialize<PortfolioData>(jsonData);
            }
            catch { /* fallback to empty */ }

            // 1. Insert into Portfolios table and get the new Id
            int portfolioId = db.CreatePortfolio(userId, templateId, portfolioTitle, data.AboutMe);

            // 2. Insert PortfolioPersonalInfo
            db.AddPortfolioPersonalInfo(new PortfolioPersonalInfo
            {
                PortfolioId = portfolioId,
                FullName = data.FullName,
                Profession = data.Profession,
                Email = data.Email,
                Phone = data.Phone,
                Location = data.Location
            });

            // 3. Insert Skills
            if (data.Skills != null)
            {
                foreach (var skill in data.Skills)
                {
                    db.AddSkill(new Skill
                    {
                        PortfolioId = portfolioId,
                        SkillName = skill.SkillName,
                        SkillLevel = skill.SkillLevel
                    });
                }
            }

            // 4. Insert Projects
            if (data.Projects != null)
            {
                foreach (var proj in data.Projects)
                {
                    db.AddProject(new Project
                    {
                        PortfolioId = portfolioId,
                        ProjectTitle = proj.ProjectTitle,
                        Description = proj.Description,
                        TechStack = proj.TechStack,
                        GitHubLink = proj.GitHubLink,
                        LiveLink = proj.LiveLink
                    });
                }
            }

            // 5. Insert Experiences
            if (data.Experience != null)
            {
                foreach (var exp in data.Experience)
                {
                    db.AddExperience(new Experience
                    {
                        PortfolioId = portfolioId,
                        CompanyName = exp.CompanyName,
                        Role = exp.Role,
                        StartDate = exp.StartDate,
                        EndDate = exp.EndDate,
                        Description = exp.Description
                    });
                }
            }

            // 6. Insert Social Links
            if (!string.IsNullOrEmpty(data.GitHub))
            {
                db.AddSocialLink(new SocialLink { PortfolioId = portfolioId, Platform = "GitHub", Url = data.GitHub });
            }
            if (!string.IsNullOrEmpty(data.LinkedIn))
            {
                db.AddSocialLink(new SocialLink { PortfolioId = portfolioId, Platform = "LinkedIn", Url = data.LinkedIn });
            }

            // 7. Insert Education
            if (data.Education != null)
            {
                foreach (var edu in data.Education)
                {
                    db.AddEducation(new Education
                    {
                        PortfolioId = portfolioId,
                        Degree = edu.Degree,
                        Institution = edu.Institution,
                        Year = edu.Year
                    });
                }
            }

            // 8. Generate ZIP file with the rendered HTML and download it
            var template = db.GetTemplateById(templateId);
            if (template == null) return HttpNotFound("Template not found.");

            string templatePath = Server.MapPath(template.FilePath);
            if (!System.IO.File.Exists(templatePath))
            {
                return HttpNotFound("Template file missing from disk.");
            }

            string html = System.IO.File.ReadAllText(templatePath);
            html = RenderTemplate(html, data);

            string safeName = (portfolioTitle ?? "Portfolio").Replace(" ", "_");

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    var entry = archive.CreateEntry("index.html");
                    using (var entryStream = entry.Open())
                    {
                        byte[] htmlBytes = Encoding.UTF8.GetBytes(html);
                        entryStream.Write(htmlBytes, 0, htmlBytes.Length);
                    }
                }

                memoryStream.Position = 0;
                return File(memoryStream.ToArray(), "application/zip", safeName + ".zip");
            }
        }

        // POST: Portfolio/Preview — live preview via AJAX
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Preview(int templateId, string jsonData)
        {
            if (!GuardUser()) return new HttpStatusCodeResult(401);

            PortifyDbContext db = new PortifyDbContext();
            var template = db.GetTemplateById(templateId);
            if (template == null) return HttpNotFound("Template not found.");

            string templatePath = Server.MapPath(template.FilePath);
            if (!System.IO.File.Exists(templatePath))
            {
                return HttpNotFound("Template file missing from disk.");
            }

            string html = System.IO.File.ReadAllText(templatePath);

            JavaScriptSerializer js = new JavaScriptSerializer();
            PortfolioData data = new PortfolioData();
            try
            {
                data = js.Deserialize<PortfolioData>(jsonData);
            }
            catch { /* Ignore empty/malformed data */ }

            html = RenderTemplate(html, data);

            return Content(html, "text/html", Encoding.UTF8);
        }

        // GET: Portfolio/Download?id=1
        public ActionResult Download(int id)
        {
            if (!GuardUser()) return RedirectToAction("Login", "Home");

            int userId = (int)Session["UserId"];
            PortifyDbContext db = new PortifyDbContext();
            var portfolio = db.GetPortfolioById(id);

            if (portfolio == null || portfolio.UserId != userId)
            {
                return HttpNotFound();
            }

            var template = db.GetTemplateById(portfolio.TemplateId);
            if (template == null) return HttpNotFound("Template not found.");

            string templatePath = Server.MapPath(template.FilePath);
            if (!System.IO.File.Exists(templatePath))
            {
                return HttpNotFound("Template file missing from disk.");
            }

            string html = System.IO.File.ReadAllText(templatePath);

            // Reconstruct PortfolioData from all related tables
            var personalInfo = db.GetPersonalInfoByPortfolioId(portfolio.Id);
            var skills = db.GetSkillsByPortfolioId(portfolio.Id);
            var projects = db.GetProjectsByPortfolioId(portfolio.Id);
            var experiences = db.GetExperiencesByPortfolioId(portfolio.Id);
            var socials = db.GetSocialLinksByPortfolioId(portfolio.Id);
            var education = db.GetEducationByPortfolioId(portfolio.Id);

            PortfolioData data = new PortfolioData
            {
                FullName = personalInfo?.FullName,
                Profession = personalInfo?.Profession,
                Email = personalInfo?.Email,
                Phone = personalInfo?.Phone,
                Location = personalInfo?.Location,
                AboutMe = portfolio.AboutMe,
                GitHub = socials?.FirstOrDefault(s => s.Platform == "GitHub")?.Url,
                LinkedIn = socials?.FirstOrDefault(s => s.Platform == "LinkedIn")?.Url,
                Skills = skills?.Select(s => new SkillEntry { SkillName = s.SkillName, SkillLevel = s.SkillLevel }).ToList(),
                Projects = projects?.Select(p => new ProjectEntry { ProjectTitle = p.ProjectTitle, Description = p.Description, TechStack = p.TechStack, GitHubLink = p.GitHubLink, LiveLink = p.LiveLink }).ToList(),
                Experience = experiences?.Select(e => new ExperienceEntry { CompanyName = e.CompanyName, Role = e.Role, StartDate = e.StartDate, EndDate = e.EndDate, Description = e.Description }).ToList(),
                Education = education?.Select(e => new EducationEntry { Degree = e.Degree, Institution = e.Institution, Year = e.Year }).ToList()
            };

            html = RenderTemplate(html, data);

            byte[] fileBytes = Encoding.UTF8.GetBytes(html);
            string fileName = (portfolio.Title ?? "Portfolio").Replace(" ", "_") + ".html";
            return File(fileBytes, "text/html", fileName);
        }

        /// <summary>
        /// Replaces all {{...}} placeholders in the template HTML with actual user data
        /// </summary>
        private string RenderTemplate(string html, PortfolioData data)
        {
            string fullName = data.FullName ?? "";
            string[] nameParts = fullName.Split(new[] { ' ' }, 2);
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string lastName = nameParts.Length > 1 ? nameParts[1] : "";

            // Replace BOTH placeholder styles used across templates
            html = html.Replace("{{FullName}}", fullName)
                       .Replace("{{FirstName}}", firstName)
                       .Replace("{{LastName}}", lastName)
                       .Replace("{{Email}}", data.Email ?? "")
                       .Replace("{{Phone}}", data.Phone ?? "")
                       .Replace("{{Profession}}", data.Profession ?? "")
                       .Replace("{{Location}}", data.Location ?? "")
                       .Replace("{{AboutMe}}", data.AboutMe ?? "")
                       .Replace("{{GitHubUsername}}", data.GitHub ?? "")
                       // Bio.* style placeholders (used in Portfolio1, Portfolio3, etc.)
                       .Replace("{{Bio.Name}}", fullName)
                       .Replace("{{Bio.Title}}", data.Profession ?? "")
                       .Replace("{{Bio.Summary}}", data.AboutMe ?? "")
                       .Replace("{{Bio.ContactEmail}}", data.Email ?? "")
                       .Replace("{{Bio.GitHub}}", data.GitHub ?? "")
                       .Replace("{{Bio.LinkedIn}}", data.LinkedIn ?? "");

            // Skills
            if (data.Skills != null && data.Skills.Any())
            {
                string skillHtml = string.Join("", data.Skills.Select(s => $"<span class=\"skill-tag\">{s.SkillName}</span>"));
                html = html.Replace("{{SkillsList}}", skillHtml);
            }
            else html = html.Replace("{{SkillsList}}", "");

            // Projects
            if (data.Projects != null && data.Projects.Any())
            {
                var sb = new StringBuilder();
                foreach (var p in data.Projects)
                {
                    sb.Append("<div class=\"project-card reveal stagger-1\">");
                    sb.Append("<div class=\"card-decoration\"></div>");
                    sb.Append("<div class=\"project-content\">");
                    sb.Append("<i class=\"fas fa-briefcase project-icon-top\"></i>");
                    sb.Append($"<h3>{p.ProjectTitle}</h3>");
                    sb.Append($"<p>{p.Description}</p>");
                    if (!string.IsNullOrEmpty(p.TechStack))
                    {
                        sb.Append($"<div class=\"project-tech\">{p.TechStack}</div>");
                    }
                    if (!string.IsNullOrEmpty(p.GitHubLink))
                    {
                        sb.Append("<div class=\"project-links\">");
                        sb.Append($"<a href=\"{p.GitHubLink}\" target=\"_blank\" class=\"project-link\"><i class=\"fab fa-github\"></i> View Source</a>");
                        sb.Append("</div>");
                    }
                    sb.Append("</div></div>");
                }
                html = html.Replace("{{ProjectsList}}", sb.ToString());
            }
            else html = html.Replace("{{ProjectsList}}", "");

            // Experience
            if (data.Experience != null && data.Experience.Any())
            {
                var sb = new StringBuilder();
                foreach (var exp in data.Experience)
                {
                    sb.Append("<div class=\"edu-item\">");
                    sb.Append($"<h4>{exp.Role}</h4>");
                    sb.Append($"<p class=\"institution\">{exp.CompanyName}</p>");
                    sb.Append($"<span class=\"year\">{exp.StartDate} - {exp.EndDate}</span>");
                    if (!string.IsNullOrEmpty(exp.Description))
                    {
                        sb.Append($"<p>{exp.Description}</p>");
                    }
                    sb.Append("</div>");
                }
                html = html.Replace("{{ExperienceList}}", sb.ToString());
            }
            else html = html.Replace("{{ExperienceList}}", "");

            // Education
            if (data.Education != null && data.Education.Any())
            {
                var sb = new StringBuilder();
                foreach (var edu in data.Education)
                {
                    sb.Append("<div class=\"edu-item\">");
                    sb.Append($"<h4>{edu.Degree}</h4>");
                    sb.Append($"<p class=\"institution\">{edu.Institution}</p>");
                    sb.Append($"<span class=\"year\">{edu.Year}</span>");
                    sb.Append("</div>");
                }
                html = html.Replace("{{EducationList}}", sb.ToString());
            }
            else html = html.Replace("{{EducationList}}", "");

            return html;
        }
    }
}
