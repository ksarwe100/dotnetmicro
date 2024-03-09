using MICROSERVICES.AZ.Security.Models;
using MICROSERVICES.AZ.Security.Repositories;

namespace MICROSERVICE.AZ.Security.Data;

public class DbInitializer
{
    public static void Initialize(SecurityContext securityContext)
    {
        securityContext.Database.EnsureCreated();

        if (securityContext.UserAccess.Any())
        {
            return;   // DB has been seeded
        }

        var users = new IdentityModel[]
        {
                new IdentityModel{Email="mcaceres@microservice.com", Password="microservice#",FullName="Martin Caceres"},
                new IdentityModel{Email="jperez@microservice.com", Password="microservice#123",FullName="Juan Perez"},
                new IdentityModel{Email="mzarate@microservice.com", Password="microservice#",FullName="Martin Zapata"},
                new IdentityModel{Email="aparedes@microservice.com", Password="@microservice#",FullName="Antonio Paredes"},
        };

        foreach (IdentityModel s in users)
        {
            securityContext.UserAccess.Add(s);
        }

        securityContext.SaveChanges();
    }
}

