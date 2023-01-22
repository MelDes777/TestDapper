namespace TestDapper.Repository
{
    public interface IAdditionalDbOperations
    {
        Task<string> GetVersion();

        Task CreateTableIfNotExists();
    }
}
