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
using System.Data;
using System.Data.Common;
using System.IO;
using Invoice365.util;

namespace Invoice365
{
    /// <summary>
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class SalesReport : Inv365Sortable
    {
        DataTable t = new DataTable();

        public const int MONTHLY = 2;
        public const int WEEKLY = 1;
        public const int DAILY = 0;
        
        public const int SALES = 0;
        public const int CUSTOMER = 1;
        public const int BILLER = 2;
        public const int CUSTOMER_DUE = 3;
        public int type = 0;
        public int rtype = 0;
        GridView gridview = null;
        public static string[] filenames = { "sales_report.csv", "customer_report.csv", "biller_report.csv", "customer_due.csv" };
        
        string[] salescols = new string[] { "Item Code", "Item", "Qty Sold", "Amount" };
        string[] custcols = new string[] { "Customer", "Qty Sold", "Amount" };
        string[] billercols = new string[] { "Biller", "Qty Sold", "Amount" };
        string[] custduecols = new string[] { "Customer", "Amount Due"};
        string[] cols = null;

        public SalesReport(int type) : this(DAILY,type)
        {
        }

        public DateTime getDate(bool start)
        {
            DateTime dt = DateTime.Today;
            if (start)
            {
                if(rtype == DAILY)
                    dt = DateTime.Today;
                else if(rtype == WEEKLY)
                    dt = dt.AddDays(-7);
                return dt;
            }
            
            dt = dt.AddDays(1);            
            return dt;            
        }
        
        public SalesReport(int rtype, int type)
        {
            this.type = type;
            this.rtype = rtype;
            InitializeComponent();
            DateTime st = getDate(true);
            DateTime end = getDate(false);
            switch (type)
            {
                case SALES:
                    cols = salescols;
                    initTable();
                    loadSalesReport(st,end);
                    break;
                case CUSTOMER:
                    reportTitle.Content = "Sales By Customer";
                    cols = custcols;
                    initTable();
                    loadCustomerReport(st,end);
                    break;
                case BILLER:
                    reportTitle.Content = "Sales By Biller";
                    cols = billercols;
                    initTable();
                    loadBillerReport(st, end);
                    break;
                case CUSTOMER_DUE:
                    reportTitle.Content = "Amount Due By Customer";
                    cols = custduecols;
                    initTable();
                    loadCustomerDueReport(st, end);
                    break;
            }
            
            
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

        private void Window_Initialized(object sender, EventArgs e)
        {
            string str = "select distinct company from customers order by company";
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(str);

            while (rdr.Read())
            {
                customer.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();

            str = "select distinct phone from customers order by phone";
            rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(str);

            while (rdr.Read())
            {
                custphone.Items.Add(rdr.GetString(0));
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }


        private void initTable()
        {

            for (int i = 0; i < cols.Length; i++)
            {
                DataColumn column = new DataColumn(cols[i]);
                column.DataType = typeof(string);
                t.Columns.Add(column);
            }


            gridview = new GridView();

            for (int i = 0; i < cols.Length; i++)
            {
                GridViewColumn gvcolumn = new GridViewColumn();
                gvcolumn.Header = cols[i];
                if (i == 0) gvcolumn.Width = 200;
                else
                    gvcolumn.Width = 150;
                gvcolumn.DisplayMemberBinding = new Binding(cols[i]);
                gridview.Columns.Add(gvcolumn);
            }
            invView.View = gridview;


            Binding bind = new Binding();
            invView.DataContext = t;
            invView.SetBinding(ListView.ItemsSourceProperty, bind);

        }

        public void loadReport(DateTime st, DateTime end)
        {         
            switch (type)
            {
                case SALES:                   
                    loadSalesReport(st, end);
                    break;
                case CUSTOMER:                   
                    loadCustomerReport(st, end);
                    break;
                case BILLER:
                    loadBillerReport(st, end);
                    break;
                case CUSTOMER_DUE:
                    loadCustomerDueReport(st, end);
                    break;
            }
        }

        public void loadSalesReport(DateTime st, DateTime end)
        {
            
            string qry = "select item_code,item,sum(qty), sum(invoice_detail.amount) from invoice_detail join invoice on invoice.invoice_id = invoice_detail.invoice_id ";
            if(!string.IsNullOrEmpty(customer.Text))
                qry = qry + "where invoice.customer='"+customer.Text+
                    "' and invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by item_code,item order by item_code";        
            else if(!string.IsNullOrEmpty(custphone.Text))
                qry = "select item_code,item,sum(qty), sum(invoice_detail.amount) from invoice_detail  join invoice on invoice.invoice_id = "+
                    "invoice_detail.invoice_id join customers on customers.company=invoice.customer where customers.phone='" + custphone.Text + 
                    "' and invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by item_code,item order by item_code";  
            else
                qry = qry + "where invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by item_code,item order by item_code";  
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(qry);
            loadReport(rdr, 3);
        }

        /*public void loadSalesReport(DateTime st, DateTime end)
       {            
           string qry = "select item_code,item,sum(qty), sum(invoice_detail.amount) from invoice_detail join invoice on invoice.invoice_id = invoice_detail.invoice_id ";
           if (!string.IsNullOrEmpty(customer.Text))
               qry = qry + "where invoice.customer='" + customer.Text + " and";
           else if (!string.IsNullOrEmpty(custphone.Text))
               qry = "select item_code,item,sum(qty), sum(invoice_detail.amount) from invoice_detail  join invoice on invoice.invoice_id = " +
                   "invoice_detail.invoice_id join customers on customers.company=invoice.customer where customers.phone='" + custphone.Text + " and";
           else
               qry = qry + " where";
                       
             qry = qry + " invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by item_code,item order by item_code";  
           DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(qry);
           loadReport(rdr, 3);
       }*/

        public void loadCustomerReport(DateTime st, DateTime end)
        {            
            string qry = "select customer,sum(qty), sum(invoice_detail.amount) from invoice_detail join invoice on invoice.invoice_id = invoice_detail.invoice_id ";
            qry = qry + "where invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by customer order by customer";
            //MessageBox.Show(qry);
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(qry);
            loadReport(rdr, 2);
        }

        public void loadCustomerDueReport(DateTime st, DateTime end)
        {
            string qry = "select customer,sum(amount-amount_paid) as due from invoice ";
            //string qry = "select customer,sum(amount-amount_paid) from invoice ";
            qry = qry + "where invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by customer order by due desc";            
            //MessageBox.Show(qry);
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(qry);
            loadReport(rdr, 1);
        }

        public void loadBillerReport(DateTime st, DateTime end)
        {

            string qry = "select biller,sum(qty), sum(invoice_detail.amount) from invoice_detail join invoice on invoice.invoice_id = invoice_detail.invoice_id ";
            qry = qry + "where invoice.last_updated <='" + end + "' and invoice.last_updated >='" + st + "' group by biller order by biller";            
            //MessageBox.Show(qry);
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(qry);
            loadReport(rdr, 2);
        }

        public void loadReport(DbDataReader rdr, int idx)
        {
            t.Rows.Clear();
            while (rdr.Read())
            {
                DataRow r = t.NewRow();
                for (int i = 0; i < cols.Length; i++)
                {
                    if(i == idx)
                        r[cols[i]] = rdr.GetDouble(i).ToString("N2");
                    else
                        r[cols[i]] = rdr.GetValue(i).ToString();
                }
                t.Rows.Add(r);
            }
            rdr.Close();
            DB.getInstance(DB.MSSQL).close();
        }

        private void customer_SelectedValueChanged(object sender, EventArgs e)
        {
            /*string item = (string)customer.SelectedItem;
            DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
                "select item_code,item,sum(qty), sum(amount) from invoice_detail join invoice on invoice.invoice_id = invoice_detail.invoice_id where invoice.customer='" + item + "'" + " group by item_code,item order by item_code");
                //"select item_code,item,sum(amount), qty, unit_qty from invoice_detail group by item_code order by item_code where customer='" + item + "'");
            loadReport(rdr);*/
        }

        private void custphone_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void itemCode_SelectedValueChanged(object sender, EventArgs e)  {
        //{
            //string item = (string)itemCode.Text;
            //DbDataReader rdr = DB.getInstance(DB.MSSQL).ExecuteQuery(
               // "select item_code,item,sum(qty), sum(amount) from invoice_detail where item_code='" + item + "' group by item_code,item order by item_code ");
            //loadReport(rdr);
       }

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            if(startDate.SelectedDate == null || endDate.SelectedDate == null)   {
                MessageBox.Show("Please enter a valid start and end date");
                return;
            }
            DateTime st = startDate.SelectedDate.Value;
            DateTime end = endDate.SelectedDate.Value;
            end = end.AddDays(1);            
            loadReport(st,end);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ExportUtil.exportReports(filenames[type], t);
        }
    }
}