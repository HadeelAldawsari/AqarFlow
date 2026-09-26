namespace AqarFlow.Models
{
    // Role Table:
    // Stores the roles available in the system.
    // Example: Admin, Marketer, Viewer
    public class Role
    {
        // Primary Key
        public int Id { get; set; }

        // Role name
        public string Name { get; set; } = "";

        // Navigation Property
        // One role can have many permissions
        public ICollection<PermissionRole> PermissionRoles { get; set; }
            = new List<PermissionRole>();

        // Navigation Property
        // One role can be assigned to many users
        public ICollection<RoleUser> RoleUsers { get; set; }
            = new List<RoleUser>();
    }
}

