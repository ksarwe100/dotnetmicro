using MICROSERVICES.AZ.Security.Components;
using MICROSERVICES.AZ.Security.DTOs;
using MICROSERVICES.AZ.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICROSERVICES.AZ.Security.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtGenerator _jwtGenerator;

    public AuthController(IUserService userService, IJwtGenerator jwtGenerator)
    {
        _userService = userService;
        _jwtGenerator = jwtGenerator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
    {
        if (await _userService.Validated(new Models.IdentityModel() { Email = userRequest.Email, Password = userRequest.Password }))
        {
            UserResponse userResponse = new UserResponse()
            {
                Token = _jwtGenerator.Create(),
                Username = userRequest.Email,
                AccessTokenExpiration = "1d"
            };

            return Ok(userResponse);
        }
        return Unauthorized();
    }
}

