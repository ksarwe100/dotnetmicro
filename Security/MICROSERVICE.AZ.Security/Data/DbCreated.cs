using MICROSERVICE.AZ.Security.Data;
using MICROSERVICES.AZ.Security.Repositories;

namespace MICROSERVICES.AZ.Security.Data;

public static class DbCreated
{
    public static void CreateDbIfNotExists(IHost host)
    {

        using (var scope = host.Services.CreateScope())
        {

            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<SecurityContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}

