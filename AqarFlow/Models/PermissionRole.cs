using System.ComponentModel.DataAnnotations.Schema;

namespace AqarFlow.Models
{
    // PermissionRole Table:
    // Connects Roles with Permissions.
    // A role can have many permissions,
    // and a permission can belong to many roles.
    public class PermissionRole
    {
        // Foreign Key for Permission
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        // Navigation Property
        public Permission? Permission { get; set; }

        // Foreign Key for Role
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        // Navigation Property
        public Role? Role { get; set; }
    }
}