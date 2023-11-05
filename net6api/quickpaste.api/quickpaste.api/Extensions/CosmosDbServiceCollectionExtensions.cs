using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace quickpaste.api.Extensions
{
    public static class CosmosDbServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCosmosDbClient(this IServiceCollection services, string databaseUrl, string databaseName)
        {
            services.AddSingleton(provider =>
            {
                var credential = new DefaultAzureCredential();
                var authKey = Environment.GetEnvironmentVariable("DATABASE_KEY") ?? string.Empty;

                var cosmonsClient = new CosmosClient(databaseUrl,authKey);

                return cosmonsClient;
            });

            services.AddSingleton(provider =>
            {
                var cosmosClient = provider.GetRequiredService<CosmosClient>();
                var database = cosmosClient.GetDatabase(databaseName);

                return database;
            });

            return services;
        }
    }
}
