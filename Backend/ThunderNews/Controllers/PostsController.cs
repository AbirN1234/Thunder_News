using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ThunderNews.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace ThunderNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PostsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                              select id, title, description, image, category, isActive, postedAt from dbo.posts
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult put(Posts post)
        {
            string query = @"
                              update dbo.Posts set 
                             isActive =  1
                             where id = '" + post.id + @"'
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }

        [Route("disablePost")]

        [HttpPut]
        public JsonResult Put(Posts post)
        {
            string query = @"
                              update dbo.Posts set 
                             isActive =  0
                             where id = '" + post.id + @"'
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }




        [Route("uploadPosts")]

        [HttpPut]
        public JsonResult Post(Posts post)
        {
            string query = @"
                              insert into dbo.Posts (title,description,image,category,isActive) values
                                (
                                    '" + post.title + @"',
                                    '" + post.description + @"',
                                    '" + post.image + @"',
                                    '" + post.category + @"',
                                    0
                                )
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }

        [Route("uploadPhoto")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);

            }
            catch
            {
                return new JsonResult("anonymous.png");
            }
        }


        [Route("getSubscribers")]

        [HttpGet]
        public JsonResult getSubscribers()
        {
            string query = @"
                                select * from dbo.newsLetter; 
                             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }



   


    }
}
