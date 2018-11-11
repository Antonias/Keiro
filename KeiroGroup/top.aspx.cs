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

#if Debug 
            return @"Data Source=MYCOMPUTER;"
                    + @"Integrated Security=False;"
                    + @"User ID=sa;"
                    + @"Password=yuto";


#else       
            return @"Data Source=YUTO06;"
                    + @"Integrated Security=False;"
                    + @"User ID=sa;"
                    + @"Password=yuto";
#endif            

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public class clsCommon
    {
        public static string GetNameFromId(int id)
        {
            string name = string.Empty;
            clsDataBase clsdb = new clsDataBase(top.GetConnectionString());
            SqlDataReader reader = clsdb.GetReader(string.Format(
            "select employee_name from [KeiroGroup].[dbo].[TM_Employee] where employee_id = {0}",id));
            while (reader.Read())
            {
                name = reader.GetValue(0).ToString();
            }

            clsdb.closedb();
            return name;
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

