namespace quickpaste.api.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<object>> RetrieveAsync(int n);
    }
}
