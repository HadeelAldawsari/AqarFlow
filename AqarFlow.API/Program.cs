
using AqarFlow.Application.Interfaces.Base;
using AqarFlow.Application.Services;
using AqarFlow.Application.Services.Base;
using AqarFlow.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

// Infrastructure DbContext
using InfrastructureDbContext =
    AqarFlow.Infrastructure.Data.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// API CONTROLLERS
// =====================================

builder.Services.AddControllers();

// =====================================
// SWAGGER
// =====================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================
// DATABASE
// =====================================

builder.Services.AddDbContext<InfrastructureDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "DefaultDatabase")));

// =====================================
// UNIT OF WORK
// =====================================

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// =====================================
// APPLICATION SERVICES
// =====================================

builder.Services.AddScoped<
    ICustomerService,
    CustomerService>();

builder.Services.AddScoped<
    IPropertyService,
    PropertyService>();

builder.Services.AddScoped<
    IFollowUpService,
    FollowUpService>();

builder.Services.AddScoped<
    IDealService,
    DealService>();

builder.Services.AddScoped<
    ICustomerPropertyInterestService,
    CustomerPropertyInterestService>();

// =====================================
// BUILD APPLICATION
// =====================================

var app = builder.Build();

// =====================================
// SWAGGER UI
// =====================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =====================================
// HTTP PIPELINE
// =====================================

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
