using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExam.Templates
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null)
            {
                SingOrLogout.HRef = "../Login.aspx";
                spanTosay.InnerText = "LogIn/SignUp";
            }
            else
            {
                SingOrLogout.HRef = "../Login.aspx?singal=Logout";
                spanTosay.InnerText = "Logout";
            }
        }
    }
}