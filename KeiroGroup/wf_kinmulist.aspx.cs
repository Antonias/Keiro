using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;

namespace KeiroGroup
{
    class worker_info
    {

    }
    public partial class wf_kinmulist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (dl_KinmuListJa.Items.Count == 0) { addKinmuListJaToDropList(); }
            if (string.IsNullOrEmpty(this.tb_TargetYear.Text))
            {
                this.tb_TargetYear.Text = DateTime.Now.Year.ToString();                
            }

            if (string.IsNullOrEmpty(this.ddl_TargetMonth.Text))
            {
                this.ddl_TargetMonth.Text = DateTime.Now.Month.ToString();
            }
        }

        private void addKinmuListJaToDropList()
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
            command.CommandText = "select kinmulist_ja from [KeiroGroup].[dbo].[TM_KinmuListCategory]";

            // SQLの実行
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var KJ = reader.GetValue(0);
                this.dl_KinmuListJa.Items.Add(KJ.ToString());


            }

            connection.Close();

        }

        protected void bt_ViewKinmuList_Click(object sender, EventArgs e)
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
            command.CommandText = "select employee_name, employee_id  from [KeiroGroup].[dbo].[TM_Employee] where kinmulist_id = ";
            command.CommandText = command.CommandText + "(select kinmulist_id from [KeiroGroup].[dbo].[TM_KinmuListCategory] ";
            command.CommandText = command.CommandText + "where kinmulist_ja = '" +  this.dl_KinmuListJa.Text.TrimEnd() + "') and employee_flg = 1";

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                addRowToList(reader.GetValue(0).ToString(), reader.GetValue(1).ToString());
            }

            connection.Close();
        }
        
        private void addRowToList(string name, string id)
        {
            HyperLink hl_ev = new HyperLink();
            TableRow tr = new TableRow();
            tbl_KinmuList.Rows.Add(tr);

            // 接続文字列の取得
            var connectionString = KeiroGroup.top.GetConnectionString();

            // データベース接続の準備
            var connection = new SqlConnection(connectionString);

            // データベースの接続開始
            connection.Open();

            // 実行するSQLの準備
            var command = new SqlCommand();
            command.Connection = connection;

            DateTime sd = StartDate();
            DateTime ed = EndDate();
            DateTime td = sd;
            int i = 1;

            TableCell tc = new TableCell();
            tc.Wrap  = false;
            tc.Text = name;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Wrap = false;
            hl_ev.Text = "勤務登録";
            hl_ev.Target = "_blank";
            hl_ev.NavigateUrl = "~/wf_KinmuTodoke.aspx?year=" + tb_TargetYear.Text.ToString() +
                                    "&month=" + ddl_TargetMonth.Text.ToString() +
                                    "&employee_id=" + id;
            tc.Controls.Add(hl_ev);
            tr.Cells.Add(tc);

            while (td != ed)
            {
                
                SqlDataReader reader;

                command.CommandText = "select wt.*, em.*, km.* from [KeiroGroup].[dbo].[T_WorkTime] wt ";
                command.CommandText = command.CommandText + "inner join [KeiroGroup].[dbo].[TM_Employee] em ";
                command.CommandText = command.CommandText + "on wt.employee_id = em.employee_id ";
                command.CommandText = command.CommandText + "inner join [KeiroGroup].[dbo].[TM_KinmuTimeMark] km ";
                command.CommandText = command.CommandText + "ON wt.start_time_yotei = km.start_time and wt.end_time_yotei = km.end_time ";
                command.CommandText = command.CommandText + "and em.kinmulist_id = km.kinmulist_id ";
                command.CommandText = command.CommandText + "where work_dt = '" + td +"' and employee_name = '" + name + "'";

                reader = command.ExecuteReader();

                tc = new TableCell();
                tc.Wrap = false;
                hl_ev = new HyperLink();
                

                if (reader.Read())
                {                                       
                    hl_ev.Text = reader.GetValue(reader.GetOrdinal("kinmulist_mark")).ToString();
                    hl_ev.NavigateUrl = "~/wf_KintaiTodoke.aspx?employee_id=" + reader.GetValue(reader.GetOrdinal("employee_id")).ToString() +
                                                                            "&work_dt=" + reader.GetValue(reader.GetOrdinal("work_dt")).ToString();


                    judgeKinmu cls_jk = new judgeKinmu(reader.GetValue(reader.GetOrdinal("start_time_yotei")).ToString(), 
                                                       reader.GetValue(reader.GetOrdinal("end_time_yotei")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("start_time_zisseki")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("end_time_zisseki")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("start_time_henkou")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("end_time_henkou")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("tikoku_time")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("soutai_time")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("zangyou_time")).ToString(),
                                                       reader.GetValue(reader.GetOrdinal("kekkin_flg")).ToString()
                                                       );
                    tc.BackColor = cls_jk.getJudgeColor();
                }
                else
                {                    
                    hl_ev.Text = "--";                              
                }
                
                hl_ev.Target = "_blank";
                
                tc.Controls.Add(hl_ev);
                tr.Cells.Add(tc);


                reader.Close();

                tr.Cells.Add(tc);
                td = sd.AddDays(i);

                i++;
            }

            connection.Close();
                
        }

        

        private void writeHeaderColumn()
        {
            string year_no = tb_TargetYear.ToString();
            string month_no = ddl_TargetMonth.ToString();

            TableRow tr = new TableRow();
            tbl_KinmuList.Rows.Add(tr);

            DateTime sd = StartDate();
            DateTime ed = EndDate();
            DateTime td = sd;
            int i = 1;

            TableCell tcs = new TableCell();
            tcs.Wrap = false;
            tcs.Text = "氏名";
            tr.Cells.Add(tcs);

            tcs = new TableCell();
            tcs.Wrap = false;
            tcs.Text = "勤務表";
            tr.Cells.Add(tcs);

            while (td != ed)
            {
                TableCell tc = new TableCell();
                tc.Text = td.Day.ToString() + Environment.NewLine + "(" +  td.ToString("ddd") + ")";                
                tr.Cells.Add(tc);
                

                td = sd.AddDays(i);
                i++;
            }

        }

        //集計開始日
        private DateTime StartDate()
        {
            string year_no = tb_TargetYear.Text.ToString();
            string month_no = ddl_TargetMonth.Text.ToString();

            DateTime tmp_dt = new DateTime(int.Parse(year_no), int.Parse(month_no), 1);

            return tmp_dt;
        }

        //集計終了日
        private DateTime EndDate()
        {
            string year_no = tb_TargetYear.Text.ToString();
            string month_no = ddl_TargetMonth.Text.ToString();

            DateTime tmp_dt = new DateTime(int.Parse(year_no), int.Parse(month_no), 1);
            return tmp_dt.AddMonths(1);
            
        }

    }

    class judgeKinmu
    {
        DateTime start_time_yotei;
        DateTime end_time_yotei;
        DateTime start_time_zisseki;
        DateTime end_time_zisseki;
        DateTime start_time_henkou;
        DateTime end_time_henkou;
        DateTime tikoku_time;
        DateTime soutai_time;
        DateTime zangyou_time;
        Boolean kekkin_flg;


        public judgeKinmu(string sty, string ety, string stz, string etz, string sth, string eth, string tikoku_t, 
                          string soutai_t, string zangyou_t, string kekkin_f)
        {
            if (String.IsNullOrEmpty(sty) == false) { start_time_yotei = DateTime.Parse (sty); };
            if (String.IsNullOrEmpty(ety) == false) { end_time_yotei = DateTime.Parse(ety); };
            if (String.IsNullOrEmpty(stz) == false) { start_time_zisseki = DateTime.Parse(stz); };
            if (String.IsNullOrEmpty(etz) == false) { end_time_zisseki = DateTime.Parse(etz); };
            if (String.IsNullOrEmpty(sth) == false) { start_time_henkou = DateTime.Parse(sth); };
            if (String.IsNullOrEmpty(eth) == false) { end_time_henkou = DateTime.Parse(eth); };
            if (String.IsNullOrEmpty(tikoku_t) == false) { tikoku_time = DateTime.Parse(tikoku_t); }
            if (String.IsNullOrEmpty(soutai_t) == false) { soutai_time = DateTime.Parse(soutai_t); }
            if (String.IsNullOrEmpty(zangyou_t) == false) { zangyou_time = DateTime.Parse(zangyou_t); }
            if (String.IsNullOrEmpty(kekkin_f) == false)
            {
                if (kekkin_f == "True")
                {
                    kekkin_flg = true;
                }
                else
                {
                    kekkin_flg = false;
                }
                
            }
            else
            {
                kekkin_flg = false;

            }
           
        }

        public Color getJudgeColor()
        {
            DateTime cal_start_time;
            DateTime cal_end_time;

            cal_start_time = start_time_yotei;
            cal_end_time = end_time_yotei;

            //勤務予定が存在しない
            if (isExistDatetime(start_time_yotei)== false || isExistDatetime(end_time_yotei) == false)
            {
                return Color.White;
            }

            //実績のいずれかが存在しない
            if(isExistDatetime (start_time_zisseki ) == false || isExistDatetime (end_time_zisseki ) == false)
            {
                return Color.White;
            }

            //欠勤届けあり
            if (kekkin_flg)
            {
                cal_start_time = DateTime.Parse("00:00:00");
                cal_end_time = DateTime.Parse("00:00:00");
            }
            
            //勤務変更あり
            if(isExistDatetime (start_time_henkou))
            {
                cal_start_time = start_time_henkou;
            }
            if(isExistDatetime(end_time_henkou))
            {
                cal_end_time = end_time_henkou;
            }

            //遅刻
            if(isExistDatetime(tikoku_time))
            {
                cal_start_time = cal_start_time.AddHours(tikoku_time.Hour);
                cal_start_time = cal_start_time.AddMinutes(tikoku_time.Minute);
            }

            //早退
            if (isExistDatetime(soutai_time))
            {
                cal_end_time = cal_end_time.AddHours(-1 * soutai_time.Hour);
                cal_end_time = cal_end_time.AddMinutes(-1 * soutai_time.Minute);
            }

            //残業
            if (isExistDatetime(zangyou_time))
            {
                cal_end_time = cal_end_time.AddHours(zangyou_time.Hour);
                cal_end_time = cal_end_time.AddMinutes(zangyou_time.Minute);
            }

            
            //判定
            if (cal_start_time == start_time_zisseki && cal_end_time == end_time_zisseki)
            {
                if (cal_start_time == start_time_yotei && cal_end_time == end_time_yotei)
                {
                    return Color.Blue;
                }
                else
                {
                    return Color.Yellow;
                }
            }
            else
            {
                return Color.Red;
            }
            
        }

        private Boolean isExistDatetime(DateTime dt)
        {
            DateTime temp_dt = new DateTime();
            if (dt.CompareTo(temp_dt)==0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }



    }
}