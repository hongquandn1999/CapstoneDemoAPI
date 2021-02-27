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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CapstoneDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        // env file
        private readonly IWebHostEnvironment _env;
        public CharacterController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        // HTTP GET
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select CharacterId, CharacterName, MovieName, 
                             convert(varchar(10), DateOfRelease, 120) as DateOfRelease, 
                             PhotoFileName from dbo.AnimeCharacter";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        // HTTP POST
        [HttpPost]
        public JsonResult Post(Character character)
        {
            string query = @"insert into dbo.AnimeCharacter 
                            (CharacterName,MovieName,DateOfRelease,PhotoFileName)
                            values 
                            (
                                '" + character.CharacterName + @"'
                                ,'" + character.MovieName + @"'
                                ,'" + character.DateOfRelease + @"'
                                ,'" + character.PhotoFileName + @"'
                            )
                            ";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Add Sucessfully !!!");
        }

        // HTTP PUT
        [HttpPut]
        public JsonResult Put(Character character)
        {
            string query = @"update dbo.AnimeCharacter set 
                            CharacterName = '" + character.CharacterName + @"'
                            ,MovieName = '" + character.MovieName + @"'
                            ,DateOfRelease = '" + character.DateOfRelease + @"'
                            ,PhotoFileName = '" + character.PhotoFileName + @"'
                            where CharacterId = " + character.CharacterId + @"";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Sucessfully !!!");
        }

        // HTTP Delete
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.AnimeCharacter 
                            where CharacterId = " + id + @"";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AnimeAppCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Sucessfully !!!");
        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("sinon.png");
            }
        }

        //[Route("GetAllMovieNames")]
        //public JsonResult GetAllMovieNames()
        //{
        //    string query = @"select MovieName from dbo.AnimeMovie";
        //    DataTable dataTable = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("AnimeAppCon");
        //    SqlDataReader sqlDataReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
        //        {
        //            sqlDataReader = sqlCommand.ExecuteReader();
        //            dataTable.Load(sqlDataReader);
        //            sqlDataReader.Close();
        //            myCon.Close();
        //        }
        //    }
        //    return new JsonResult(dataTable);
        //}
    }
}
