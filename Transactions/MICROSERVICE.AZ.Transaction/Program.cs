using MICROSERVICE.AZ.Transaction.Contexts;
using MICROSERVICE.AZ.Transaction.Data;
using MICROSERVICE.AZ.Transaction.Messages;
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

builder.Services.AddControllers();


builder.Services.AddDbContext<TransactionContext>(opt =>
                   opt.UseNpgsql(builder.Configuration["CONFIG_CN_TRANSACTION"]));

builder.Services.AddScoped<IEventBus, EventBus>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

DbCreated.CreateDbIfNotExists(app);


app.Run();
