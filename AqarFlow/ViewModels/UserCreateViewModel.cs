using System.ComponentModel.DataAnnotations;

namespace AqarFlow.ViewModels
{
    // ViewModel used when creating a new user
    public class UserCreateViewModel
    {
        [Required]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        // Selected role from the dropdown list
        public int? RoleId { get; set; }

        // Determines if the account is active
        public bool IsActive { get; set; } = true;
    }
}

