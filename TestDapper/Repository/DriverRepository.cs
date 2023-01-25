using TestDapper.DbContext;
using TestDapper.Models;

namespace TestDapper.Repository;

#region AdditionalDbOperations Driver
//public class DapperDriverAdditionalDbOperations : IAdditionalDbOperations
//{

//    private const string TABLE_NAME = "driver";
//    private readonly NpgsqlConnection connection;

//    public DapperDriverAdditionalDbOperations()
//    {
//        connection = new NpgsqlConnection("AppContext");
//        connection.Open();
//    }

//    public async Task CreateTableIfNotExists()
//    {
//        var sql = $"CREATE TABLE if not exists {TABLE_NAME}" +
//                 $"(" +
//                 $"id serial PRIMARY KEY, " +
//                 $"name VARCHAR (200) NOT NULL, " +
//                 $"auto_id BIGINT REFERENCES auto (id), " +
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

#region Old Version Driver
//public class DapperDriverRepository : IDriverRepository
//{

//    private const string TABLE_NAME = "driver";
//    private readonly NpgsqlConnection connection;

//    public DapperDriverRepository()
//    {
//        connection = new NpgsqlConnection("AppContext");
//        connection.Open();
//    }

//    public async Task AddDriver(Driver driver)
//    {
//        string commandText = $"INSERT INTO {TABLE_NAME} (id, name, auto_id) VALUES (@id, @name, @auto_id)";

//        var queryArguments = new
//        {
//            id = driver.DriverId,
//            name = driver.Name,
//            auto_id = driver.AutoId
//        };

//        await connection.ExecuteAsync(commandText, queryArguments);
//    }

//    public async Task DeleteDriver(int id)
//    {
//        string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@p)";

//        var queryArguments = new
//        {
//            p = id
//        };

//        await connection.ExecuteAsync(commandText, queryArguments);
//    }

//    public async Task<Driver> GetDriver(int id)
//    {
//        string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

//        var queryArgs = new { Id = id };
//        var driver = await connection.QueryFirstAsync<Driver>(commandText, queryArgs);
//        return driver;
//    }

//    public async Task<IEnumerable<Driver>> GetDrivers()
//    {
//        string commandText = $"SELECT * FROM {TABLE_NAME}";
//        var driver = await connection.QueryAsync<Driver>(commandText);

//        return driver;
//    }

//    public async Task UpdateDriver(int id, Driver driver)
//    {
//        var commandText = $@"UPDATE {TABLE_NAME}
//                    SET name = @name WHERE id = @id";

//        var queryArgs = new
//        {
//            id = driver.DriverId,
//            name = driver.Name
//        };

//        await connection.ExecuteAsync(commandText, queryArgs);
//    }
//}
#endregion

public class DriverRepository : IDriverRepository
{
    private readonly DapperContext _context;

    public DriverRepository(DapperContext context)
    {
        this._context = context;
    }


    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        var query = "SELECT * FROM driver";

        using (var connection = _context.CreateConnection())
        {
            var drivers = await connection.QueryAsync<Driver>(query);
            return drivers.ToList();
        }
    }


    public async Task<Driver> GetDriver(int? id)
    {
        var query = "SELECT * FROM auto WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            var driver = await connection.QuerySingleOrDefaultAsync<Driver>(query, new { id });
            return driver;
        }
    }

    public async Task CreateDriver(Driver driver)
    {
        var query = "INSERT INTO driver (name) VALUES (@name)";

        var parameters = new DynamicParameters();
        parameters.Add("name", driver.Name, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task UpdateDriver(int id, Driver driver)
    {
        var query = "INSERT INTO driver (name) VALUES (@name WHERE id = @id)";
        var parameters = new DynamicParameters();
        parameters.Add("name", driver.Name, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
    public async Task DeleteDriver(int id)
    {

        var query = "DELETE FROM driver WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { id });
        }
    }

    public async Task<List<Driver>> GetDriversAutosMultipleMapping()
    {
        var query = "SELECT * FROM auto a JOIN driver d ON a.Id = d.auto_id";
        using (var connection = _context.CreateConnection())
        {
            var driverDict = new Dictionary<int, Driver>();
            var drivers = await connection.QueryAsync<Driver, Auto, Driver>(
                query, (driver, auto) =>
                {
                    if (!driverDict.TryGetValue(driver.AutoId, out var currentDriver))
                    {
                        currentDriver = driver;
                        driverDict.Add(currentDriver.Id, currentDriver);
                    }
                    currentDriver.Autos.Add(auto);
                    return currentDriver;
                }
            );
            return drivers.Distinct().ToList();
        }
    }

    //public async Task<Driver> GetDriversAutosMultipleResults(int id)
    //{
    //    var query = "SELECT * FROM driver WHERE id = @id;" +
    //                "SELECT * FROM auto WHERE auto_id = @id";
    //    using (var connection = _context.CreateConnection())
    //    using (var multi = await connection.QueryMultipleAsync(query, new { id }))
    //    {
    //        var driver = await multi.ReadSingleOrDefaultAsync<Driver>();
    //        if (driver != null)
    //            driver.Autos = (await multi.ReadAsync<Auto>()).ToList();
    //        return driver;
    //    }
    //}
}

