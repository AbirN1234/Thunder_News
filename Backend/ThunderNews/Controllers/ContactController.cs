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
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public JsonResult Post(Contact comm)
        {
            string query = @"
                              insert into dbo.contact (name,email,phone,comment) values
                                (
                                    '" + comm.name + @"',
                                    '" + comm.email + @"',
                                    '" + comm.phone + @"',
                                    '" + comm.comment + @"'
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





        [Route("getContacts")]
        [HttpGet]
        public JsonResult getContacts()
        {
            string query = @"
                              select * from dbo.contact
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
