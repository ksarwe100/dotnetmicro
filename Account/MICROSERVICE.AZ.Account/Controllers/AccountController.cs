using MICROSERVICES.AZ.Account.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MICROSERVICES.AZ.Account.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _accountRepository.GetAccounts());
    }
}

