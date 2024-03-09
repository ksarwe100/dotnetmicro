using MICROSERVICES.AZ.Account.Data;
using MICROSERVICES.AZ.Account.Listener;
using MICROSERVICES.AZ.Account.Repositories;
using MICROSERVICES.AZ.Account.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddHostedService<TransactionManagerService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextDatabase>(
                     options => options.UseSqlServer(builder.Configuration["CONFIG_CN_ACCOUNT"]));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

DbCreated.CreateDbIfNotExists(app);
app.Run();
