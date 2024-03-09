using MICROSERVICE.AZ.Movement.DTOs;
using MICROSERVICE.AZ.Movement.Models;
using MICROSERVICE.AZ.Movement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MICROSERVICE.AZ.Movement.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class MovementController : ControllerBase
{
    private readonly IMongoRepository<TransactionModel> _mongoRepository;
    private readonly IDistributedCache _cache;

    public MovementController(IMongoRepository<TransactionModel> mongoRepository, IDistributedCache cache)
    {
        _mongoRepository = mongoRepository;
        _cache = cache;
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        string _key = $"key-account-{id}";
        var dataCache = await _cache.GetStringAsync(_key);
        if(dataCache == null)
        {
            var movementsAccount = await _mongoRepository.FilterBy(
                    filter => filter.AccountId == id
                );

            if (movementsAccount.FirstOrDefault() == null)
            {
                return NotFound();
            }

            var options = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromSeconds(30));

            _cache.SetString(_key, JsonSerializer.Serialize(movementsAccount),
                options);

            return Ok(movementsAccount);

        }else{

            var movementsAccount = JsonSerializer.Deserialize<List<TransactionModel>>(dataCache);
            return Ok(movementsAccount);
        }
    }

}

