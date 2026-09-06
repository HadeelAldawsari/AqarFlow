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


    }
}
