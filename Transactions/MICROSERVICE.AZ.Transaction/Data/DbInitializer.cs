using MICROSERVICE.AZ.Transaction.Contexts;

namespace MICROSERVICE.AZ.Transaction.Data;
public class DbInitializer
{
    public static void Initialize(TransactionContext context)
    {
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
}

