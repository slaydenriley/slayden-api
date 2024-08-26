using Microsoft.Extensions.DependencyInjection;
using Slayden.Core.Data;
using Slayden.Core.Repositories;
using Slayden.Core.Services;

namespace Slayden.Core;

public static class SlaydenCoreServiceCollectionExtension
{
    public static void AddSlaydenCore(this IServiceCollection services)
    {
        // Cosmos
        services.AddTransient<ICosmosContainerProvider, CosmosContainerProvider>();

        // Posts
        services.AddSingleton<IPostService, PostService>();
        services.AddTransient<IPostRepository, PostRepository>();

        services.AddHttpClient();
    }
}
