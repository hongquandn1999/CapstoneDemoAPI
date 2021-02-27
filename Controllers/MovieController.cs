using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using CapstoneDemo.Models;
using MySql.Data.MySqlClient;

namespace CapstoneDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // HTTP GET
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select MovieId, MovieName from animemovie";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppConMySql");
            
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, myCon))
                {
                    var reader = sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        // HTTP POST
        [HttpPost]
        public JsonResult Post(Movie movie)
        {
            string query = @"insert into AnimeMovie(MovieName) values ('" + movie.MovieName + @"')";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppConMySql");
            
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, myCon))
                {
                    var reader = sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Add Sucessfully !!!");
        }

        // HTTP PUT
        [HttpPut]
        public JsonResult Put(Movie movie)
        {
            string query = @"update AnimeMovie set MovieName = '" + movie.MovieName + @"'
                            where MovieId = " + movie.MovieId + @"";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppConMySql");
            
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, myCon))
                {
                    var reader = sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Sucessfully !!!");
        }

        // HTTP Delete
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from AnimeMovie 
                            where MovieId = " + id + @"";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppConMySql");
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, myCon))
                {
                    var reader = sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Sucessfully !!!");
        }
    }
}
