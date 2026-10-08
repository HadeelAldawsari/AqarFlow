using AqarFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DB SETS
        // =========================

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<CustomerPropertyInterest> CustomerPropertyInterests { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }


        // =========================
        // MODEL CONFIGURATION
        // =========================

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer decimal precision
            modelBuilder.Entity<Customer>()
                .Property(c => c.MinBudget)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Customer>()
                .Property(c => c.MaxBudget)
                .HasPrecision(18, 2);

            // Property decimal precision
            modelBuilder.Entity<Property>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Property>()
                .Property(p => p.PropertySize)
                .HasPrecision(18, 2);

            // Deal decimal precision
            modelBuilder.Entity<Deal>()
                .Property(d => d.DealValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Deal>()
                .Property(d => d.Commission)
                .HasPrecision(18, 2);


            // PermissionRole composite key
            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new
                {
                    pr.RoleId,
                    pr.PermissionId
                });

            modelBuilder.Entity<PermissionRole>()
                .HasOne(pr => pr.Role)
                .WithMany(r => r.PermissionRoles)
                .HasForeignKey(pr => pr.RoleId);

            modelBuilder.Entity<PermissionRole>()
                .HasOne(pr => pr.Permission)
                .WithMany(p => p.PermissionRoles)
                .HasForeignKey(pr => pr.PermissionId);


            // RoleUser composite key
            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new
                {
                    ru.UserId,
                    ru.RoleId
                });

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(ru => ru.UserId);

            modelBuilder.Entity<RoleUser>()
                .HasOne(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(ru => ru.RoleId);
        }
    }
}