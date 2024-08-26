using System.Security.Claims;
using idunno.Authentication.Basic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Slayden.Core.Auth;

public static class BasicAuthServiceCollectionExtensions
{
    public static void AddBasicAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var basicAuthOptions = new BasicAuthOptions();
        configuration.GetSection(BasicAuthOptions.SectionKey).Bind(basicAuthOptions);

        services
            .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
            .AddBasic(options =>
            {
                options.Realm = "Basic Auth";
                options.Events = new BasicAuthenticationEvents
                {
                    OnValidateCredentials = context =>
                    {
                        if (
                            context.Username == basicAuthOptions.Username
                            && context.Password == basicAuthOptions.Password
                        )
                        {
                            var claims = new[]
                            {
                                new Claim(
                                    ClaimTypes.NameIdentifier,
                                    context.Username,
                                    ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer
                                ),
                                new Claim(
                                    ClaimTypes.Name,
                                    context.Username,
                                    ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer
                                ),
                            };

                            context.Principal = new ClaimsPrincipal(
                                new ClaimsIdentity(claims, context.Scheme.Name)
                            );
                            context.Success();
                        }

                        return Task.CompletedTask;
                    },
                };
            });

        services.AddAuthorization();
    }

    public static void UseBasicAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
