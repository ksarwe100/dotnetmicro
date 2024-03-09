using MICROSERVICES.AZ.Account.DTOs;
using MICROSERVICES.AZ.Account.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MICROSERVICES.AZ.Account.Repositories;
public class AccountRepository : IAccountRepository
{
    private readonly ContextDatabase _context;

    public AccountRepository(ContextDatabase context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountResponse>> GetAccounts()
    {
        var result = (from a in _context.Accounts
                      join c in _context.Customers
                      on a.IdCustomer equals c.IdCustomer
                      select new
                      {
                          IdAccount = a.IdAccount,
                          TotalAmount = a.TotalAmount,
                          IdCustomer = a.IdCustomer,
                          FullName = c.FullName
                      }).ToList();

        return (from item in result
                select new AccountResponse() { IdAccount = item.IdAccount, TotalAmount = item.TotalAmount, IdCustomer = item.IdCustomer, FullName = item.FullName }).ToList();
    }

    public Task<bool> UpdateAccount(int idAccount, decimal amount)
    {
        using (_context)
        {
            var result = _context.Accounts?.FirstOrDefault(b => b.IdAccount == idAccount);
            if (result != null)
            {
                result.IdAccount = idAccount;
                result.TotalAmount += amount;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
        }
        return Task.FromResult(false);
    }
}

