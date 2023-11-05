using Microsoft.Azure.Cosmos;
using quickpaste.api.Interfaces;
using quickpaste.api.Models;

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
        public async Task<IEnumerable<QuickPasteModel>> RetrieveAsync(int n)
        {
            List<QuickPasteModel> results = new List<QuickPasteModel>();
            try
            {
                string containerName = Environment.GetEnvironmentVariable("DB_CONTAINER_NAME") ?? string.Empty;
                var container = database.GetContainer(containerName);

                var query = new QueryDefinition(query: "select * from items x");
                using FeedIterator<QuickPasteModel> feed = container.GetItemQueryIterator<QuickPasteModel>(queryDefinition: query);

                while (feed.HasMoreResults)
                {
                    FeedResponse<QuickPasteModel> response = await feed.ReadNextAsync();
                    foreach(QuickPasteModel item in response)
                    {
                        results.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return results;
        }
    }
}