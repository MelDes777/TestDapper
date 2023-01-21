using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using TestDapper.Models;

namespace TestDapper.Controllers
{
    public class AutoController : Controller
    {
        private readonly string ConnectionString = "User ID=postgres;" +
            "Password=x569Gm471PSXZ;Host=localhost;Port=5432;Database=testbase;";

        public IActionResult Index()
        {
            IDbConnection connection;

            try
            {
                string favotireQuery = "SELECT * FROM auto";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                IEnumerable<Auto> ListCars = connection.Query<Auto>(favotireQuery).ToList();
                return View(ListCars);
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
        public IActionResult Create(Auto auto)
        {
            if (ModelState.IsValid)
            {
                IDbConnection connection;

                try
                {
                    string insertQuery = "INSERT INTO auto(brand,model) VALUES(@brand, @model)";
                    connection = new NpgsqlConnection(ConnectionString);
                    connection.Open();
                    connection.Execute(insertQuery, auto);
                    connection.Close();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return View(auto);
        }

        [HttpGet]
        public IActionResult Edit(int autoId)
        {
            IDbConnection connection;

            try
            {
                string favoriteQuery = "SELECT * FROM auto WHERE AutoId = @auto_Id";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                Auto auto = connection.Query<Auto>(favoriteQuery, new { autoId = autoId}).FirstOrDefault();
                connection.Close();
                return View(auto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(int autoId, Auto auto)
        {
            if (autoId != auto.AutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                IDbConnection connection;

                try
                {
                    connection= new NpgsqlConnection(ConnectionString);
                    string updateQuery = "UPDATE auto SET Brand=@brand, Model=@model WHERE AutoId=@auto_id";
                    connection.Open();
                    connection.Execute(updateQuery, auto);
                    connection.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View(auto);
        }

        [HttpPost]

        public IActionResult Delete(int autoId)
        {
            IDbConnection connection;

            try
            {
                string deleteQuery = "DELETE FROM auto WHERE AutoId= @auto_id";
                connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                connection.Execute(deleteQuery, new { autoId = autoId });
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
