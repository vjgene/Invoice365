using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data.Common;
using System.Collections;


namespace Invoice365
{
    class DB
    {
        public static int MYSQL = 0;
        public static int MSSQL = 1;
        public static int LCL_MYSQL = 2;
        
        private SQLDB sqldb;
        private static DB db;
        
        

        private DB(int type)    {
            if (type == MSSQL)
            {
                try
                {
                    sqldb = new MSSQLDB();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.GetBaseException().Message + " " + e.GetBaseException().StackTrace);
                }
            }
            else if (type == LCL_MYSQL) sqldb = new MYSQLDB(true);
            else sqldb = new MYSQLDB();
        }

        public static DB getInstance()
        {
            if (db == null) db = new DB(MYSQL);
            return db;
        }

        public String getUpdateString(Hashtable atts, string table, string where)
        {
            string str = "update " + table + " set ";
            int c = 0;
            foreach(DictionaryEntry de in atts)   {
                if(c++ == 0)
                str = str + " " + de.Key + "=" + de.Value;
                else
                    str = str + "," + de.Key + "=" + de.Value;
            }
            str = str + where;
            //"update customers set street1='" + _street1 + "',city='" + _city + "',state='" + _state + "',phone='" + _phone + "' where customer='" + _company + "'");
            return str;
        }

        public String getInsertString(ICollection vals, string table)
        {
            string str = "insert into "+table+" values(";
            int c = 0;
            foreach (string val in vals)
            {
                if (c++ == 0)
                    str = str + " " + val;
                else
                    str = str + "," + val;
            }
            str = str + ")";
            //"update customers set street1='" + _street1 + "',city='" + _city + "',state='" + _state + "',phone='" + _phone + "' where customer='" + _company + "'");
            return str;
        }

        public static DB getInstance(int type)
        {
            if (db == null) db = new DB(type);
            return db;
        }

        public void close()
        {
            sqldb.close();
        }

        private void connect()
        {
            sqldb.connect();             
        }

        public int ExecuteNonQuery(string sql)
        {
            return sqldb.ExecuteNonQuery(sql);
        }

        public DbDataReader ExecuteQuery(string sql)
        {
            return sqldb.ExecuteQuery(sql);
        }
    }
}