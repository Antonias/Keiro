using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace KeiroGroup
{
    public partial class wf_EventDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string event_id = Request.QueryString["event_id"];    //クエリ文字列を参照 


            // 接続文字列の取得
            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = getEventDetailSql(event_id);
          
            // SQLの実行
            SqlDataReader reader = command.ExecuteReader();

            

            TableCell tc;
            while (reader.Read())
            {
                lb_EventTytle.Text = reader.GetValue(0).ToString();
                lb_EventStartDt.Text = reader.GetValue(1).ToString();
                lb_EventEndDt.Text = reader.GetValue(2).ToString();

                TableRow tr = new TableRow();
                tbl_EventDetail.Rows.Add(tr);

                tc = new TableCell();
                tc.Text = reader.GetValue(3).ToString();
                tr.Cells.Add(tc);
            }


        }

        private string getEventDetailSql(string id)
        {
            string tmp;

            tmp = @"select tn.task_name , tn.start_dt , tn.end_dt, em.employee_name ";
            tmp = tmp + "from [KeiroGroup].[dbo].[T_AddMenber] am ";
            tmp = tmp + "inner join [KeiroGroup].[dbo].[TM_TaskName] tn ";
            tmp = tmp + "on tn.id = am.task_id ";
            tmp = tmp + "inner join [KeiroGroup].[dbo].[TM_Employee] em ";
            tmp = tmp + "on am.employee_id = em.employee_id ";
            tmp = tmp + "where am.task_id = " + int.Parse(id);

                

            return tmp;
        }
    }


   
}