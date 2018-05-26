using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace KeiroGroup
{
    public partial class EventCalendar : System.Web.UI.Page
    {
        int y_code;
        int m_code;

        protected void Page_Load(object sender, EventArgs e)
        {
            y_code = int.Parse(Request.QueryString["year"]);
            m_code = int.Parse(Request.QueryString["month"]);

            this.CLD_EventCalendar.VisibleDate = new DateTime(y_code, m_code, 1);
        }

        protected void CLD_EventCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            clsDataBase clsdb = new clsDataBase(KeiroGroup.top.GetConnectionString());

            string str_sql = "select e.employee_name , es.kensyu_name , es.start_time_kensyu , es.end_time_kensyu , kanzan_flg ";
            str_sql = str_sql + "from[KeiroGroup].[dbo].[T_EmployeeSchedule] es ";
            str_sql = str_sql + "inner join[KeiroGroup].[dbo].[TM_Employee] e ";
            str_sql = str_sql + "on es.employee_id = e.employee_id ";
            str_sql = str_sql + "where es.day = '" + e.Day.Date.ToString() + " 'and es.start_time_kensyu is not null ";

            SqlDataReader reader = clsdb.GetReader(str_sql);

            string cal_info = string.Empty;
            while (reader.Read())
            {
                if (reader.GetValue(4).ToString() == "True")
                {
                    cal_info = cal_info + "<br />[換]";
                }
                else
                {
                    cal_info = cal_info + "<br />";
                }

                
                cal_info = cal_info + reader.GetValue(0).ToString() + 
                "<br />" + reader.GetValue(1).ToString() +
                "<br />"  + reader.GetValue(2).ToString().Substring(0,5) + "～" + reader.GetValue(3).ToString().Substring(0,5);
                
                //if(reader.GetValue(4).ToString = )

            }

            e.Cell.Controls.Add(new LiteralControl(cal_info));



            clsdb.closedb();
        }
    }
}