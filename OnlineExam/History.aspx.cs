using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using OnlineExam.db;

namespace OnlineExam
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string name = Session["username"].ToString();
            //string sn = Session["sn"].ToString();
            if (Session["sn"] == null && Session["username"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                GetHistory();
            }
            
        }

        public void GetHistory()
        {
            string userId = Session["sn"].ToString();
            string sql = "select id, examId, score, datetime from exam_record where userId = @userId";
            Mysqldb mysqldb = new Mysqldb();
            MySqlConnection connection = mysqldb.getConnection();
            MySqlCommand cmd = mysqldb.getCommand(connection);
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userId", userId);
            MySqlDataAdapter da = new MySqlDataAdapter();
            connection.Open();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds, "history");
            GridView_Exam_History.DataSource = ds;
            GridView_Exam_History.DataBind();
            connection.Close();
        }
    
        protected void GridView_Exam_History_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Exam_History.PageIndex = e.NewPageIndex;
            GridView_Exam_History.DataBind();
        }
    }
}