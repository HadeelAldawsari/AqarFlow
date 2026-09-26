using AqarFlow.Data;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// =========================
// MVC + LOCALIZATION
// =========================

// Add MVC services with localization
builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Add localization services
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});


// =========================
// DATABASE
// =========================

// Register database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDatabase")));


// =========================
// REPOSITORIES
// =========================

// Register repositories
// REPOSITORIES
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


// =========================
// AUTHENTICATION
// =========================

// Configure cookie authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });


var app = builder.Build();


// =========================
// LOCALIZATION
// =========================

// Supported application languages
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

var localizationOptions =
    new RequestLocalizationOptions
    {
        // English is the default language
        DefaultRequestCulture =
            new RequestCulture("en"),

        SupportedCultures =
            supportedCultures,

        SupportedUICultures =
            supportedCultures
    };

// Read selected language from cookie
localizationOptions.RequestCultureProviders.Insert(
    0,
    new CookieRequestCultureProvider()
);


// =========================
// HTTP PIPELINE
// =========================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();


// Enable localization
app.UseRequestLocalization(
    localizationOptions
);

app.UseRouting();


// Authentication must come before Authorization
app.UseAuthentication();

app.UseAuthorization();


// =========================
// ROUTES
// =========================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();