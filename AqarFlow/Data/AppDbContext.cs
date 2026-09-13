using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Data
{
    // AppDbContext:
    // Connects the application models to the database
    // and allows Entity Framework Core to create and manage the tables.
    public class AppDbContext : DbContext
    {
        // Constructor used to receive database configuration
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Customer table
        public DbSet<Customer> Customers { get; set; }

        // Property table
        public DbSet<Property> Properties { get; set; }

        // Follow-up table
        public DbSet<FollowUp> FollowUps { get; set; }

        // Customer and property interest relationship table
        public DbSet<CustomerPropertyInterest> CustomerPropertyInterests { get; set; }

        // Deal table
        public DbSet<Deal> Deals { get; set; }

        // Permissions System Tables
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Primary Key for PermissionRole
            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new { pr.RoleId, pr.PermissionId });

            // Role -> PermissionRole Relationship
            modelBuilder.Entity<PermissionRole>()
                .HasOne(pr => pr.Role)
                .WithMany(r => r.PermissionRoles)
                .HasForeignKey(pr => pr.RoleId);

            // Permission -> PermissionRole Relationship
            modelBuilder.Entity<PermissionRole>()
                .HasOne(pr => pr.Permission)
                .WithMany(p => p.PermissionRoles)
                .HasForeignKey(pr => pr.PermissionId);





        }
    }
}