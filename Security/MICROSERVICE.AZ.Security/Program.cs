using MICROSERVICES.AZ.Security.Components;
using MICROSERVICES.AZ.Security.Data;
using MICROSERVICES.AZ.Security.Repositories;
using MICROSERVICES.AZ.Security.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var cnAppConfig = builder.Configuration["CONFIG_CN_APP_EXTERNAL"];
builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(opt =>
    {
        opt.Connect(cnAppConfig)
        .ConfigureRefresh(refre =>
        {
            refre.Register("VERSION", true).SetCacheExpiration(TimeSpan.FromSeconds(5));
        });
    });
});

builder.Services.AddDbContext<SecurityContext>(
               options => options.UseSqlServer(builder.Configuration["CONFIG_CN_SECURITY"]));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

DbCreated.CreateDbIfNotExists(app);
app.Run();
