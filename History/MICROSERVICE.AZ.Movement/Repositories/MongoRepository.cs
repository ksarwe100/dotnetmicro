using MICROSERVICE.AZ.Movement.ConfigCollection;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System.Linq.Expressions;

namespace MICROSERVICE.AZ.Movement.Repositories;
public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
{
    private readonly Container _container;
    private readonly IMongoCollection<TDocument> _collection;
    private readonly FeedIterator<TDocument>? _collections;
    private QueryDefinition collectionName;

    public MongoRepository(IConfiguration configuration)
    {
        CosmosClient client = new(connectionString: configuration["CONFIG_CN_HISTORY"]); 
        _container = client.GetContainer(configuration["CONFIG_DATABASE_HISTORY"], configuration["CONFIG_DATABASE_TRANSACTION_COLLECTION"]);
    }

    public async Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
    {
        var query = _container.GetItemLinqQueryable<TDocument>().Where(filterExpression);
        var iterator = query.ToFeedIterator<TDocument>();
        var result = new List<TDocument>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            result.AddRange(response.Resource);
        }

        return result;
    }
}

