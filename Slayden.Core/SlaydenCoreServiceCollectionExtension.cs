using Microsoft.Extensions.DependencyInjection;
using Slayden.Core.Repositories;
using Slayden.Core.Services;

namespace Slayden.Core;

public static class SlaydenCoreServiceCollectionExtension
{
    public static void AddSlaydenCore(this IServiceCollection services)
    {
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<IPostRepository, PostRepository>();

        services.AddHttpClient();
    }
}
