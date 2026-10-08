
using AqarFlow.Data;
using AqarFlow.Repositories;
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services;
using AqarFlow.Application.Services.Base;
using AqarFlow.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Alias for the Infrastructure DbContext
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
// DATABASE - ORIGINAL
// =====================================

// Keep the original DbContext temporarily
// because some controllers still use it.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDatabase")));


// =====================================
// DATABASE - INFRASTRUCTURE
// =====================================

// Infrastructure DbContext for Clean Architecture

builder.Services.AddDbContext<InfrastructureDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDatabase")));


// =====================================
// EXISTING REPOSITORIES
// =====================================

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddScoped<IFollowUpRepository, FollowUpRepository>();

builder.Services.AddScoped<IDealRepository, DealRepository>();

builder.Services.AddScoped<
    ICustomerPropertyInterestRepository,
    CustomerPropertyInterestRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

builder.Services.AddScoped<
    IPermissionRoleRepository,
    PermissionRoleRepository>();


// =====================================
// UNIT OF WORK
// =====================================

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// =====================================
// APPLICATION SERVICES
// =====================================

// Customer Service
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Property Service
builder.Services.AddScoped<IPropertyService, PropertyService>();

// Follow-up Service
builder.Services.AddScoped<IFollowUpService, FollowUpService>();

// Deal Service
builder.Services.AddScoped<IDealService, DealService>();

// Customer Property Interest Service
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

// Enable authorization services.
// Permission-based policies can be added here later.

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

// Authentication must run before Authorization.
app.UseAuthentication();

app.UseAuthorization();


// =====================================
// DEFAULT ROUTE
// =====================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
