using System;
using System.Collections.Generic;

namespace Portify.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalPortfolios { get; set; }
        public int TotalTemplates { get; set; }
        public double AverageRating { get; set; }
        public List<TemplateUsageStat> TemplateUsage { get; set; }
        public List<DailyStat> DailyPortfolios { get; set; }
        public List<DailyStat> DailyRegistrations { get; set; }

        public AdminDashboardViewModel()
        {
            TemplateUsage = new List<TemplateUsageStat>();
            DailyPortfolios = new List<DailyStat>();
            DailyRegistrations = new List<DailyStat>();
        }
    }

    public class TemplateUsageStat
    {
        public string TemplateName { get; set; }
        public int UsageCount { get; set; }
    }

    public class DailyStat
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
