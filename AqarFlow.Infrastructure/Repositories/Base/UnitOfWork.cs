using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;

namespace AqarFlow.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            Customers = new Repository<Customer>(_db);
            Properties = new Repository<Property>(_db);
            FollowUps = new Repository<FollowUp>(_db);
            CustomerPropertyInterests =
                new Repository<CustomerPropertyInterest>(_db);
            Deals = new Repository<Deal>(_db);
            Users = new Repository<User>(_db);
            Roles = new Repository<Role>(_db);
            RoleUsers = new Repository<RoleUser>(_db);
            Permissions = new Repository<Permission>(_db);
            PermissionRoles = new Repository<PermissionRole>(_db);
        }

        public IRepository<Customer> Customers { get; }
        public IRepository<Property> Properties { get; }
        public IRepository<FollowUp> FollowUps { get; }
        public IRepository<CustomerPropertyInterest> CustomerPropertyInterests { get; }
        public IRepository<Deal> Deals { get; }
        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<RoleUser> RoleUsers { get; }
        public IRepository<Permission> Permissions { get; }
        public IRepository<PermissionRole> PermissionRoles { get; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}


