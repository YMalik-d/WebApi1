using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace WebApi3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userDataController : ControllerBase
    {
        public static void createInitialTable()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;"))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Create Table webAppData ( guid int NULL, lastModified datetime2(7) NOT NULL, userData nvarchar(max) NULL )";
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [HttpGet]
        [ActionName("GetUserData")]
        public WebAppData Get()
        {  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from webAppData";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            WebAppData dat = null;
            while (reader.Read())
            {
                dat = new WebAppData();
                dat.Guid = Convert.ToInt32(reader.GetValue(0));
                dat.UserData = reader.GetString(0);
            }
            return dat;

        }

        [HttpPut]
        [ActionName("UpdateUserData")]
        public void ChangeWebAppData(WebAppData webAppData)
        {  
            string userDataInput = "";
            string requestedGuid = "";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE webAppData SET lastModified = " + DateTime.Now + ", userData = " + userDataInput + " WHERE guid =" + requestedGuid;
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }


    }
}
