using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;  
using System.Windows.Navigation;
using System.Data.Common;
using System.ComponentModel;
using System.IO;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;
using System.Windows.Markup;

using Microsoft.Win32;
using System.Collections;


namespace Invoice365
{
    /// <summary>
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class CustomerFrame : Inv365Sortable
    {      
        string table = "customers";
        DataTable t = new DataTable();
        string[] cols = new string[] { "Company", "Phone" , "FirstName", "LastName", "Street1", "City", "State","Zip"};

        string [] defaultCities = new string[]{"Todo", "Bronx", "Brooklyn", "Manhattan", "New Jersey", "Staten Island", "Queens"};

        private void fillCust()
        {
            for (int i = 0; i < defaultCities.Length; i++) city.Items.Add(defaultCities[i]);
        }

        public CustomerFrame()
        {
            InitializeComponent();
            initTable(cols);
            fillCust();
            CustUtil.loadCustomers(cols, t);
        }

        public override DataTemplate getUpTemplate()
        {
            return Resources["HeaderTemplateArrowUp"] as DataTemplate;
        }

        public override DataTemplate getDownTemplate()
        {
            return Resources["HeaderTemplateArrowDown"] as DataTemplate;
        }

        public override ListView getView()
        {
            return custView;
        }

        public CustomerFrame(bool billers)
        {
            
            InitializeComponent();           
                initTable(cols);
                fillCust();
                CustUtil.loadCustomers(cols, t);
             
        }

        private void initTable(string [] acols)
        {
            for (int i = 0; i < acols.Length; i++)
            {
                DataColumn column = new DataColumn(acols[i]);
                column.DataType = typeof(string);
                t.Columns.Add(column);
            }
            t.Rows.Clear();
            GridView gridview = new GridView();

            for (int i = 0; i < acols.Length; i++)
            {
                GridViewColumn gvcolumn = new GridViewColumn();
                gvcolumn.Width = 150;
                gvcolumn.Header = acols[i];
                gvcolumn.DisplayMemberBinding = new Binding(acols[i]);
                gridview.Columns.Add(gvcolumn);
            }
            custView.View = gridview;

            Binding bind = new Binding();
            custView.DataContext = t;
            custView.SetBinding(ListView.ItemsSourceProperty, bind);
        }

        private void custView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("" + invView.SelectedIndex);
            if (custView.SelectedIndex == -1)            {                
                return;
            }
            DataRow r = t.Rows[custView.SelectedIndex];
            int i = 0;
            customerCB.Text = (string)r[i++];
            phone.Text = (string)r[i++];
            
            firstName.Text = (string)r[i++];
            lastName.Text = (string)r[i++];
                
            
            street1.Text = (string)r[i++];
            city.Text = (string)r[i++];
            state.Text = (string)r[i++];
            //phone.Text = (string)r[4];
            //unitValue.IsEnabled = false;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            string str = "select distinct company from " + table + " order by company";
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(str);

            while (rdr.Read())
            {
                customerCB.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();

            str = "select distinct phone from " + table + " order by phone";
            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(str);

            while (rdr.Read())
            {
                phone.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
            //GridView v = (GridView)txnView.View;
        }

        private void txnView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }

        private void itemCB_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cityCB_SelectedValueChanged(object sender, EventArgs e)
        {
            if(city.Text == "All" || city.Text == "Todo")  {
                CustUtil.loadCustomers(cols,t);
            }
            else
            CustUtil.loadCustomersFilter(cols, t, city.Text);
        }

        private void phoneCB_SelectedValueChanged(object sender, EventArgs e)
        {
               CustUtil.loadCustomersPhone(cols, t, phone.Text);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
        }



        private void savecustomer_Click(object sender, RoutedEventArgs e)
        {
            string _company = customerCB.Text;
            string _fn = firstName.Text;
            string _ln = lastName.Text;
            string _street1 = street1.Text;
            string _street2 = street2.Text;
            string _city = city.Text;
            string _state = state.Text;
            string _zip = zip.Text;
            string _phone = phone.Text;
            string _fax = fax.Text;
            string _email = email.Text;

            string __street1 = s_street1.Text;
            string __street2 = s_street2.Text;
            string __city = s_city.Text;
            string __state = s_state.Text;
            string __zip = s_zip.Text;
            string __phone = s_phone.Text;
            string __fax = s_fax.Text;
            string __email = s_email.Text;

            Hashtable t = new Hashtable();            
            t["city"] = "'" + _city + "'";
            t["state"] = "'" + _state + "'";
            t["zip"] = "'" + _zip + "'";
            t["phone"] = "'" + _phone + "'";
            t["street1"] = "'" + _street1 + "'";
            t["street2"] = "'" + _street2 + "'";
            t["fax"] = "'" + _fax + "'";
            t["email"] = "'" + _email + "'";

            t["s_city"] = "'" + __city + "'";
            t["s_state"] = "'" + __state + "'";
            t["s_zip"] = "'" + __zip + "'";
            t["s_phone"] = "'" + __phone + "'";
            t["s_street1"] = "'" + __street1 + "'";
            t["s_street2"] = "'" + __street2 + "'";
            t["s_fax"] = "'" + __fax + "'";
            t["s_email"] = "'" + __email + "'";


           
            t["fname"] = "'" + _fn + "'";
            t["lname"] = "'" + _ln + "'";            
            t["company"] = "'" + _company + "'";

            ArrayList l = new ArrayList();
            l.Add("'" + _city + "'");
            l.Add("'" + _state + "'");
            
            l.Add("'" + _phone + "'");
            l.Add("'" + _street1 + "'");
            l.Add("'" + _street2 + "'");
            l.Add("'" + _fax + "'");
            l.Add("'" + _email + "'");

            l.Add("'" + _fn + "'");
            l.Add("'" + _ln + "'");
            l.Add("'" + _company + "'");
            l.Add("'" + _zip + "'");

            l.Add("'" + __city + "'");
            l.Add("'" + __state + "'");
            l.Add("'" + __zip + "'");
            l.Add("'" + __phone + "'");
            l.Add("'" + __street1 + "'");
            l.Add("'" + __street2 + "'");
            l.Add("'" + __fax + "'");
            l.Add("'" + __email + "'");


           
           

            if (_company == null || _company.Trim().Length == 0)
            {
                MessageBox.Show("Company Name is Required");
                return;
            }
            string str = DB.getInstance(DB.MSSQL).getUpdateString(t, table, " where company='" + _company + "'");            
            int ret = DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);

            if (ret == 0)
            {
                str = DB.getInstance(DB.MSSQL).getInsertString(l, table);
                ret = DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                if (ret == 0)
                {
                    MessageBox.Show("Please enter required fields (first name, last name, company, street1, city, state) ");
                }                
            }
            this.NavigationService.RemoveBackEntry();
            this.NavigationService.Navigate(new CustomerFrame(false));
            //MessageBox.Show(this.Parent.GetType().ToString());
            //initTable();
        }

