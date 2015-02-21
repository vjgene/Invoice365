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
    public partial class NewInvoice : Page
    {
        DataTable t = new DataTable();
        string[] cols = new string[] { "Item Code", "Item", "Qty", "Price Sell", "Amount"};
        bool  edit = false;
        private Invoice inv = null;

        public NewInvoice()
        {
            InitializeComponent();
            initTable();
        }

        public NewInvoice(Invoice inv)
        {
            InitializeComponent();
            invHeader.Content = "Invoice# " + inv.id;
            initTable();
            loadInv(inv);
            edit = true;
        }

        public void loadInv(Invoice inv)
        {
            //MessageBox.Show(""+inv.id);
            List<LineItem> items = inv.lines;
            foreach (LineItem i in items)
            {
                DataRow r = t.NewRow();
                r[0] = i.item_code;
                r[1] = i.item;
                r[2] = i.qty;

                r[3] = i.pricesell.ToString("N2");
                r[4] = i.amount.ToString("N2");
                t.Rows.Add(r);
            }
            this.inv = inv;
            customer.Text = inv.cust.name;
            biller.Text = inv.biller.name;
        }

        private void initTable()
        {
            for (int i = 0; i < cols.Length; i++)
            {
                DataColumn column = new DataColumn(cols[i]);
                column.DataType = typeof(string);
                t.Columns.Add(column);
            }
            t.Rows.Clear();
            GridView gridview = new GridView();

            for (int i = 0; i < cols.Length; i++)
            {
                GridViewColumn gvcolumn = new GridViewColumn();
                gvcolumn.Header = cols[i];
                gvcolumn.Width = 150;
                gvcolumn.DisplayMemberBinding = new Binding(cols[i]);
                gridview.Columns.Add(gvcolumn);
            }
            txnView.View = gridview;

            Binding bind = new Binding();
            txnView.DataContext = t;
            txnView.SetBinding(ListView.ItemsSourceProperty, bind);   
        }

        Dictionary<string,double> iUpd = new Dictionary<string,double>();
       
        private void add_Click(object sender, RoutedEventArgs e)
        {       
            
            DataRow r = t.NewRow();
            if (itemCodeCB.Text == null || itemCodeCB.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Select a Valid Item");
                return;
            }            
            r[0] = itemCodeCB.Text;
            r[1] = itemCB.Text;

            double qty = Double.Parse(qtyTB.Text != null && qtyTB.Text.Trim().Length > 0 ? qtyTB.Text : "1.0");
            //MessageBox.Show("qty " + qty);
            //MessageBox.Show("cqty " + qty);
            string currQty = (string)cq[itemCodeCB.Text];
            //MessageBox.Show(currQty);
            double cqty = Double.Parse(""+(currQty != null && currQty.ToString().Trim().Length > 0 ? currQty : "1.0"));
           
            if (qty <= 0)
            {
                MessageBox.Show("Please enter a qty greater than 0 ");
                return;
            }
            else if (cqty == 0)
            {
                MessageBox.Show("Zero items in inventory. Please update the inventory");
                return;
            }
            else if (qty > cqty)    {
                double tqty = cqty + 1;
                MessageBox.Show("Please enter a qty less than " + tqty);
                return;
            }
            cqty = cqty - qty;
            //MessageBox.Show(itemCodeCB.Text + " " + cqty);
            cq[itemCodeCB.Text] = ""+cqty;
            
            //MessageBox.Show("cqty2: "+curQty.Content);
           
            r[2] = qty;
            if(iUpd.ContainsKey(itemCodeCB.Text))  {
                iUpd[itemCodeCB.Text] -= qty;
            }
            else
                iUpd[itemCodeCB.Text] = 0-qty;
                
            
            string prSell = editPrice.Text == null || editPrice.Text.Trim().Length == 0 ? priceSell.Text : editPrice.Text;
            //r[2] = r[2] + " (" + unit + ")";
            r[3] = prSell;
            double amt = Double.Parse(r[2] != null && r[2].ToString().Trim().Length != 0 ? r[2].ToString() : "0");
            r[4] = amt*Double.Parse(prSell);
            t.Rows.Add(r);
            Clear();            
        }

        private void Clear()
        {
            itemCB.SelectedIndex = -1;
            itemCodeCB.SelectedIndex = -1;
            priceSell.Clear();
            qtyTB.Clear();
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

            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select distinct company from customers order by company");
            while (rdr.Read())
            {
                customer.Items.Add(rdr.GetString(0));
            }
            rdr.Close();

            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select distinct company from billers order by company");
            while (rdr.Read())
            {
                biller.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();

            
            GridView v = (GridView)txnView.View;            
        }

        private void txnView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }

        Dictionary<string,string> cq = new Dictionary<string,string>();
  
        private void itemCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = (string)itemCodeCB.SelectedItem;            
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code, item, qty, sell_price, bar_code from items where item_code='" + item+"'");
            
            if (rdr.Read())
            {
                priceSell.Text = rdr.GetDouble(3).ToString("N2");
                itemCB.Text = rdr.GetString(1);
                barCodeCombo.Text = rdr.GetString(4);             
                double qty = rdr.GetDouble(2);
                if(!cq.ContainsKey(item))
                    cq[item] = qty.ToString();
                //curQty.Content = qty;               

            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if(txnView.SelectedIndex == -1) return;
            DataRow r = t.Rows[txnView.SelectedIndex];
            double qty = Double.Parse(""+r[2]);
            //iUpd[itemCodeCB.Text] -= qty;
            if (iUpd.ContainsKey(itemCodeCB.Text))
            {
                iUpd[""+r[0]] += qty;
            }
            else
                iUpd[""+r[0]] = qty;
            r.Delete();
            
        }

        private void barCodeCombo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string item = barCodeCombo.Text;
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code, item, qty, sell_price, bar_code from items where bar_code='" + item + "'");

            if (rdr.Read())
            {
                priceSell.Text = rdr.GetDouble(3).ToString("N2");
                itemCB.Text = rdr.GetString(1);
                barCodeCombo.Text = rdr.GetString(4);
                itemCodeCB.Text = rdr.GetString(0);
                string qty = rdr.GetDouble(2).ToString();
                if (!cq.ContainsKey(itemCodeCB.Text))
                    cq[item] = qty;
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        
      
        private void custCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = (string)customer.SelectedItem;            
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select phone from customers where company='" + item+"'");            
            if (rdr.Read())
            {                
                custPhone.Text = rdr.GetString(0);               
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        
        private void saveAndPrint(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                editSaveAndPrint(sender, e);
                return;
            }
                string str = "insert into invoice(customer,iuser,biller) values('"+customer.Text+"','"+HomeWindow.user+"','"+biller.Text+"')".ToString();
                if (customer.Text == null || customer.Text.Length == 0)
                {
                    MessageBox.Show("Please select a valid customer");
                    return;
                }
                if (biller.Text == null || biller.Text.Length == 0)
                {
                    MessageBox.Show("Please select a valid biller");
                    return;
                }  
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                int cnt = t.Rows.Count;


                DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select max(invoice_id) from invoice");
                int invoice_id = 0;
                if (rdr.Read())
                {
                    invoice_id = rdr.GetInt32(0);
                }
                rdr.Close();
                DB.getInstance(DB.MSSQL).close();

                double total = 0;
                Invoice inv = InvUtil.getInvoice(customer.Text, biller.Text);
                

                for (int i = 0; i < cnt; i++)
                {
                    DataRow r = t.Rows[i];
                    string qstr = r[2].ToString();
                    
                    double qty = 0; 
                    
                    double price = 0;
                    double _amt = 0;
                    try
                    {
                        qty = Double.Parse(qstr);
                        
                        price = Double.Parse(r[3].ToString());
                        _amt = Double.Parse(r[4].ToString());
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message);
                    }
                    LineItem item = new LineItem();
                    item.item_code = (string)r[0];
                    item.item = (string)r[1];
                    item.qty = qty;
                    
                    item.pricesell = price;
                    item.amount = _amt;
                    
                    total += qty * price;
                    
                    str = "insert into invoice_detail(item_code,item,qty,price_sell,amount,invoice_id) values('";
                    str = str + r[0] + "','" + item.item+"',"+ qty + "," + item.pricesell + "," + item.amount + ","+invoice_id+")";                    
                    DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                    str = "update items set qty=qty-" + qty + " where item_code='" + r[0] + "'";                    
                    DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                    inv.lines.Add(item);
                }
                inv.id = getInvoiceId();
                str = "update invoice set amount=" + total + " where invoice_id=" + inv.id;
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                DB.getInstance(DB.MSSQL).close();
                inv.total = total;                
                printInvoice(inv);         
        }

        private int getInvoiceId()
        {
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery("select max(invoice_id) from invoice");                
            if (rdr.Read())
            {
                return rdr.GetInt32(0);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
            return -1;
        }

        private void printInvoice(Invoice invoice)
        {            
            this.NavigationService.Navigate(new PrintInvoice(invoice));
        }

        /*private void printInvoice(Invoice invoice)
        {
            PayInvoice pi = new PayInvoice(invoice);
            pi.due.Text = (invoice.total - invoice.paid).ToString("N2");
            pi.otherAmt.Text = (invoice.total - invoice.paid).ToString("N2");
            bool refund = false;
            if (invoice.total > invoice.paid)
            {
                pi.ShowDialog();
            }
            else if (invoice.total < invoice.paid)
            {
                pi.payNow.Content = "Refund Now";
                pi.payLater.Content = "Refund Later";
                refund = true;
                pi.ShowDialog();
            }
            
            if (pi.DialogResult == true)
            {
                if (string.IsNullOrEmpty(pi.otherAmt.Text))
                {
                    MessageBox.Show("Please Enter an amount greater than zero");
                    return;
                }
                else
                {
                    double amt = Double.Parse(pi.otherAmt.Text);
                    if (amt == 0 || (!refund && amt < 0))
                    {
                        MessageBox.Show("Please Enter an amount greater than zero");
                        return;
                    }
                    amt = inv.paid + amt;
                    string str = "update invoice set amount_paid =" + amt + " where invoice_id=" + inv.id;
                    DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);  
                }
                pi.Close();
                this.NavigationService.Navigate(new PrintInvoice(invoice));
            }
            else
            {
                pi.Close();
                this.NavigationService.Navigate(new PrintInvoice(invoice));
                
            }
                
            
        }*/

        private void biller_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void editSaveAndPrint(object sender, RoutedEventArgs e)
        {
            //string str = "insert into invoice(customer,iuser,biller) values('" + customer.Text + "','" + HomeWindow.user + "','" + biller.Text + "')".ToString();
            string str = "";
            if (customer.Text == null || customer.Text.Length == 0)
            {
                MessageBox.Show("Please select a valid customer");
                return;
            }
            if (biller.Text == null || biller.Text.Length == 0)
            {
                MessageBox.Show("Please select a valid biller");
                return;
            }


            //DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
            int cnt = t.Rows.Count;
            int invoice_id = inv.id;

            double total = 0;
            //Invoice inv = InvUtil.getInvoice(customer.Text, biller.Text);



            DB.getInstance(DB.MSSQL).ExecuteNonQuery("delete from invoice_detail where invoice_id=" + invoice_id);
            DB.getInstance(DB.MSSQL).close();
            for (int i = 0; i < cnt; i++)
            {
                DataRow r = t.Rows[i];
                string qstr = r[2].ToString();
                double qty = 0;
                double price = 0;
                double _amt = 0;
                try
                {
                    qty = Double.Parse(qstr);

                    price = Double.Parse(r[3].ToString());
                    _amt = Double.Parse(r[4].ToString());
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
                LineItem item = new LineItem();
                item.item_code = (string)r[0];
                item.item = (string)r[1];
                item.qty = qty;
                item.pricesell = price;
                item.amount = _amt;
                total += qty * price;
                str = "insert into invoice_detail(item_code,item,qty,price_sell,amount,invoice_id) values('";
                str = str + r[0] + "','" + item.item + "'," + qty + "," + item.pricesell + "," + item.amount + "," + invoice_id + ")";
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
                
                inv.lines.Add(item);
            }      
            for(IDictionaryEnumerator de=iUpd.GetEnumerator(); de.MoveNext();)  {
                str = "update items set qty=qty+" + de.Value + " where item_code='" + de.Key + "'";
                DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
            }
            //str = "update invoice set  where invoice_id=" + inv.id;
            //DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);            
            inv.total = total;
            str = "update invoice set biller='" + biller.Text + "', customer='" + customer.Text + "', amount=" + total + ",last_updated=getdate() where invoice_id=" + inv.id;
            
            DB.getInstance(DB.MSSQL).ExecuteNonQuery(str);
            DB.getInstance(DB.MSSQL).close();
            Invoice tInv = InvUtil.getInvoice(customer.Text, biller.Text);
            inv.cust = tInv.cust;
            inv.biller = tInv.biller;
 
            printInvoice(inv);
        }
        }
    }