using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using OnlineExam.db;
using MySql.Data.MySqlClient;



namespace OnlineExam
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["singal"] != null)
            {
                if(Request.QueryString["singal"].ToString() == "Logout")
                {
                    Session.Abandon();
                }
            }
              
        }

        protected void useridValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CustomValidator useridValidator = source as CustomValidator;
            string id = null;
            if (useridValidator.ID == "useridValidator")
                id = userid.Text;
            else if (useridValidator.ID == "userid_signCustomValidator2")
                id = userid_sign.Text;
            if(id != null)
            {
                if (id.Length != 10 || id.Length == 0)
                {
                    args.IsValid = false;       // 验证失败  （长度）
                }
                else
                {
                    Regex reg = new Regex("^[0-9]+$");
                    Match ma = reg.Match(id);
                    if (ma.Success)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;   // 验证失败  非数字
                    }
                }
            }
        }

        protected void passwordValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CustomValidator passwordValidator = source as CustomValidator;
            string pwd = null;
            if (passwordValidator.ID == "passwordValidator")
                pwd = password_sign.Text;
            else if (passwordValidator.ID == "passwordCustomValidator")
                pwd = password.Text;
            if(pwd != null)
            {
                if (pwd.Length > 10 || pwd.Length < 6)
                    args.IsValid = false;       // 验证失败
                else
                {
                    Regex reg = new Regex("^[0-9A-Za-z_]+$");
                    Match ma = reg.Match(pwd);
                    if (ma.Success)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;   // 验证失败  含有非法字符（非数字、大小写字母、下划线）
                    }
                }
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            string sn = userid.Text.Trim();
            string name = username.Text.Trim();
            string pwd = password.Text.Trim();
            string sql = null;
            useridValidator.Validate();
            usernameRequiredFieldValidator3.Validate();
            passwordCustomValidator.Validate();
            if(useridValidator.IsValid && usernameRequiredFieldValidator3.IsValid && passwordCustomValidator.IsValid)
            {
                try
                {
                    sql = "select * from user where sn = @sn and name = @name and password = @pwd";

                    Mysqldb mysqldb = new Mysqldb();
                    MySqlConnection connection = mysqldb.getConnection();
                    MySqlCommand cmd = mysqldb.getCommand(connection);
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@sn", sn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    MySqlDataReader reader = mysqldb.ExceSql_getDataReader(cmd, connection);
                    if(reader.HasRows)
                    {
                        //code here if the login is valid
                        mysqldb.Close();
                        // 将学生姓名、学号存储在 session 中
                        Session["sn"] = sn;
                        Session["username"] = name;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        login_result.Text = "<a class='error'>学号、姓名、密码错误或者不存在</a>";
                    }
                }
                catch (Exception error)
                {
                    //login_result.Text = "<a class='error'>服务器繁忙，请稍后重试</a>";
                    login_result.Text = error.ToString();
                    Console.WriteLine("An error occurred when connection trys to connect: '{0}'", error);
                }

            }
        }

        protected void signup_Click(object sender, EventArgs e)
        {
            string sql = null;
            string userid = userid_sign.Text.Trim();
            string email = email_sign.Text.Trim();
            string username = username_sign.Text.Trim();
            string pwd = password_sign.Text.Trim();
            userid_signCustomValidator2.Validate();
            username_signRequiredFieldValidator1.Validate();
            email_signValidator1.Validate();
            passwordValidator.Validate();
            PasswordCompareValidator.Validate();
            if(userid_signCustomValidator2.IsValid && username_signRequiredFieldValidator1.IsValid && email_signValidator1.IsValid && passwordValidator.IsValid && PasswordCompareValidator.IsValid)
            {
                try
                {
                    sql = "insert into user (sn, name, password, email) values (@userid, @username, @pwd, @email)";
                    Mysqldb mysqldb = new Mysqldb();
                    MySqlConnection connection = mysqldb.getConnection();
                    MySqlCommand cmd = mysqldb.getCommand(connection);
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.Parameters.AddWithValue("@email", email);
                    if (mysqldb.ExceSql(cmd, connection))
                    {
                        //成功插入，注册成功，重新登陆
                        mysqldb.Close();
                        this.RegisterStartupScript("hello", "<script>alert('注册成功，请登录!')</script>");
                    }
                    else
                    {
                        login_result.Text = "<a class='error'>注册失败，请稍后重试</a>";
                    }
                }
                catch (Exception error)
                {
                    //login_result.Text = "<a class='error'>服务器繁忙，请稍后重试</a>";
                    login_result.Text = error.ToString();
                    Console.WriteLine("An error occurred when connection trys to connect: '{0}'", error);
                }
            }
        }
    }
}

