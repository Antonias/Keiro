using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace KeiroGroup.Kintai
{
    public partial class wf_KinmuTodoke : System.Web.UI.Page
    {
        int y_code;
        int m_code;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {

            y_code = int.Parse(Request.QueryString["year"]);
            m_code = int.Parse(Request.QueryString["month"]);
            id = int.Parse(Request.QueryString["employee_id"].ToString());
            
            InitKinmuList("start_time_zisseki", "end_time_zisseki");
            
        }

        protected void Cmd_inputYotei_Click(object sender, EventArgs e)
        {

            ControlDatabase ClsDatabase = new ControlDatabase(top.GetConnectionString());
            string sqlstr = "select work_dt, start_time_yotei, end_time_yotei ";
            sqlstr = sqlstr + "from[KeiroGroup].[dbo].[T_WorkTime] ";
            sqlstr = sqlstr + "where employee_id =" + id  ;
            sqlstr = sqlstr + " and work_dt between '" + StartDate() + "'and '" + EndDate() + "'";
            
            SqlDataReader reader = ClsDatabase.GetReader(sqlstr);

            while(reader.Read())
            {
                string start_time = reader.GetValue(1).ToString();
                string end_time = reader.GetValue(2).ToString();
                DateTime work_dt = DateTime.Parse( reader.GetValue(0).ToString());
          
                int y_axis;
                int x_axis;

                if(work_dt.Day < 16)
                {
                    x_axis = 3;
                    y_axis = work_dt.Day;
                }
                else
                {
                    x_axis = 9;
                    y_axis = work_dt.Day - 15;

                }

                TableRow tr = this.tbl_KinmuList.Rows[y_axis];

                CheckBox cb = (CheckBox)tr.Cells[x_axis - 3].Controls[0];
                if (cb.Checked)
                {

                    TextBox tb = (TextBox)tr.Cells[x_axis].Controls[0];
                    if (string.IsNullOrEmpty(tb.Text)) { tb.Text = start_time; }

                    tb = (TextBox)tr.Cells[x_axis + 1].Controls[0];
                    if (string.IsNullOrEmpty(tb.Text)) { tb.Text = end_time; }
                }
            }

            ClsDatabase.closedb();
        }

        private void InitKinmuList(string st_code, string ed_code)
        {
            writeHeaderColumn();       

            // 接続文字列の取得
            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;

            string start_time;
            string end_time;
            TextBox tb = new TextBox();
            for (int d = 1; d <= 16; d++)
            {
                DateTime td = new DateTime(y_code, m_code, d);
                TableRow tr = new TableRow();
                this.tbl_KinmuList.Rows.Add(tr);
                TableCell tc = new TableCell();
                CheckBox cb = new CheckBox();

         
                tc.Controls.Add(cb);
                tr.Cells.Add(tc);
                               
                tc = new TableCell();
                if (d != 16) { tc.Text = d.ToString(); }
                tr.Cells.Add(tc);

                tc = new TableCell();
                if (d != 16) { tc.Text = GetYoubiCode(td); }
                tr.Cells.Add(tc);

                //ここタプル候補
                
                command.CommandText = "select wt." + st_code +  ", wt." + ed_code ;
                command.CommandText = command.CommandText + " from[KeiroGroup].[dbo].[T_WorkTime] wt ";
                command.CommandText = command.CommandText + "where employee_id = " + id +  "and wt.work_dt = '" + td + "'";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    start_time = reader.GetValue(0).ToString();                    
                    end_time = reader.GetValue(1).ToString();
                    
                }
                else
                {
                    start_time = "";
                    end_time = ""; 

                }
            

                tb = new TextBox();
                if (d != 16) { tb.Text = start_time; }
                tc = new TableCell();
                tc.Controls.Add(tb);
                tr.Cells.Add(tc);

                tb = new TextBox();
                if (d != 16) { tb.Text = end_time; }
                tc = new TableCell();
                tc.Controls.Add(tb);
                tr.Cells.Add(tc);

                reader.Close();

                //int i = EndDateNo();
                if (d + 15 <= EndDate().Day)
                {
                    
                    tc = new TableCell();
                    tc.Text = "";
                    tr.Cells.Add(tc);

                    cb = new CheckBox();
                    tc = new TableCell();
                    tc.Controls.Add(cb);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = (d+15).ToString();
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    tc.Text = GetYoubiCode(td.AddDays(15));
                    tr.Cells.Add(tc);

                    command.CommandText = "select wt." + st_code + ", wt." + ed_code ;
                    command.CommandText = command.CommandText + " from[KeiroGroup].[dbo].[T_WorkTime] wt ";
                    command.CommandText = command.CommandText + "where employee_id =" + id + " and wt.work_dt = '" + td.AddDays(15) + "'";
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        start_time =  reader.GetValue(0).ToString();
                        end_time = reader.GetValue(1).ToString();                        
                    }
                    else
                    {
                        start_time = "";
                        end_time = "";
                    }

                    tb = new TextBox();
                    tb.Text = start_time;
                    tc = new TableCell();
                    tc.Controls.Add(tb);
                    tr.Cells.Add(tc);

                    tb = new TextBox();
                    tb.Text = end_time;
                    tc = new TableCell();
                    tc.Controls.Add(tb);
                    tr.Cells.Add(tc);
                }


                reader.Close();
            }
            TableRow trl = new TableRow();

            connection.Close();
        }

        private string GetYoubiCode(DateTime dt)
        {
            DayOfWeek dow = dt.DayOfWeek;

            switch (dow)
            {
                case DayOfWeek.Sunday:
                    return "日";
                case DayOfWeek.Monday:
                    return "月";
                case DayOfWeek.Tuesday:
                    return "火";
                case DayOfWeek.Wednesday:
                    return "水";
                case DayOfWeek.Thursday:
                    return "木";
                case DayOfWeek.Friday:
                    return "金";
                case DayOfWeek.Saturday:
                    return "土";
                default:
                    return "";

            }
        }

            private void writeHeaderColumn()
        {
            TableRow tr = new TableRow();
            tbl_KinmuList.Rows.Add(tr);
            

            TableCell tc = new TableCell();
            tc.Text = "　";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "日付";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "曜日";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "開始日時";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "終了日時";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "--------------";
            tc.ForeColor = System.Drawing.Color.White;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "　";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "日付";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "曜日";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "開始日時";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "終了日時";
            tr.Cells.Add(tc);
        }

        private DateTime StartDate()
        {            
            DateTime tmp_dt = new DateTime(y_code, m_code, 1);
            return tmp_dt;
        }

        

        private DateTime EndDate()
        {           
            DateTime tmp_dt = new DateTime(y_code, m_code, 1);
            return tmp_dt.AddMonths(1).AddDays(-1);
        }

        protected void Cmd_updateZisseki_Click(object sender, EventArgs e)
        {
            //ここlinq候補
            TableRowCollection trc = this.tbl_KinmuList.Rows;
            for(int row_no = 1; row_no<=16;row_no ++)
            {
                TextBox tb_start_time;
                TextBox tb_end_time;
                int day;
                if (!string.IsNullOrEmpty(trc[row_no].Cells[1].Text.ToString()))
                {
                    day = int.Parse(trc[row_no].Cells[1].Text.ToString());
                    tb_start_time = (TextBox)trc[row_no].Cells[3].Controls[0];
                    tb_end_time = (TextBox)trc[row_no].Cells[4].Controls[0];
                    updateKinmuTodoke(day, tb_start_time.Text, tb_end_time.Text);
                }

                if (!string.IsNullOrEmpty(trc[row_no].Cells[7].Text.ToString()))
                {
                    day = int.Parse(trc[row_no].Cells[7].Text.ToString());
                    tb_start_time = (TextBox)trc[row_no].Cells[9].Controls[0];
                    tb_end_time = (TextBox)trc[row_no].Cells[10].Controls[0];
                    updateKinmuTodoke(day, tb_start_time.Text, tb_end_time.Text);
                    
                }

                
            }
            
            
        }

        private void updateKinmuTodoke(int dt, string st, string et)
        {
            DateTime work_dt = new DateTime(y_code, m_code, dt);
            // 接続文字列の取得
            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "update [KeiroGroup].[dbo].[T_WorkTime] ";
            if (string.IsNullOrEmpty(st))
            {
                command.CommandText = command.CommandText + "set start_time_zisseki =  NULL,";
            }
            else
            {
                command.CommandText = command.CommandText + "set start_time_zisseki = '" + st + "',";
            }

            if (string.IsNullOrEmpty(et))
            {
                command.CommandText = command.CommandText + "end_time_zisseki =  NULL ";
            }
            else
            {
                command.CommandText = command.CommandText + "end_time_zisseki = '" + et + "' ";
            }

            command.CommandText = command.CommandText + "where employee_id = " + id + " and work_dt = '" + work_dt + "'";
            


            int i = command.ExecuteNonQuery();

            connection.Close();
        }

        protected void Cmd_checkAllYotei_Click(object sender, EventArgs e)
        {
            TableRowCollection trc = this.tbl_KinmuList.Rows;
            for (int i = 1; i <= 16; i++)
            {
                CheckBox cb = (CheckBox)trc[i].Cells[0].Controls[0];
                cb.Checked = true;

                cb = (CheckBox)trc[i].Cells[6].Controls[0];
                cb.Checked = true;
            }


        }
    }
}