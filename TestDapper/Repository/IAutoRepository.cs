using TestDapper.Models;

namespace TestDapper.Repository
{
    public interface IAutoRepository
    {
        Task<IEnumerable<Auto>> GetCars();

        Task<Auto> GetCar(int? id);

        Task CreateCar(Auto auto);

        Task UpdateCar(int id, Auto auto);

        Task DeleteCar(int id);

        
    }

}
