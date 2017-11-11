using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace KeiroGroup
{
    public partial class top : System.Web.UI.Page
    {
        public static string GetConnectionString()

        {

            
            return @"Data Source=TANAATU-PC\SQLEXPRESS;"
                    + @"Integrated Security=False;"
                    + @"User ID=sa;"
                    + @"Password=yuto";
            

            /*           
            return @"Data Source=YUTO06;"
                    + @"Integrated Security=False;"
                    + @"User ID=sa;"
                    + @"Password=yuto";
            */


        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public class ControlDatabase
    {
        string connectionString;
        SqlConnection connection;
        SqlCommand command;

        public ControlDatabase(string constr)
        {
            connectionString = constr;
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;


        }
        
        public int ExecuteSQL(string sqlstr)
        {
            command.CommandText = sqlstr;
            return command.ExecuteNonQuery();
        }

        public SqlDataReader GetReader(string sqlstr)
        {
            command.CommandText = sqlstr;
            return command.ExecuteReader();
        }

        public void closedb()
        {
            connection.Close();

        }
    }

    
}

