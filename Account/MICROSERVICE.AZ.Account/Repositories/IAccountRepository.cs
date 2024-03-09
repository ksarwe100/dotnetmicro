using MICROSERVICES.AZ.Account.DTOs;

namespace MICROSERVICES.AZ.Account.Repositories;

public interface IAccountRepository
{
    Task<IEnumerable<AccountResponse>> GetAccounts();
    Task<bool> UpdateAccount(int idAccount, decimal amount);
}

