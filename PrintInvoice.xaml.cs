﻿using System;
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
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO;
using System.IO.Packaging;
using System.Printing;

namespace Invoice365
{
    /// <summary>
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class PrintInvoice : Page
    {
       DataTable t = new DataTable();
        string[] cols = new string[] { "Item", "Description","Qty","Price Sell","Amount"};
        int[] widths = new int[] {64,192,64,128,64,64 };
        public PrintInvoice(Invoice inv)
        {
            InitializeComponent();
            initTable();
            loadInv(inv);
        }

        private void initTable()
        {
            for (int i = 0; i < cols.Length; i++)
            {
                DataColumn column = new DataColumn(cols[i]);
                column.DataType = typeof(string);
                t.Columns.Add(column);
            }

            GridView gridview = new GridView();

            for (int i = 0; i < cols.Length; i++)
            {
                GridViewColumn gvcolumn = new GridViewColumn();
                gvcolumn.Header = cols[i];
                gvcolumn.Width = widths[i];
                gvcolumn.DisplayMemberBinding = new Binding(cols[i]);
                gridview.Columns.Add(gvcolumn);
            }
            printinvView.View = gridview;

            //gridview.
            Binding bind = new Binding();
            printinvView.DataContext = t;
            printinvView.SetBinding(ListView.ItemsSourceProperty, bind);
        }

        public void loadInv(Invoice inv)    {
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

            totalTB.Text = inv.total.ToString("N2");
            invNo.Content = inv.id;
            customerTB.Text = inv.cust.ToString();
            billerTB.Text = inv.biller.ToString();
            shipTo.Text = inv.scust.ToString();
            if (string.IsNullOrEmpty(shipTo.Text)) shipTo.Text = inv.cust.ToString();
            date.Content = Convert.ToString(System.DateTime.Now);
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public static PrintDialog GetPrintDialog(PrintInvoice inv)
        {
            PrintDialog printDialog = null;

            // Create a Print dialog.
            PrintDialog dlg = new PrintDialog();

            // Show the printer dialog.  If the return is "true",
            // the user made a valid selection and clicked "Ok".
            if (dlg.ShowDialog() == true)
            {
                System.Printing.PrintCapabilities capabilities = dlg.PrintQueue.GetPrintCapabilities(dlg.PrintTicket);
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / 
                    inv.viewbox.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                inv.viewbox.ActualHeight);
                //inv.scrollview.LayoutTransform = new ScaleTransform(scale, scale);
                //Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                Size sz = new Size(inv.viewbox.ActualWidth, inv.viewbox.ActualHeight);
                //update the layout of the visual to the printer page size.
                //inv.viewbox.Measure(sz);
                inv.viewbox.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth,
                    capabilities.PageImageableArea.OriginHeight), sz));
                //now print the visual to printer to fit on the one page.
                dlg.PrintVisual(inv.viewbox, "Invoice");
                printDialog = dlg;  // return the dialog the user selections.
            }
            return printDialog;
        }

        private void print_Click_1(object sender, RoutedEventArgs e)
        {
            this.print.Visibility = Visibility.Hidden;

            //PrintQueue printQueue = GetPrintDialog(this).PrintQueue;
            GetPrintDialog(this);
            /*XpsDocumentWriter xpsWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
            //this.printinvView.
            xpsWriter.Write(this);*/
            this.print.Visibility = Visibility.Visible;
        }
    }
}