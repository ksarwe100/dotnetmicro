using MICROSERVICES.AZ.Security.Models;

namespace MICROSERVICES.AZ.Security.Services;
public interface IUserService
{
    Task<bool> Validated(IdentityModel identityModel);
}

