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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.ComponentModel;

namespace Invoice365
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Inv365Sortable
    {
        DataTable t = new DataTable();
        string[] cols = new string[] { "Item Code", "Item", "Vendor", "Qty", "Price Buy", "Price Sell"};
        public Inventory()
        {
            InitializeComponent();
            initTable();
            InvUtil.loadInv(cols, t);
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
            return invView;
        }

        private void initTable()
        {

            for (int i = 0; i < cols.Length; i++)
            {
                DataColumn column = new DataColumn(cols[i]);   
                
                column.DataType = typeof(string);
                t.Columns.Add(column);
                if (i == 0) t.PrimaryKey = new DataColumn[]{column};
            }

           
            GridView gridview = new GridView();

            for (int i = 0; i < cols.Length; i++)
            {
                GridViewColumn gvcolumn = new GridViewColumn();
                gvcolumn.Width = 150;
                gvcolumn.Header = cols[i];
                gvcolumn.DisplayMemberBinding = new Binding(cols[i]);
                gridview.Columns.Add(gvcolumn);
            }
            invView.View = gridview;


            Binding bind = new Binding();
            invView.DataContext = t;
            invView.SetBinding(ListView.ItemsSourceProperty, bind);
            

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select distinct item_code from items order by item_code");
            while (rdr.Read())
            {                
                itemCodeCB.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();

            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select distinct item from items order by item");
            while (rdr.Read())
            {
                itemCB.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();

            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select distinct vendor from items order by vendor");
            while (rdr.Read())
            {
                object _str = rdr.GetValue(0);
                string str = _str != null ? (string)_str : "";
                if(!String.IsNullOrEmpty(str))
                vendor.Items.Add(str);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();            
            GridView v = (GridView)invView.View;
        }

        

        private void itemCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = (string)itemCodeCB.SelectedItem;
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code, item, vendor, qty, buy_price, sell_price, bar_code from items where item_code='" + item + "'");

            if (rdr.Read())
            {
                buyTB.Text = rdr.GetDouble(4).ToString("N2");
                sellTB.Text = rdr.GetDouble(5).ToString("N2");
                itemCB.Text = rdr.GetString(1);
                
                vendor.Text = rdr.GetString(2);
                //unit.Text = rdr.GetString(7);
                //unitValue.Text = rdr.GetDouble(8).ToString();
                barCodeCombo.Text = rdr.GetString(6);
                DataRow r = t.Rows.Find(itemCodeCB.Text);
                
                if (r != null)
                {
                    invView.SelectedIndex = t.Rows.IndexOf(r);
                    invView.ScrollIntoView(invView.SelectedItem);
                    //unit.IsEnabled = false;
                    //unitValue.IsEnabled = false;
                }
            }
            else
            {
               
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {            
            Item txn = new Item();
            txn.item = itemCB.Text;
            txn.item_code = itemCodeCB.Text;
            if (txn.item_code == null || txn.item_code.Trim().Length == 0)
            {
                MessageBox.Show("Please Enter a Valid Item Code");
                return;
            }
            txn.qty = Double.Parse(qtyTB.Text != null && qtyTB.Text.Trim().Length > 0 ? qtyTB.Text : "0.0");
            txn.pricesell = Double.Parse(sellTB.Text != null && sellTB.Text.Trim().Length > 0 ? sellTB.Text : "0");
            txn.barcode = barCodeCombo.Text;
            txn.pricebuy = Double.Parse(buyTB.Text != null && buyTB.Text.Trim().Length > 0 ? buyTB.Text : "0");
            txn.vendor = vendor.Text;
            
            
           
            
            string str = "update items set qty=qty+" + txn.qty;
            str = str + ",sell_price=" + txn.pricesell + ",buy_price=" + txn.pricebuy;
            str = str + ",item='" + txn.item + "'";
            str = str + ",vendor='" + txn.vendor + "'";
            //str = str + ",bar_code='" + txn.barcode + "'";
           
                str = str + ",bar_code='" + txn.barcode + "' where item_code='" + txn.item_code + "'";
           
            
            int ret = DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
            if (ret == 0)
            {               
               str = "insert into items(item_code,item,vendor,bar_code,qty,buy_price,sell_price)";
               str = str + " values('" + txn.item_code + "','" + txn.item + "','" + txn.vendor+"','"+txn.barcode + "',";
               str = str + txn.qty + "," +txn.pricebuy + "," + txn.pricesell + ")";               
               DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
            }
            this.NavigationService.RemoveBackEntry();
            this.NavigationService.Navigate(new Inventory());
            
        }

        private void barCodeCombo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = (string)barCodeCombo.Text;
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code, item, vendor,qty, buy_price, sell_price, bar_code from items where bar_code='" + barcode + "'");

            if (rdr.Read())
            {
                buyTB.Text = rdr.GetDouble(4).ToString("N2");
                sellTB.Text = rdr.GetDouble(5).ToString("N2");
                itemCB.Text = rdr.GetString(1);
                itemCodeCB.Text = rdr.GetString(0);
                vendor.Text = rdr.GetString(2);
                //unit.Text = rdr.GetString(7);
                //unitValue.Text = rdr.GetString(8);
                barCodeCombo.Text = rdr.GetString(6);
                DataRow r = t.Rows.Find(itemCodeCB.Text);

                if (r != null)
                {
                    invView.SelectedIndex = t.Rows.IndexOf(r);
                    invView.ScrollIntoView(invView.SelectedItem);
                    unit.IsEnabled = false;
                    unitValue.IsEnabled = false;
                }
            }
            else
            {                
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (invView.SelectedIndex == -1) return;
            if (System.Windows.Forms.MessageBox.Show("Really delete?", "Confirm delete", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(
                        "delete from items where item_code='" + itemCodeCB.Text + "'");                
                this.NavigationService.RemoveBackEntry();
                this.NavigationService.Navigate(new Inventory());
            }            
        }

        private void invView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("" + invView.SelectedIndex);
            if (invView.SelectedIndex == -1)
            {                
                return;
            }            
            DataRow r = t.Rows[invView.SelectedIndex];
            itemCodeCB.Text = (string)r[0];
            itemCB.Text = (string)r[1];
            //barCodeCombo.Text = (string)r[2];            
            //unit.IsEnabled = false;
            //unitValue.IsEnabled = false;
        }

        private void vendor_SelectedValueChanged(object sender, EventArgs e)
        {

        }



        
       
    }
}
