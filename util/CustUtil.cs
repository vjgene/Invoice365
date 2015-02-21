using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Invoice365
{
    class CustUtil
    {
        public static void loadCustomersPhone(String[] cols, DataTable t, string phone)
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select company,phone,fname,lname,street1,city,state,zip from customers where phone='"+phone+"' order by company asc");
            t.Rows.Clear();
            while (rdr.Read())
            {

                DataRow r = t.NewRow();
                for (int i = 0; i < cols.Length; i++)
                {
                    string val = rdr.GetValue(i).ToString();
                    r[cols[i]] = val;
                }
                t.Rows.Add(r);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }
        public static void loadCustomers(String[] cols, DataTable t)
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select company,phone,fname,lname,street1,city,state,zip from customers order by company asc");
            t.Rows.Clear();
            while (rdr.Read())
            {

                DataRow r = t.NewRow();
                for (int i = 0; i < cols.Length; i++)
                {
                    string val = rdr.GetValue(i).ToString();                    
                    r[cols[i]] = val;                    
                }
                t.Rows.Add(r);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        public static void loadCustomersFilter(String[] cols, DataTable t, String city)
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select company,phone,fname,lname,street1,city,state,zip from customers where city='"+city+"' order by company asc");
            t.Rows.Clear();
            while (rdr.Read())
            {

                DataRow r = t.NewRow();
                for (int i = 0; i < cols.Length; i++)
                {
                    string val = rdr.GetValue(i).ToString();
                    r[cols[i]] = val;
                }
                t.Rows.Add(r);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }
   
    public static void loadBillers(String[] cols, DataTable t)
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select company,phone,street1,city,state,zip from billers order by company asc");
            t.Rows.Clear();
            while (rdr.Read())
            {                
                DataRow r = t.NewRow();
                for (int i = 0; i < cols.Length; i++)
                {
                    string val = rdr.GetValue(i).ToString();                    
                    r[cols[i]] = val;                    
                }
                t.Rows.Add(r);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }
}
}
