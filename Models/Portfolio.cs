using System;
using System.Collections.Generic;

namespace Portify.Models
{
    // Matches: dbo.Portfolios
    public class Portfolio
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public string Title { get; set; }
        public string AboutMe { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Matches: dbo.PortfolioPersonalInfo
    public class PortfolioPersonalInfo
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string ProfileImagePath { get; set; }
    }

    // Matches: dbo.Projects
    public class Project
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string TechStack { get; set; }
        public string GitHubLink { get; set; }
        public string LiveLink { get; set; }
    }

    // Matches: dbo.Skills
    public class Skill
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
    }

    // Matches: dbo.Experiences
    public class Experience
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }

    // Matches: dbo.SocialLinks
    public class SocialLink
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
    }

    // Matches: dbo.Education
    public class Education
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string Year { get; set; }
    }
}
