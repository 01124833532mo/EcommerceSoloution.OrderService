using eCommerce.OrdersMicroservice.DataAccessLayer.RepositoryContracts;
using eCommerce.OrdersMicroservice.DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.OrdersMicroservice.DataAccessLayer;

public static class DependencyInjection
{
  public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
  {
    // Read connection string template from configuration or build from environment variables
    string? connectionStringTemplate = configuration.GetConnectionString("MongoDB");

    string mongoHost = Environment.GetEnvironmentVariable("MONGODB_HOST") ?? "localhost";
    string mongoPort = Environment.GetEnvironmentVariable("MONGODB_PORT") ?? "27017";

    string connectionString;

    if (!string.IsNullOrWhiteSpace(connectionStringTemplate))
    {
      // Replace placeholders if present
      connectionString = connectionStringTemplate
        .Replace("$MONGO_HOST", mongoHost)
        .Replace("$MONGO_PORT", mongoPort);
    }
    else
    {
      // Fallback to building a basic connection string from environment variables/defaults
      connectionString = $"mongodb://{mongoHost}:{mongoPort}";
    }

    services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

    services.AddScoped<IMongoDatabase>(provider =>
    {
      IMongoClient client = provider.GetRequiredService<IMongoClient>();
      return client.GetDatabase("OrdersDatabase");
    });

    services.AddScoped<IOrdersRepository, OrdersRepository>();


    return services;
  }
}
