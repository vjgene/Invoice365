using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Invoice365
{
    class MYSQLDB   : SQLDB
    {
        MySqlConnection conn = null;
        bool lcl = false;

        public MYSQLDB(bool lcl)
        {
            this.lcl = lcl;
        }

        public MYSQLDB()
        {
            this.lcl = false;
        }

        public void close()
        {
            conn.Close();
        }

        public void connect()
        {
            string MyConString;
            if(lcl)
                MyConString = "SERVER=localhost;" + "DATABASE=Balance;" + "UID=root;" + "PASSWORD=;";
            else
            MyConString = "SERVER=mysql.netfirms.com;" + "DATABASE=d60648171;" + "UID=u70715654;" + "PASSWORD=620f98;";
           conn = new MySqlConnection(MyConString);
           conn.Open();             
        }

        public int ExecuteNonQuery(string sql)
        {            
            connect();           
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;            
            int res = cmd.ExecuteNonQuery();
            close();
            return res;
        }

        public DbDataReader ExecuteQuery(string sql)
        {        
            connect();            
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
    }
}