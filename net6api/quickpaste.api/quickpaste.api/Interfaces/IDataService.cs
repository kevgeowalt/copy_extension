using quickpaste.api.Models;

namespace quickpaste.api.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<QuickPasteModel>> RetrieveAsync(int n);
    }
}
