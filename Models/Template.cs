using System;

namespace Portify.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailPath { get; set; }
        public string FilePath { get; set; }
        public string CssPath { get; set; }
        public string ConfigPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
