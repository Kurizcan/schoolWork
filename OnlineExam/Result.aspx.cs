using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExam
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sn"] != null && Session["username"] != null && Session["score_total"] != null)
            {
                number.Text = Session["sn"].ToString();
                name.Text = Session["username"].ToString();
                score.Text = Session["score_total"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}