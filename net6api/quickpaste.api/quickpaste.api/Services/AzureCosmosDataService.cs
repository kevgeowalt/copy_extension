using Microsoft.Azure.Cosmos;
using quickpaste.api.Interfaces;

namespace quickpaste.api.Services
{
    public class AzureCosmosDataService : IDataService
    {
        private readonly Database database;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        public AzureCosmosDataService(Database database)
        {
            this.database = database;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> RetrieveAsync(int n)
        {
            try
            {
                string containerName = Environment.GetEnvironmentVariable("CONTAINER_NAME") ?? string.Empty;
                var container = database.GetContainer(containerName);

                var query = new QueryDefinition(query: "select * from items x");
                using FeedIterator<object> feed = container.GetItemQueryIterator<object>(queryDefinition: query);

                List<object> results = new List<object>();

                while (feed.HasMoreResults)
                {
                    FeedResponse<object> response = await feed.ReadNextAsync();
                    foreach(object item in response)
                    {

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}