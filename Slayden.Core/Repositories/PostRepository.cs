using Slayden.Core.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Slayden.Core.Options;
using Microsoft.Extensions.Options;

namespace Slayden.Core.Repositories;

public interface IPostRepository
{
    Task<Post?> GetPostById(Guid id);
}

public class PostRepository(IOptions<CosmosOptions> cosmosOptions) : IPostRepository
{
    public async Task<Post?> GetPostById(Guid id)
    {
        var client = new CosmosClient(
            connectionString: cosmosOptions.Value.ConnectionString
        );
        
        var container = client.GetDatabase("slayden-db").GetContainer("posts");
        var queryable = container.GetItemLinqQueryable<Post>();
        
        using var feed = queryable
            .Where(p => p.Id == id)
            .ToFeedIterator();
        
        List<Post> results = [];
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            results.AddRange(response);
        }

        return results.Count != 1 ? null : results.SingleOrDefault();
    }
}