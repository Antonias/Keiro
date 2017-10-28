
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace KeiroGroup
{
    public partial class wf_Schedule : System.Web.UI.Page
    {
        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.tb_TargetYear.Text =="")
            {
                this.tb_TargetYear.Text = DateTime.Now.Year.ToString();
            }

            if (this.ddl_TargetMonth.Text == "")
            {
                this.ddl_TargetMonth.Text = DateTime.Now.Month.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            writeCalender2();

        }

        private void writeCalender2()

        {
            // 接続文字列の取得
            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = getCalenderSql();

            // SQLの実行
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dt = reader.GetValue(0);
                var ev = reader.GetValue(1);
                var id = reader.GetValue(2);
                TableCell tc;

                #region ハイパーリンク設定
                HyperLink hl_ev = new HyperLink();
                hl_ev.Text = ev.ToString();
                hl_ev.NavigateUrl = "~/wf_EventDetail.aspx?event_id=" + id.ToString();
                hl_ev.Target = "_blank";            
                #endregion


                TableRow tr = new TableRow();
                tb_Calender.Rows.Add(tr);

                tc = new TableCell();
                tc.Text = dt.ToString();
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Controls.Add(hl_ev);
                tr.Cells.Add(tc);


            }
        }
        
        private string getCalenderSql()
        {
            string tmp;
            int target_year = int.Parse(tb_TargetYear.Text.ToString());
            int target_month = int.Parse(ddl_TargetMonth.Text.ToString());

            DateTime sd = new DateTime(target_year,target_month,1);
            DateTime ed = sd.AddMonths(1);

            tmp = @"select s.act_dt ";
            tmp = tmp + ", t.task_name , t.id ";
            tmp = tmp + "from [KeiroGroup].[dbo].[T_Schedule] s ";
            tmp = tmp + "inner join [KeiroGroup].[dbo].[TM_TaskName] t ";
            tmp = tmp + "on s.task_id = t.id ";
            tmp = tmp + "where s.act_dt >= '" + sd;
            tmp = tmp + "' and s.act_dt < '" + ed;
            tmp = tmp + "' order by s.act_dt ";


            return tmp;
        }
        
    }
}