using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using Slayden.Core.Dtos;
using Slayden.Core.Options;

namespace Slayden.Core.Repositories;

public interface IPostRepository
{
    Task<PostDto?> GetPostById(Guid id);

    Task<List<PostDto>> GetAllPosts();

    Task<PostDto?> CreatePost(string title, string body);

    Task<PostDto?> UpdatePost(Guid id, string? title, string? body);

    Task<PostDto?> DeletePost(Guid id);
}

public class PostRepository(
    IOptions<CosmosOptions> cosmosOptions,
    IOptions<UserOptions> userOptions
) : IPostRepository
{
    public async Task<PostDto?> GetPostById(Guid id)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);

        var container = client.GetDatabase(cosmosOptions.Value.Database).GetContainer("posts");
        var queryable = container.GetItemLinqQueryable<PostDto>();

        using var feed = queryable.Where(post => post.id == id.ToString()).ToFeedIterator();

        var results = new List<PostDto>();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            results.AddRange(response);
        }

        return results.Count != 1 ? null : results.SingleOrDefault();
    }

    public async Task<List<PostDto>> GetAllPosts()
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);

        var container = client.GetDatabase(cosmosOptions.Value.Database).GetContainer("posts");
        var queryable = container.GetItemLinqQueryable<PostDto>();

        using var feed = queryable.ToFeedIterator();

        var results = new List<PostDto>();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<PostDto?> CreatePost(string title, string body)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);
        var container = client.GetDatabase(cosmosOptions.Value.Database).GetContainer("posts");

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

    public async Task<PostDto?> UpdatePost(Guid id, string? title, string? body)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);
        var container = client.GetDatabase(cosmosOptions.Value.Database).GetContainer("posts");

        var userId = userOptions.Value.Id.ToString();

        var patchOperation = new List<PatchOperation>();
        if (title != null)
        {
            patchOperation.Add(PatchOperation.Add("/title", title));
        }

        if (body != null)
        {
            patchOperation.Add(PatchOperation.Add("/body", body));
        }

        patchOperation.Add(PatchOperation.Add("/updatedAt", DateTime.UtcNow));

        var response = await container.PatchItemAsync<PostDto>(
            id: id.ToString(),
            partitionKey: new PartitionKey(userId),
            patchOperation
        );

        return response.Resource;
    }

    public async Task<PostDto?> DeletePost(Guid id)
    {
        var client = new CosmosClient(cosmosOptions.Value.ConnectionString);
        var container = client.GetDatabase(cosmosOptions.Value.Database).GetContainer("posts");

        var userId = userOptions.Value.Id.ToString();

        var response = await container.PatchItemAsync<PostDto>(
            id: id.ToString(),
            partitionKey: new PartitionKey(userId),
            new PatchOperation[]
            {
                PatchOperation.Add("/deletedAt", DateTime.UtcNow)
            }
        );

        return response.Resource;
    }
}
