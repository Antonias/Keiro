using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace KeiroGroup
{
    public class clsDataBase
    {       
        SqlConnection connection;
        SqlCommand command;
        


        public clsDataBase(string GetConnectionString)
        {
            
            connection = new SqlConnection(GetConnectionString);
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