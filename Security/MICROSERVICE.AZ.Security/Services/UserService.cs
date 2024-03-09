using MICROSERVICES.AZ.Security.Models;
using MICROSERVICES.AZ.Security.Repositories;

namespace MICROSERVICES.AZ.Security.Services;

public class UserService : IUserService
{
    private readonly SecurityContext _securityContext;

    public UserService(SecurityContext securityContext) => _securityContext = securityContext;

    public Task<bool> Validated(IdentityModel identityModel)
    {
        var users = _securityContext.UserAccess.ToList();
        var user = users.Where(x => x.Email == identityModel.Email && x.Password == identityModel.Password).FirstOrDefault();
        if (user == null)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }
}

