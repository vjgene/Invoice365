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
    public partial class Biller : Inv365Sortable
    {
        bool billers = false;
        string table = "billers";
        DataTable t = new DataTable();
       
        string[] billcols = new string[] { "Company", "Phone", "Street", "City", "State","Zip" };
        private String unit;

        public Biller()
        {
            InitializeComponent();
            initTable(billcols);
            CustUtil.loadCustomers(billcols, t);
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

        public Biller(bool billers)
        {
            
                this.table = "billers";
                this.billers = true;
           
            InitializeComponent();
            
                initTable(billcols);
                this.title.Content = "Add Biller";
                this.savecustomer.Content = "Save Biller";
                this.deleteCustomer.Content = "Delete Biller";
                

                CustUtil.loadBillers(billcols, t);
            
            
        }

        private void initTable(string[] acols)
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
            if (custView.SelectedIndex == -1)
            {
                return;
            }
            DataRow r = t.Rows[custView.SelectedIndex];
            int i = 0;
            customerCB.Text = (string)r[i++];
            phone.Text = (string)r[i++];
            
            street1.Text = (string)r[i++];
            city.Text = (string)r[i++];
            state.Text = (string)r[i++];
            zip.Text = (string)r[i++];
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
            //GridView v = (GridView)txnView.View;
        }

        private void txnView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }

        private void itemCB_SelectedValueChanged(object sender, EventArgs e)
        {
            /*string item = (string)itemCodeCB.SelectedItem;            
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code, item, qty, sell_price, bar_code, unit_value, unit from items where item_code='" + item+"'");
            
            if (rdr.Read())
            {
                priceSell.Text = rdr.GetDouble(3).ToString();
                itemCB.Text = rdr.GetString(1);
                barCodeCombo.Text = rdr.GetString(4);                
                unitVal.Content = rdr.GetString(5);
                unit = rdr.GetString(6);

                int uval = rdr.GetInt32(5);                
                int qty = rdr.GetInt32(2);
                if (uval > 0)
                {
                    curQty.Content = (qty / uval) + "";
                }
                else
                    curQty.Content = qty + "";
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();*/
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            //if(txnView.SelectedIndex == -1) return;
            //t.Rows[txnView.SelectedIndex].Delete();

        }



        private void savecustomer_Click(object sender, RoutedEventArgs e)
        {
            string _company = customerCB.Text;
            
            string _street1 = street1.Text;
            string _street2 = street2.Text;
            string _city = city.Text;
            string _state = state.Text;
            string _phone = phone.Text;
            string _fax = fax.Text;
            string _email = email.Text;
            string _zip = zip.Text;

            Hashtable t = new Hashtable();
            t["city"] = "'" + _city + "'";
            t["state"] = "'" + _state + "'";
            t["phone"] = "'" + _phone + "'";
            t["street1"] = "'" + _street1 + "'";
            t["street2"] = "'" + _street2 + "'";
            t["fax"] = "'" + _fax + "'";
            t["email"] = "'" + _email + "'";           
            t["company"] = "'" + _company + "'";
            t["zip"] = "'" + _zip + "'";

            ArrayList l = new ArrayList();
            l.Add("'" + _city + "'");
            l.Add("'" + _state + "'");
            l.Add("'" + _phone + "'");
            l.Add("'" + _street1 + "'");
            l.Add("'" + _street2 + "'");
            l.Add("'" + _fax + "'");
            l.Add("'" + _email + "'");            
            l.Add("'" + _company + "'");
            l.Add("'" + _zip + "'");

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
                    MessageBox.Show("Please enter required fields (company, street1, city, state) ");
                }
            }
            this.NavigationService.RemoveBackEntry();
            this.NavigationService.Navigate(new Biller(billers));
            //MessageBox.Show(this.Parent.GetType().ToString());
            //initTable();
        }

        private void custCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = (string)customerCB.SelectedItem;
            DbDataReader rdr = null;
           
            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select company,phone,street1,street2,city,state,fax,email,zip from " + table + " where company='" + item + "'");
            
            if (rdr.Read())
            {
                //MessageBox.Show("selection changed");
                int i = 0;

                customerCB.Text = rdr.GetString(i++);
                phone.Text = rdr.GetString(i++);
                
                street1.Text = rdr.GetString(i++);
                street2.Text = rdr.GetString(i++);
                city.Text = rdr.GetString(i++);
                state.Text = rdr.GetString(i++);
                fax.Text = rdr.GetString(i++);
                email.Text = rdr.GetString(i++);
                zip.Text = rdr.GetString(i++);
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
                    "delete from " + table + " where company='" + customerCB.Text + "'");
                t.Rows[custView.SelectedIndex].Delete();
                custView.SelectedIndex = -1;
                this.NavigationService.RemoveBackEntry();
                this.NavigationService.Navigate(new Biller(billers));
            }
        }
    }
}
