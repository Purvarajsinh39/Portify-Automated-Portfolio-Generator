using System.Collections.Generic;

namespace Portify.Models
{
    /// <summary>
    /// DTO used to capture the full portfolio data from the Editor form (posted as JSON).
    /// This gets decomposed and saved into the relational tables.
    /// </summary>
    public class PortfolioData
    {
        // Personal Info (-> PortfolioPersonalInfo table)
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        // About Me (-> Portfolios.AboutMe column)
        public string AboutMe { get; set; }

        // Social Links (-> SocialLinks table)
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }

        // Skills (-> Skills table)
        public List<SkillEntry> Skills { get; set; }

        // Projects (-> Projects table)
        public List<ProjectEntry> Projects { get; set; }

        // Experience (-> Experiences table)
        public List<ExperienceEntry> Experience { get; set; }

        // Education (-> Education table)
        public List<EducationEntry> Education { get; set; }
    }

    public class SkillEntry
    {
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
    }

    public class ProjectEntry
    {
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string TechStack { get; set; }
        public string GitHubLink { get; set; }
        public string LiveLink { get; set; }
    }

    public class ExperienceEntry
    {
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }

    public class EducationEntry
    {
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string Year { get; set; }
    }
}
