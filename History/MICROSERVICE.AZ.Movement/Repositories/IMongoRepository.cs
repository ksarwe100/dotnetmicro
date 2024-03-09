using MICROSERVICE.AZ.Movement.ConfigCollection;
using System.Linq.Expressions;

namespace MICROSERVICE.AZ.Movement.Repositories;
public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
    
}

