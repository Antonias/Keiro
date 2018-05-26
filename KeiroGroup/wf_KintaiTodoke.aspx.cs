using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace KeiroGroup
{
    public partial class wf_KintaiTodoke : System.Web.UI.Page
    {
        DateTime work_date;
        int employee_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            employee_id = int.Parse ( Request.QueryString["employee_id"]);    //クエリ文字列を参照 
            work_date = DateTime.Parse(Request.QueryString["work_dt"]);

            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select wt.*, em.* from [KeiroGroup].[dbo].[T_WorkTime] wt ";
            command.CommandText = command.CommandText + "inner join [KeiroGroup].[dbo].[TM_Employee] em on wt.employee_id = em.employee_id ";
            command.CommandText = command.CommandText + "where wt.employee_id = " + employee_id;
            command.CommandText = command.CommandText + " and work_dt = '" + work_date + "'";

            
            // SQLの実行
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("出勤予定時間");
            dt.Columns.Add("退勤予定時間");
            dt.Columns.Add("出勤時間");
            dt.Columns.Add("退勤時間");

            while (reader.Read())
            {
                this.lbl_worker_name.Text = reader.GetValue(reader.GetOrdinal("employee_name")).ToString();

                DataRow dr = dt.NewRow();
                dr["出勤予定時間"] = reader.GetValue(reader.GetOrdinal("start_time_yotei"));
                dr["退勤予定時間"] = reader.GetValue(reader.GetOrdinal("end_time_yotei"));
                dr["出勤時間"] = reader.GetValue(reader.GetOrdinal("start_time_zisseki"));
                dr["退勤時間"] = reader.GetValue(reader.GetOrdinal("end_time_zisseki"));
                
                dt.Rows.Add(dr);

                writeKintaiTodoke(reader);
                
            }

            this.gv_TodokedeDetail.DataSource = dt;
            this.gv_TodokedeDetail.DataBind();

            this.La_SinseiDay.Text = "申請日：" + work_date.Year.ToString() + "/" + work_date.Month.ToString() + "/" + work_date.Day.ToString();

            writeSchedule();

            connection.Close();
        }

        private void writeKintaiTodoke(SqlDataReader reader)
        {
            //遅刻
            if (string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("tikoku_time")).ToString()))
            {
                this.La_Tikoku.Visible = false;
                this.La_TikokuInfo.Visible = false;
            }
            else
            {
                this.La_Tikoku.Visible = true;
                this.La_TikokuInfo.Visible = true;
                this.La_TikokuInfo.Text = "時間:" + reader.GetValue(reader.GetOrdinal("tikoku_time")).ToString();
                this.La_TikokuInfo.Text = this.La_TikokuInfo.Text + "  理由:" + reader.GetValue(reader.GetOrdinal("tikoku_reason")).ToString();
            }

            //早退
            if (string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("soutai_time")).ToString()))
            {
                this.La_Soutai.Visible = false;
                this.La_SoutaiInfo.Visible = false;
            }
            else
            {
                this.La_Soutai.Visible = true;
                this.La_SoutaiInfo.Visible = true;
                this.La_SoutaiInfo.Text = "時間:" + reader.GetValue(reader.GetOrdinal("soutai_time")).ToString();
                this.La_SoutaiInfo.Text = this.La_SoutaiInfo.Text +"  理由:" + reader.GetValue(reader.GetOrdinal("soutai_reason")).ToString();
            }

            //変更
            if (string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("start_time_henkou")).ToString()))
            {
                this.La_Henkou.Visible = false;
                this.La_HenkouInfo.Visible = false;
            }
            else
            {
                this.La_Henkou.Visible = true;
                this.La_HenkouInfo.Visible = true;
                this.La_HenkouInfo.Text = "時間:" + reader.GetValue(reader.GetOrdinal("start_time_henkou")).ToString() 
                                          + "～" + reader.GetValue(reader.GetOrdinal("end_time_henkou")).ToString();
                this.La_HenkouInfo.Text = this.La_HenkouInfo.Text + "  理由:" + reader.GetValue(reader.GetOrdinal("henkou_reason")).ToString();
            }

            //欠勤
            if (reader.GetValue(reader.GetOrdinal("kekkin_flg")).ToString() == "True")                
            {
                this.La_Kekkin.Visible = true;
                this.La_KekkinInfo.Visible = true;
                this.La_KekkinInfo.Text = "届出有  " + "理由:" + reader.GetValue(reader.GetOrdinal("kekkin_reason")).ToString();

                if (reader.GetValue(reader.GetOrdinal("yukyu_flg")).ToString() == "True")
                {
                    this.la_Yukyu.Visible = true;
                }
                else
                {
                    this.la_Yukyu.Visible = false;
                }
            }
            else
            {
                this.La_Kekkin.Visible = false;
                this.La_KekkinInfo.Visible = false;
            }

            //残業
            if (string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("zangyou_time")).ToString()))
            {
                this.La_Zangyou.Visible = false;
                this.La_ZangyouInfo.Visible = false;
            }
            else
            {
                this.La_Zangyou.Visible = true;
                this.La_ZangyouInfo.Visible = true;
                this.La_ZangyouInfo.Text = "時間:" + reader.GetValue(reader.GetOrdinal("zangyou_time")).ToString();
                this.La_ZangyouInfo.Text = this.La_ZangyouInfo.Text + "  理由:" + reader.GetValue(reader.GetOrdinal("zangyou_reason")).ToString();
            }

            

        }

        private void writeSchedule()
        {
            string sql_str = "select kensyu_name , start_time_kensyu , end_time_kensyu ";
            sql_str = sql_str + "from[KeiroGroup].[dbo].[T_EmployeeSchedule] ";
            sql_str = sql_str + "where employee_id = " + employee_id;
            sql_str = sql_str + "and day = '" + work_date + "'";

            clsDataBase clsdb = new clsDataBase(KeiroGroup.top.GetConnectionString());
            SqlDataReader reader = clsdb.GetReader(sql_str);

            DataTable dt = new DataTable();
            dt.Columns.Add("研修名");
            dt.Columns.Add("開始時間");
            dt.Columns.Add("終了時間");

            while(reader.Read())
            {
                DataRow dr = dt.NewRow();
                dr["研修名"] = reader.GetValue(reader.GetOrdinal("kensyu_name"));
                dr["開始時間"] = reader.GetValue(reader.GetOrdinal("start_time_kensyu"));
                dr["終了時間"] = reader.GetValue(reader.GetOrdinal("end_time_kensyu"));

                dt.Rows.Add(dr);
            }

            this.GV_ScheduleInfo.DataSource = dt;
            this.GV_ScheduleInfo.DataBind();

            clsdb.closedb();
        }

        protected void Btn_InputTikokuInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string TikokuTime = this.Tb_AdjustHour.Text + ":" + this.Tb_AdjustMinute.Text + ":00";
            string TikokuReason = this.Tb_HenkouRiyuu.Text;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set tikoku_time = '" + TikokuTime + "'";
            SqlString = SqlString + ", tikoku_reason = '" + TikokuReason + "'";
            SqlString = SqlString + "where employee_id = " + employee_id + " and work_dt ='" + work_date + "'"; 

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }

        protected void Btn_InputSoutaiInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string SoutaiTime = this.Tb_AdjustHour.Text + ":" + this.Tb_AdjustMinute.Text + ":00";
            string SoutaiReason = this.Tb_HenkouRiyuu.Text;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set soutai_time = '" + SoutaiTime + "'";
            SqlString = SqlString + ", soutai_reason = '" + SoutaiReason + "'";
            SqlString = SqlString + "where employee_id = " + employee_id + " and work_dt ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }

        protected void Btn_InputZangyouInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string ZangyouTime = this.Tb_AdjustHour.Text + ":" + this.Tb_AdjustMinute.Text + ":00";
            string ZangyouReason = this.Tb_HenkouRiyuu.Text;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set zangyou_time = '" + ZangyouTime + "'";
            SqlString = SqlString + ", zangyou_reason = '" + ZangyouReason + "'";
            SqlString = SqlString + "where employee_id = " + employee_id + " and work_dt ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }

        protected void Btn_InputHenkouInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string start_time = this.Tb_AfterStartHour.Text + ":" + this.Tb_AfterStartMinute.Text + ":00";
            string end_time = this.Tb_AfterEndHour.Text + ":" + this.Tb_AfterEndMinute.Text + ":00";
            string HenkouReason = this.Tb_HenkouRiyuu.Text;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set start_time_henkou = '" + start_time + "'";
            SqlString = SqlString + ", end_time_henkou = '" + end_time + "'";
            SqlString = SqlString + ", henkou_reason = '" + HenkouReason + "'";
            SqlString = SqlString + "where employee_id = " + employee_id + " and work_dt ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }

        protected void Btn_InputKekkinInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string KekkinReason = this.Tb_HenkouRiyuu.Text;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set kekkin_flg = 1 ";
            if (this.cb_Yukyu.Checked == true) { 
                SqlString = SqlString + ", yukyu_flg = 1";
            }
            else
            {
                SqlString = SqlString + ", yukyu_flg = NULL";
            }

            SqlString = SqlString + ", kekkin_reason = '" + KekkinReason + "'";
            SqlString = SqlString + " where employee_id = " + employee_id + " and work_dt ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }

        protected void Btn_ResetKintaiTodoke_Click(object sender, EventArgs e)
        {
            string SqlString;

            SqlString = "update [KeiroGroup].[dbo].[T_WorkTime] set ";
            SqlString = SqlString + "zangyou_time = NULL, zangyou_reason = NULL,";
            SqlString = SqlString + "tikoku_time = NULL , tikoku_reason = NULL,";
            SqlString = SqlString + "soutai_time = NULL , soutai_reason = NULL,";
            SqlString = SqlString + "kekkin_flg = NULL, kekkin_reason = NULL,yukyu_flg = NULL,";
            SqlString = SqlString + "start_time_henkou = NULL, end_time_henkou = NULL, henkou_reason = NULL";
            SqlString = SqlString + " where employee_id = " + employee_id + " and work_dt ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);


            clsConDb.closedb();
        }

        protected void Btn_ResetKensyuInfo_Click(object sender, EventArgs e)
        {
            string SqlString;

            SqlString = "delete from [KeiroGroup].[dbo].[T_EmployeeSchedule] ";        
            SqlString = SqlString + " where employee_id = " + employee_id + " and day ='" + work_date + "'";

            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);


            clsConDb.closedb();
        }

        protected void Btn_InputKensyuInfo_Click(object sender, EventArgs e)
        {
            string SqlString;
            string start_time = this.Tb_KensyuStartHour.Text + ":" + this.Tb_KensyuStartMinute.Text + ":00";
            string end_time = this.Tb_KensyuEndHour.Text + ":" + this.Tb_KensyuEndMinute.Text + ":00";
            string KensyuName = this.Tb_KensyuName.Text;

            SqlString = "Insert into [KeiroGroup].[dbo].[T_EmployeeSchedule] (employee_id , day, start_time_kensyu, ";
            SqlString = SqlString + "end_time_kensyu, kensyu_name , kanzan_flg) ";
            SqlString = SqlString + "values(" + employee_id + ",'" + work_date + "'," + "'" + start_time + "',";
            SqlString = SqlString + "'" + end_time + "'," + "'" + KensyuName + "',"; 

            if (this.cb_KensyuKanzan.Checked == true)
            {
                SqlString = SqlString + "1)";
            }
            else
            {
                SqlString = SqlString + "0)";
            }

          
            ControlDatabase clsConDb = new ControlDatabase(top.GetConnectionString());

            int i = clsConDb.ExecuteSQL(SqlString);
            Response.Redirect(Request.Url.OriginalString);

            clsConDb.closedb();
        }
    }
}