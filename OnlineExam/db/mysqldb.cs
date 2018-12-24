using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace OnlineExam.db
{
    public class Mysqldb
    {
        private MySqlConnection conn;
        protected string constr = "Server=46.101.35.132; User Id= root;Password= 123456; Persist Security Info=True;Database=examonline;";
    

        public void Open()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(constr);
                conn.Open();
            }
            else
            {
                if (conn.State.Equals("Closed"))
                {
                    conn.Open();
                }
            }
        }

        public void Close()
        {
            if (conn.State.Equals("Open"))
            {
                conn.Close();
            }
        }

        public void Close(MySqlConnection connection)
        {
            connection.Close();
        }


        public MySqlConnection getConnection()
        {
            try
            {
                conn = new MySqlConnection(constr);
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred when connection trys to connect: '{0}'", e);
            }
            return conn;
        }

        public MySqlCommand getCommand(MySqlConnection conn)
        {
            MySqlCommand sqlcom = conn.CreateCommand();
            return sqlcom;
        }

        public MySqlDataReader ExceSql_getDataReader(string sql)
        {
            Open();
            MySqlCommand sqlcom = new MySqlCommand(sql, conn);
            try
            {
                MySqlDataReader reader = sqlcom.ExecuteReader();
                return reader;
            }
            catch
            {
                return null;

            }
        }

        public MySqlDataReader ExceSql_getDataReader(MySqlCommand cmd, MySqlConnection connection)
        {
            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch
            {
                return null;

            }
        }

        /**
         *  用于执行插入、删除、修改操作
         */
        public Boolean ExceSql(MySqlCommand cmd, MySqlConnection connection)
        {
            try
            {
                connection.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred when connection trys to connect: '{0}'", e);
            }
            return false;
        }


    }
}