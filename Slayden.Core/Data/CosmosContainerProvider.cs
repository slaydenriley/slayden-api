using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Slayden.Core.Options;

namespace Slayden.Core.Data;

public interface ICosmosContainerProvider
{
    public Container Posts { get; }
}

public class CosmosContainerProvider : ICosmosContainerProvider
{
    private readonly IOptions<CosmosOptions> _cosmosOptions;
    private readonly CosmosClient _cosmosClient;

    public CosmosContainerProvider(IOptions<CosmosOptions> cosmosOptions)
    {
        _cosmosOptions = cosmosOptions;
        _cosmosClient = CreateClient();
        Posts = GetContainer(Containers.PostContainer);
    }

    private CosmosClient CreateClient()
    {
        return new CosmosClient(_cosmosOptions.Value.ConnectionString);
    }

    private Container GetContainer(string containerId)
    {
        return _cosmosClient.GetContainer(_cosmosOptions.Value.Database, containerId);
    }

    public Container Posts { get; }
}
