using Microsoft.Extensions.Configuration;
using System.Configuration;
using TestDapper.DbContext;
using TestDapper.Models;
using TestDapper.Repository;

namespace TestDapper.Controllers;
#region Old controller Driver (works)
//public class DriverController : Controller
//{

//    const string connectionString = "User ID=postgres;Password=x569Gm471PSXZ;Host=localhost;Port=5432;Database=testbase;";
//    public IActionResult Index()
//    {
//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string favotireQuery = "SELECT * FROM driver";

//            connection.Open();
//            IEnumerable<Driver> ListDriver = connection.Query<Driver>(favotireQuery).ToList();
//            return View(ListDriver);
//        }
//    }

//    [HttpGet]
//    public IActionResult Create()
//    {

//        return View();
//    }

//    [HttpPost]
//    public IActionResult Create(Driver driver)
//    {
//        if (ModelState.IsValid)
//        {
//            IDbConnection connection;

//            using (connection = new NpgsqlConnection(connectionString))
//            {
//                string insertQuery = "INSERT INTO driver(name) VALUES(@name)";
//                connection = new NpgsqlConnection(connectionString);
//                connection.Open();
//                connection.Execute(insertQuery, driver);
//                connection.Close();
//                return RedirectToAction(nameof(Index));
//            }

//        }
//        return View(driver);
//    }

//    [HttpGet]
//    public IActionResult Edit(int id)
//    {
//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string favoriteQuery = "SELECT * FROM driver WHERE id = @id";
//            connection = new NpgsqlConnection(connectionString);
//            connection.Open();
//            Driver driver = connection.Query<Driver>(favoriteQuery, new { id = id }).FirstOrDefault();
//            connection.Close();
//            return View(driver);
//        }
//    }

//    [HttpPost]
//    public IActionResult Edit(int id, Driver driver)
//    {
//        if (id != driver.Id)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {

//            IDbConnection connection;

//            using (connection = new NpgsqlConnection(connectionString))
//            {
//                string updateQuery = "UPDATE driver SET name=@name WHERE id=@id";
//                connection.Open();
//                connection.Execute(updateQuery, driver);
//                connection.Close();
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        return View(driver);
//    }

//    [HttpPost]

//    public IActionResult Delete(int id)
//    {
//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string deleteQuery = "DELETE FROM driver WHERE id = @id";
//            connection.Open();
//            connection.Execute(deleteQuery, new { id = id });
//            connection.Close();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
#endregion

#region DriverApiController

//public class DriverController : Controller
//{
//    private readonly IDriverRepository _driverRepository;

//    public DriverController(IDriverRepository driverRepository, IAdditionalDbOperations additionalDbOperations)
//    {
//        _driverRepository = driverRepository;

//        additionalDbOperations.CreateTableIfNotExists().GetAwaiter().GetResult();
//        additionalDbOperations.GetVersion().GetAwaiter().GetResult();
//    }

//    // GET: api/<DriverController>
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<Driver>>> GetDriver()
//    {
//        var allDrivers = await _driverRepository.GetDrivers();
//        return Ok(allDrivers);
//    }

//    // GET api/<DriverController>/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<Driver>> GetDriver(int id)
//    {
//        var driver = await _driverRepository.GetDriver(id);
//        if (driver != null)
//            return Ok(driver);
//        else
//            return NotFound();
//    }

//    // POST api/<DriverController>
//    [HttpPost]
//    public async Task<ActionResult> CreateDriver([FromBody] Driver driver)
//    {
//        await _driverRepository.CreateDriver(driver);
//        return Ok();
//    }

//    // PUT api/<DriverController>/5
//    [HttpPut("{id}")]
//    public async Task<ActionResult> UpdateDriver(int id, [FromBody] Driver driver)
//    {
//        await _driverRepository.UpdateDriver(id, driver);
//        return Ok();
//    }

//    // DELETE api/<DriverController>/5
//    [HttpDelete("{id}")]
//    public async Task<ActionResult> DeleteDriver(int id)
//    {
//        await _driverRepository.DeleteDriver(id);
//        return Ok();
//    }
//}

#endregion

#region New controller (almost works)
public class DriverController : Controller
{
    private readonly IDriverRepository _driverRepository;
    private readonly IConfiguration _configuration;

    public DriverController(IDriverRepository driverRepository, IConfiguration configuration)
    {
        this._driverRepository = driverRepository;
        this._configuration = configuration;
    }

    [HttpGet]
    // GET: DriverController
    public async Task<IActionResult> Index()
    {
        try
        {
            var drivers = await _driverRepository.GetDrivers();
            return View(drivers);
        }
        catch (Exception ex)
        {
            // Log error
            return StatusCode(500, ex.Message);
        }


    }

    [HttpGet]
    // GET: DriverController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var drivers = await _driverRepository.GetDriver(id);
        return View(drivers);
    }

    [HttpGet]
    // GET: DriverController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: DriverController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Driver driver)
    {

        if (ModelState.IsValid)
        {
            await _driverRepository.CreateDriver(driver);
            return RedirectToAction(nameof(Index)); 
        }

        return View(driver);
    }

    [HttpGet]
    // GET: DriverController/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View();
    }

    // POST: DriverController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, Driver driver)
    {
        if (id != driver.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _driverRepository.UpdateDriver(id, driver);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // POST: DriverController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _driverRepository.DeleteDriver(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
#endregion
