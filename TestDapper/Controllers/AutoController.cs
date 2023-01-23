using TestDapper.DbContext;
using TestDapper.Models;
using TestDapper.Repository;

namespace TestDapper.Controllers;

#region Old Controller Auto (works)
//public class AutoController : Controller
//{

//    const string connectionString = "User ID=postgres;Password=admin!@#;Host=localhost;Port=5432;Database=testbase;";
//    public IActionResult Index()
//    {
//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string favotireQuery = "SELECT * FROM auto";
//            connection.Open();
//            IEnumerable<Auto> ListCars = connection.Query<Auto>(favotireQuery).ToList();
//            return View(ListCars);
//        }

//    }

//    [HttpGet]
//    public IActionResult Create()
//    {

//        return View();
//    }

//    [HttpPost]
//    public IActionResult Create(Auto auto)
//    {
//        if (ModelState.IsValid)
//        {
//            IDbConnection connection;

//            using (connection = new NpgsqlConnection(connectionString))
//            {
//                string insertQuery = "INSERT INTO auto(brand,model) VALUES(@brand, @model)";

//                connection.Open();
//                connection.Execute(insertQuery, auto);
//                connection.Close();
//                return RedirectToAction(nameof(Index));
//            }

//        }
//        return View(auto);
//    }

//    [HttpGet]
//    public IActionResult Edit(int? id)
//    {

//        if (id == null)
//        {
//            return NotFound();
//        }

//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string favoriteQuery = "SELECT * FROM auto WHERE id = @id";
//            connection.Open();
//            Auto auto = connection.Query<Auto>(favoriteQuery, new { id = id }).FirstOrDefault();
//            connection.Close();
//            return View(auto);
//        }
//    }

//    [HttpPost]
//    public IActionResult Edit(int id, Auto auto)
//    {
//        if (id != auto.Id)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {

//            IDbConnection connection;

//            using (connection = new NpgsqlConnection(connectionString))
//            {
//                string updateQuery = "UPDATE auto SET brand=@brand, model=@model WHERE id=@id";
//                connection.Open();
//                connection.Execute(updateQuery, auto);
//                connection.Close();
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        return View(auto);
//    }

//    [HttpPost]
//    public IActionResult Delete(int id)
//    {
//        IDbConnection connection;

//        using (connection = new NpgsqlConnection(connectionString))
//        {
//            string deleteQuery = "DELETE FROM auto WHERE id=@id";
//            connection = new NpgsqlConnection();
//            connection.Open();
//            connection.Execute(deleteQuery, new { id = id });
//            connection.Close();
//            return RedirectToAction(nameof(Index));
//        }

//    }
//}
#endregion

#region Api controller

//public class AutoController : Controller
//{
//    private readonly IAutoRepository _autoRepository;

//    public AutoController(IAutoRepository autoRepository, IAdditionalDbOperations additionalDbOperations)
//    {
//        _autoRepository = autoRepository;

//        additionalDbOperations.CreateTableIfNotExists().GetAwaiter().GetResult();
//        additionalDbOperations.GetVersion().GetAwaiter().GetResult();
//    }

//    // GET: api/<AutoController>
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<Auto>>> Get()
//    {
//        var allAuto = await _autoRepository.GetCars();
//        return Ok(allAuto);
//    }

//    // GET api/<AutoController>/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<Auto>> GetCar(int id)
//    {
//        var auto = await _autoRepository.GetCar(id);
//        if (auto != null)
//            return Ok(auto);
//        else
//            return NotFound();
//    }

//    // POST api/<AutoController>
//    [HttpPost]
//    public async Task<ActionResult> CreateCar([FromBody] Auto auto)
//    {
//        await _autoRepository.CreateCar(auto);
//        return Ok();
//    }

//    // PUT api/<AutoController>/5
//    [HttpPut("{id}")]
//    public async Task<ActionResult> UpdateCar(int id, [FromBody] Auto auto)
//    {
//        await _autoRepository.UpdateCar(id, auto);
//        return Ok();
//    }

//    // DELETE api/<AutoController>/5
//    [HttpDelete("{id}")]
//    public async Task<ActionResult> DeleteCar(int id)
//    {
//        await _autoRepository.DeleteCar(id);
//        return Ok();
//    }
//}
#endregion

#region New Controller (works)
public class AutoController : Controller
{
    private readonly IAutoRepository _autoRepository;

    public AutoController(IAutoRepository autoRepository)
    {
        this._autoRepository = autoRepository;
    }

    [HttpGet]
    // GET: AutoController
    public async Task<IActionResult> Index()
    {
        try
        {
            var cars = await _autoRepository.GetCars();
            return View(cars);
        }
        catch (Exception ex)
        {
            // Log error
            return StatusCode(500, ex.Message);
        }


    }

    [HttpGet]
    // GET: AutoController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: AutoController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Auto auto)
    {
        if (ModelState.IsValid)
        {
            await _autoRepository.CreateCar(auto);
            return RedirectToAction(nameof(Index));
        }

        return View(auto);
    }

    [HttpGet]
    // GET: AutoController/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null)
        {
          return NotFound();
        }

        var car = await _autoRepository.GetCar(id);
        return View(car);
    }

    // POST: AutoController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, Auto auto)
    {
        if (id != auto.Id)
        {
           return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _autoRepository.UpdateCar(id, auto);
            return RedirectToAction(nameof(Index));
        }
        return View();
        
    }

    // POST: AutoController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _autoRepository.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

#endregion