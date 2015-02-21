using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Windows;


namespace Invoice365
{
    class MSSQLDB : SQLDB
    {
        SqlCeConnection conn = null;
        string connstr = "Datasource="+Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Invoice365.sdf;password=";

        public MSSQLDB()
        {
            SqlCeEngine en = null;
            
            try
            {
                en = new SqlCeEngine(connstr);
                //en.CreateDatabase();          
                //createTables();
            }

            catch (SqlCeException e)
            {
                //MessageBox.Show(e.GetBaseException().StackTrace);
                //MessageBox.Show(e.GetBaseException().Message);                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetBaseException().Message + " " + e.GetBaseException().StackTrace);                
            }

        }

        

        public void close()
        {
            conn.Close();
        }

        public void connect()
        {            
            conn = new SqlCeConnection(connstr);            
            conn.Open();
        }

        public int ExecuteNonQuery(string sql)
        {
            connect();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            int res = cmd.ExecuteNonQuery();
            close();
            return res;
        }

        public DbDataReader ExecuteQuery(string sql)
        {
            connect();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            SqlCeDataReader rdr = cmd.ExecuteReader();
            
            return rdr;
        }
   
     
    }
}