using System;

namespace Portify.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TemplateId { get; set; }
        public int? PortfolioId { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        // Additional properties for display (optional, can be populated in DbContext)
        public string UserName { get; set; }
        public string TemplateName { get; set; }
    }
}
