using MICROSERVICE.AZ.Movement.Repositories;

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


builder.Services.AddDistributedRedisCache(o =>
{
    o.Configuration = builder.Configuration["CONFIG_CN_REDIS"];
});


builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
