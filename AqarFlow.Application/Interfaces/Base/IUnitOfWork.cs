using AqarFlow.Domain.Models;

namespace AqarFlow.Application.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Property> Properties { get; }
        IRepository<FollowUp> FollowUps { get; }
        IRepository<CustomerPropertyInterest> CustomerPropertyInterests { get; }
        IRepository<Deal> Deals { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<RoleUser> RoleUsers { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<PermissionRole> PermissionRoles { get; }

        int Complete();
    }
}

