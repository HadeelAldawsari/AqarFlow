namespace AqarFlow.Domain.Models
{
    public class RoleUser
    {
        // Foreign key for User
        public int UserId { get; set; }

        public User? User { get; set; }


        // Foreign key for Role
        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}