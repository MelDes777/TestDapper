using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using TestDapper.Models;

namespace TestDapper.Controllers
{
    public class DriverController : Controller
    {
        private readonly string ConnectionString = "User ID=postgres;" +
            "Password=x569Gm471PSXZ;Host=localhost;Port=5432;Database=testbase;";

        public IActionResult Index()
        {
            IDbConnection connection;

            try
            {
                string favotireQuery = "SELECT * FROM driver";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                IEnumerable<Driver> ListDriver = connection.Query<Driver>(favotireQuery).ToList();
                return View(ListDriver);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Driver driver)
        {
            if (ModelState.IsValid)
            {
                IDbConnection connection;

                try
                {
                    string insertQuery = "INSERT INTO driver(name) VALUES(@name)";
                    connection = new NpgsqlConnection(ConnectionString);
                    connection.Open();
                    connection.Execute(insertQuery, driver);
                    connection.Close();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return View(driver);
        }

        [HttpGet]
        public IActionResult Edit(int driverId)
        {
            IDbConnection connection;

            try
            {
                string favoriteQuery = "SELECT * FROM driver WHERE DriverId = @driver_Id";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                Driver driver = connection.Query<Driver>(favoriteQuery, new { driverId = driverId }).FirstOrDefault();
                connection.Close();
                return View(driver);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int driverId, Driver driver)
        {
            if (driverId != driver.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                IDbConnection connection;

                try
                {
                    connection = new NpgsqlConnection(ConnectionString);
                    string updateQuery = "UPDATE driver SET Name=@name WHERE DriverId=@driver_Id";
                    connection.Open();
                    connection.Execute(updateQuery, driver);
                    connection.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View(driver);
        }

        [HttpPost]

        public IActionResult Delete(int driverId)
        {
            IDbConnection connection;

            try
            {
                string deleteQuery = "DELETE FROM driver WHERE DriverId= @driver_id";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                connection.Execute(deleteQuery, new { driverId = driverId });
                connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

