using Microsoft.AspNetCore.Mvc;
using Serilog;
using Slayden.Core;
using Slayden.Core.Options;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder
        .Configuration.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.User.json", true)
        .AddEnvironmentVariables();

    builder.Services.Configure<CosmosOptions>(builder.Configuration.GetSection("Cosmos"));
    builder.Services.Configure<UserOptions>(builder.Configuration.GetSection("User"));

    builder.Services.AddSlaydenCore();
    builder.Services.AddHealthChecks();
    builder.Services.AddControllers();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "v1";
            config.Title = "Slayden API";
        });
    }
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseOpenApi();
        app.UseSwaggerUi();
    }

    app.UseHealthChecks("/health-check/");
    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Log.Logger.Fatal("Unexpected application termination: {exception}", e);
    throw;
}
finally
{
    Log.CloseAndFlush();
}
