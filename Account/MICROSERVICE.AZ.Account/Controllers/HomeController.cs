using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MICROSERVICES.AZ.Account.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public string Get() => "Account Microservice Running ....";
}

