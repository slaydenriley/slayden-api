using Serilog;
using Slayden.Core.Options;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.Configure<CosmosOptions>(builder.Configuration.GetSection("Cosmos"));
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwaggerUI();
    }

    app.UseHealthChecks("/health-check/");
    app.MapControllers();
    
    app.Run();
}
catch (Exception e)
{
    Log.Logger.Fatal("Unexpected application termination");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
