using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portify.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } // Storing plain text as requested

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
