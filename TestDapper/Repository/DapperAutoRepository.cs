using TestDapper.DbContext;
using TestDapper.Models;

namespace TestDapper.Repository;

#region AdditionalDbOperations Auto
//public class DapperAutoAdditionalDbOperations : IAdditionalDbOperations
//{

//    private const string TABLE_NAME = "auto";
//    private readonly NpgsqlConnection connection;

//    public DapperAutoAdditionalDbOperations()
//    {
//        connection = new NpgsqlConnection("AppContext");
//        connection.Open();
//    }

//    public async Task CreateTableIfNotExists()
//    {
//        var sql = $"CREATE TABLE if not exists {TABLE_NAME}" +
//                 $"(" +
//                 $"id serial PRIMARY KEY, " +
//                 $"brand VARCHAR (200) NOT NULL, " +
//                 $"model VARCHAR (200) NOT NULL, " +
//                 $")";

//        await connection.ExecuteAsync(sql);
//    }

//    public async Task<string> GetVersion()
//    {
//        var commandText = "SELECT version()";

//        var value = await connection.ExecuteScalarAsync<string>(commandText);
//        return value;
//    }
//}
#endregion

#region Old Version Auto
//public class DapperAutoRepository : IAutoRepository
//{

//    private const string TABLE_NAME = "auto";
//    private readonly NpgsqlConnection connection;

//    public DapperAutoRepository()
//    {
//        connection = new NpgsqlConnection("AppContext");
//        connection.Open();
//    }

//    public async Task AddCar(Auto auto)
//    {
//        string commandText = $"INSERT INTO {TABLE_NAME} (id, brand, model) VALUES (@id, @brand, @model)";

//        var queryArguments = new
//        {
//            id = auto.AutoId,
//            brand = auto.Brand,
//            model = auto.Model
//        };

//        await connection.ExecuteAsync(commandText, queryArguments);
//    }

//    public async Task DeleteCar(int id)
//    {
//        string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@p)";

//        var queryArguments = new
//        {
//            p = id
//        };

//        await connection.ExecuteAsync(commandText, queryArguments);
//    }

//    public async Task<Auto> GetCar(int? id)
//    {
//        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

//        var queryArgs = new { Id = id };
//        var auto = await connection.QueryFirstAsync<Auto>(commandText, queryArgs);
//        return auto;
//    }

//    public async Task<IEnumerable<Auto>> GetCars()
//    {
//        string commandText = $"SELECT * FROM {TABLE_NAME}";
//        var auto = await connection.QueryAsync<Auto>(commandText);

//        return auto;
//    }

//    public async Task UpdateCar(int id, Auto auto)
//    {
//        var commandText = $@"UPDATE {TABLE_NAME}
//                    SET brand = @brand WHERE id = @id";

//        var queryArgs = new
//        {
//            id = auto.AutoId,
//            brand = auto.Brand
//        };

//        await connection.ExecuteAsync(commandText, queryArgs);
//    }
//}
#endregion

public class DapperAutoRepository : IAutoRepository
{
    private readonly DapperContext _context;

    public DapperAutoRepository(DapperContext context)
    {
        this._context = context;
    }

    
    public async Task<IEnumerable<Auto>> GetCars()
    {
        var query = "SELECT * FROM auto";

        using (var connection = _context.CreateConnection())
        {
            var cars = await connection.QueryAsync<Auto>(query);
            return cars.ToList();
        }
    }

    
    public async Task<Auto> GetCar(int? id)
    {
        var query = "SELECT * FROM auto WHERE id = id";

        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QuerySingleOrDefaultAsync<Auto>(query, new { id });
            return company;
        }
    }

    public async Task CreateCar(Auto auto)
    {
        var query = "INSERT INTO auto (brand, model) VALUES (@brand, @model)";

        var parameters = new DynamicParameters();
        parameters.Add("brand", auto.Brand, DbType.String);
        parameters.Add("model", auto.Model, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task UpdateCar(int id, Auto auto)
    {
        var query = "INSERT INTO auto (brand, model) VALUES (@brand, @model WHERE id = @id)";
        var parameters = new DynamicParameters();
        parameters.Add("brand", auto.Brand, DbType.String);
        parameters.Add("model", auto.Model, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
    public async Task DeleteCar(int id)
    {

        var query = "DELETE FROM auto WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { id });
        }
    }
}