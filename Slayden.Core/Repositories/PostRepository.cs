using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using Slayden.Core.Dtos;
using Slayden.Core.Models;
using Slayden.Core.Options;

namespace Slayden.Core.Repositories;

public interface IPostRepository
{
    Task<Post?> GetPostById(Guid id);

    Task<PostDto?> CreatePost(string title, string body);
}

public class PostRepository(
    IOptions<CosmosOptions> cosmosOptions,
    IOptions<UserOptions> userOptions
) : IPostRepository
{
    public async Task<Post?> GetPostById(Guid id)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);

        var container = client.GetDatabase("slayden-db").GetContainer("posts");
        var queryable = container.GetItemLinqQueryable<Post>();

        using var feed = queryable.Where(p => p.Id == id).ToFeedIterator();

        List<Post> results = [];
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            results.AddRange(response);
        }

        return results.Count != 1 ? null : results.SingleOrDefault();
    }

    public async Task<PostDto?> CreatePost(string title, string body)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);
        var container = client.GetDatabase("slayden-db").GetContainer("posts");

        var now = DateTime.UtcNow.ToString("O");
        var userId = userOptions.Value.Id.ToString();

        return await container.CreateItemAsync<PostDto?>(
            item: new PostDto
            {
                id = Guid.NewGuid().ToString(),
                userId = userId,
                title = title,
                body = body,
                createdAt = now,
                updatedAt = now,
            },
            partitionKey: new PartitionKey(userId)
        );
    }
}
