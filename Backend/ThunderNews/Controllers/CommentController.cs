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

namespace ThunderNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CommentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult Post(Comment comm)
        {
            string query = @"
                              insert into dbo.comment (postId,name,email,comment,isActive) values
                                (
                                    '" + comm.postId + @"',
                                    '" + comm.name + @"',
                                    '" + comm.email + @"',
                                    '" + comm.comment + @"',
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


        [Route("getComments")]
        [HttpPost]
        public JsonResult post(int id)
        {
            string query = @"
                              select * from dbo.comment where isActive=1;
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


        [Route("saveComment")]
        [HttpPost]
        public JsonResult SaveComment(Comment comm)
        {
            string query = @"
                                insert into dbo.comment (postId,name,email,comment,isActive) values
                                   (
                                            '"+comm.postId+ @"',
                                            '" + comm.name + @"',
                                            '" + comm.email + @"',
                                            '" + comm.comment + @"',
                                            0
                                    )
                                ;
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





        [Route("getAllComments")]
        [HttpGet]
        public JsonResult getAllComments()
        {
            string query = @"
                                select * from dbo.comment where isActive=0
                                ;
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








        [Route("approveComment")]
        [HttpPost]
        public JsonResult approveComment(Comment comm)
        {
            string query = @"
                                 update dbo.comment
                                    set isActive = 1
                                    where id='" + comm.id +@"' 
                                ;
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
























        [Route("getApprovedComments")]
        [HttpGet]
        public JsonResult getApprovedComments()
        {
            string query = @"
                                select * from dbo.comment where isActive=1
                                ;
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




        [Route("deactivateComment")]
        [HttpPost]
        public JsonResult deactivateComment(Comment comm)
        {
            string query = @"
                                 update dbo.comment
                                    set isActive = 0
                                    where id='" + comm.id + @"' 
                                ;
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
