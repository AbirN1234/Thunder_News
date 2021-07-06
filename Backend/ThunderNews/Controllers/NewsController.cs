using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThunderNews.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace ThunderNews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }





        [Route("postNewsLetter")]

        [HttpPost]
        public JsonResult postNewsLetter(News post)
        {
            string query = @"
                              insert into dbo.newsLetter (email) values
                                (
                                    '" + post.email + @"'
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





        [Route("sendNewsMail")]

        [HttpPost]
        public int sendNewsMail(News em)
        {
            string To = em.email;
            string Subject = "THUNDER NEWS: New News Added";
            string Body = "Have a look at our site to get latest news updates.";
            MailMessage mm = new MailMessage();
            mm.To.Add(To);
            mm.Subject = Subject;
            mm.Body = Body;
            mm.From = new MailAddress("gmitcse1@gmail.com");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("gmitcse1@gmail.com", "gmit12345");
            smtp.Send(mm);
            return 0;

        }


    }
}
