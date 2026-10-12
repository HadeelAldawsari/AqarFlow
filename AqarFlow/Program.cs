
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services;
using AqarFlow.Application.Services.Base;
using AqarFlow.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Infrastructure DbContext
using InfrastructureDbContext = AqarFlow.Infrastructure.Data.AppDbContext;

var builder = WebApplication.CreateBuilder(args);


// =====================================
// MVC + LOCALIZATION
// =====================================

builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});


// =====================================
// DATABASE - INFRASTRUCTURE ONLY
// =====================================

builder.Services.AddDbContext<InfrastructureDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDatabase")));


// =====================================
// REPOSITORIES
// =====================================

// Customer
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.ICustomerRepository,
    AqarFlow.Infrastructure.Repositories.CustomerRepository>();

// Property
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IPropertyRepository,
    AqarFlow.Infrastructure.Repositories.PropertyRepository>();

// Follow-up
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IFollowUpRepository,
    AqarFlow.Infrastructure.Repositories.FollowUpRepository>();

// Deal
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IDealRepository,
    AqarFlow.Infrastructure.Repositories.DealRepository>();

// Customer Property Interest
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.ICustomerPropertyInterestRepository,
    AqarFlow.Infrastructure.Repositories.CustomerPropertyInterestRepository>();

// User
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IUserRepository,
    AqarFlow.Infrastructure.Repositories.UserRepository>();

// Role
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IRoleRepository,
    AqarFlow.Infrastructure.Repositories.RoleRepository>();

// Role User
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IRoleUserRepository,
    AqarFlow.Infrastructure.Repositories.RoleUserRepository>();

// Permission
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IPermissionRepository,
    AqarFlow.Infrastructure.Repositories.PermissionRepository>();

// Permission Role
builder.Services.AddScoped<
    AqarFlow.Application.Interfaces.IPermissionRoleRepository,
    AqarFlow.Infrastructure.Repositories.PermissionRoleRepository>();


// =====================================
// UNIT OF WORK
// =====================================

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// =====================================
// APPLICATION SERVICES
// =====================================

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddScoped<IFollowUpService, FollowUpService>();

builder.Services.AddScoped<IDealService, DealService>();

builder.Services.AddScoped<
    ICustomerPropertyInterestService,
    CustomerPropertyInterestService>();


// =====================================
// AUTHENTICATION
// =====================================

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";

        options.AccessDeniedPath = "/Accounts/AccessDenied";

        options.ExpireTimeSpan = TimeSpan.FromHours(8);

        options.SlidingExpiration = true;

        options.Cookie.HttpOnly = true;

        options.Cookie.SecurePolicy =
            Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;

        options.Cookie.SameSite =
            Microsoft.AspNetCore.Http.SameSiteMode.Lax;
    });


// =====================================
// AUTHORIZATION
// =====================================

builder.Services.AddAuthorization();


// =====================================
// BUILD APPLICATION
// =====================================

var app = builder.Build();


// =====================================
// LOCALIZATION CONFIGURATION
// =====================================

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),

    SupportedCultures = supportedCultures,

    SupportedUICultures = supportedCultures
};

localizationOptions.RequestCultureProviders.Insert(
    0,
    new CookieRequestCultureProvider()
);


// =====================================
// HTTP PIPELINE
// =====================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRequestLocalization(localizationOptions);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// =====================================
// DEFAULT ROUTE
// =====================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
