namespace AqarFlow.Models
{
    // Permission Table:
    // Stores all available permissions in the system.
    // Example: View Customers, Create Customers, Edit Customers, Delete Customers
    public class Permission
    {
        // Primary Key
        public int Id { get; set; }

        // Permission name
        public string Name { get; set; } = "";

        // Navigation Property
        // One permission can be assigned to many roles
        public ICollection<PermissionRole>? PermissionRoles { get; set; }
    }
}