        private void custCB_SelectedValueChanged(object sender, EventArgs e)
        {            
            string item = (string)customerCB.SelectedItem;
            DbDataReader rdr = null;
           
                rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                 "select company,phone,fname,lname,street1,street2,city,state,fax,email,zip,s_street1,s_street2,s_city,s_state,s_fax,s_email,s_zip,s_phone from " + table + " where company='" + item + "'");
          
           
            if (rdr.Read())
            {
                //MessageBox.Show("selection changed");

                int i = 0;

                customerCB.Text = rdr.GetString(i++);
                phone.Text = rdr.GetString(i++);
                
                    firstName.Text = rdr.GetString(i++);
                    lastName.Text = rdr.GetString(i++);
               
                street1.Text = rdr.GetString(i++);
                street2.Text = rdr.GetString(i++);
                city.Text = rdr.GetString(i++);
                state.Text = rdr.GetString(i++);
                fax.Text = rdr.GetString(i++);
                email.Text = rdr.GetString(i++);
                zip.Text = rdr.GetString(i++);

                s_street1.Text = rdr.GetString(i++);
                s_street2.Text = rdr.GetString(i++);
                s_city.Text = rdr.GetString(i++);
                s_state.Text = rdr.GetString(i++);
                s_fax.Text = rdr.GetString(i++);
                s_email.Text = rdr.GetString(i++);
                s_zip.Text = rdr.GetString(i++);
                s_phone.Text = rdr.GetString(i++);

            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        private void deleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (custView.SelectedIndex == -1) return;
            if (System.Windows.Forms.MessageBox.Show("Really delete?", "Confirm delete", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(
                    "delete from "+table+" where company='" + customerCB.Text + "'");
                t.Rows[custView.SelectedIndex].Delete();
                custView.SelectedIndex = -1;
                this.NavigationService.RemoveBackEntry();
                this.NavigationService.Navigate(new CustomerFrame(false));
            }                   
        }
    }        
}