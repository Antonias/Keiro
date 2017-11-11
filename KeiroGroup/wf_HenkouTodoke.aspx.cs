using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace KeiroGroup
{
    public partial class wf_HenkouTodoke : System.Web.UI.Page
    {
        int id;
        int y_code;
        int m_code;

        protected void Page_Load(object sender, EventArgs e)
        {
            y_code = int.Parse(Request.QueryString["year"]);
            m_code = int.Parse(Request.QueryString["month"]);
            id = int.Parse(Request.QueryString["employee_id"]);

            ControlDatabase clsDatabase = new ControlDatabase(top.GetConnectionString());
            
            this.la_employee_name.Text = m_code + "月　届出登録　社員名："+ clsDatabase.GetNameFromId(id);
            this.lbl_ThisMonth.Text = m_code + "月";
        }

        

        

        protected void ddl_TodokeType_TextChanged(object sender, EventArgs e)
        {
            switch (ddl_TodokeType.SelectedValue)
            {
                case "欠勤":
                    this.txt_EndTodokeTime.Visible = false;
                    this.txt_StartTodokeTime.Visible = false;
                    this.txt_TodokeHour.Visible = false;
                    this.lbl_TodokeHour.Visible = false;

                    this.txt_TodokeMinute.Visible = false;
                    this.lbl_TodokeMinute.Visible = false;

                    this.la_Kara.Visible = false;
                    break;

                case "変更":
                    this.txt_EndTodokeTime.Visible = true;
                    this.txt_StartTodokeTime.Visible = true;
                    this.txt_TodokeHour.Visible = false;
                    this.lbl_TodokeHour.Visible = false;

                    this.txt_TodokeMinute.Visible = false;
                    this.lbl_TodokeMinute.Visible = false;

                    this.la_Kara.Visible = true ;
                    break;


                default:
                    this.txt_EndTodokeTime.Visible = false;
                    this.txt_StartTodokeTime.Visible = false;
                    this.txt_TodokeHour.Visible = true;
                    this.lbl_TodokeHour.Visible = true;

                    this.txt_TodokeMinute.Visible = true;
                    this.lbl_TodokeMinute.Visible = true;

                    this.la_Kara.Visible = false;

                    break;



            }
        }
    }


    
}