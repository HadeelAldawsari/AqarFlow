using System.ComponentModel.DataAnnotations;

namespace AqarFlow.Domain.Models
{
    public class User
    {
        // Primary Key
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        // Stores the hashed password
        [Required]
        public string PasswordHash { get; set; } = "";

        // Determines if the user account is active
        public bool IsActive { get; set; } = true;

        // User creation date
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        // One user can have one or more roles
        public ICollection<RoleUser> RoleUsers { get; set; }
            = new List<RoleUser>();
    }
}

