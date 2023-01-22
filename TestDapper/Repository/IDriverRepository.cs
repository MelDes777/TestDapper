using TestDapper.Models;

namespace TestDapper.Repository
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDrivers();

        Task<Driver> GetDriver(int? id);

        Task CreateDriver(Driver driver);

        Task UpdateDriver(int id, Driver driver);

        Task DeleteDriver(int id);
    }
   
}
