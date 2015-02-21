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
using System.Security.Principal;
using System.Data.Common;
//using System.Web.ClientServices.Providers;
using Invoice365.util;
using System.IO;
using Microsoft.Win32;
using System.Globalization;
using System.Threading;

namespace Invoice365 
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public static string user = "";
        public static Boolean isAdmin = false;


        public HomeWindow()
        {
            
            DB.getInstance(DB.MSSQL);
            
            /*WindowsIdentity w = WindowsIdentity.GetCurrent();
            string name = w.Name;
            int idx = name.IndexOf("\\");
            if(idx > 0)
            name = name.Substring(idx+1);
            user = name;*/
            
            
            //LicenserIdentification.Form1 f = new LicenserIdentification.Form1();
            
            InitializeComponent();              
        }

        public void Window_Initialized(object sender, EventArgs e)
        {
            LicenserAPI.Logic l = new LicenserAPI.Logic();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\license.xml";
            string npath = LicenseUtil.licenseCheck(path);            
            if (string.IsNullOrEmpty(npath) || !l.IsValid(npath, "Invoice365", "inv365", false))
            {
                LicenseMessage lm = new LicenseMessage(LicenseMessage.MSG);             
                bool res = (bool)lm.ShowDialog();
                if (!res)
                {
                    if (!File.Exists(path))
                        Environment.Exit(0);
                    else if(l.IsValid(path, "Invoice365", "inv365", false))
                    {
                        MessageBox.Show("License File Inserted Successfully");
                    }                        
                }
            }
            else if (!string.IsNullOrEmpty(npath))
            {
                //MessageBox.Show("License file Inserted Successfully");
            } 
        }

        

       
        private void DashboardClick(object sender, RoutedEventArgs e)
        {
            navigate(new NewInvoice());                              
        }

        private void InvClick(object sender, RoutedEventArgs e)
        {

            navigate(new Inventory());             
        }


        private void InvoicesClick(object sender, RoutedEventArgs e)
        {
            navigate(new Invoices());
            
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            navigate(new CustomerFrame());
        }


        private void navigate(object _fr)
        {
            //this.ContentFrame.RemoveBackEntry();
            this.ContentFrame.Navigate(_fr);
            this.ContentFrame.RemoveBackEntry();
        }

        private void reports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.SALES));
        }

        private void customerDue_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.CUSTOMER_DUE));
        }


        private void wcustomerreports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.WEEKLY,SalesReport.CUSTOMER));
        }

        private void wbillerreports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.WEEKLY, SalesReport.BILLER));
        }

        private void wreports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.WEEKLY, SalesReport.SALES));
        }

        private void wcustomerDue_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.WEEKLY, SalesReport.CUSTOMER_DUE));
        }


        private void customerreports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.CUSTOMER));
        }

        private void billerreports_Click(object sender, RoutedEventArgs e)
        {
            navigate(new SalesReport(SalesReport.BILLER));
        }

        
        private void billers_Click(object sender, RoutedEventArgs e)
        {
            navigate(new Biller(true));
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutEWeb ae = new AboutEWeb();
            ae.Show();
        }

        private void expBillers_Click(object sender, RoutedEventArgs e)
        {
            navigate(new Export(Export.BILLER));
        }

        private void expCustomers_Click(object sender, RoutedEventArgs e)
        {
            navigate(new Export(Export.CUSTOMER));
        }

        private void expInventory_Click(object sender, RoutedEventArgs e)
        {
            navigate(new Export(Export.INVENTORY));
        }

        private void expInvoices_Click(object sender, RoutedEventArgs e)
        {
            navigate(new Export(Export.INVOICE));
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\Invoice365.chm");
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            LicenseMessage dlg = new LicenseMessage(LicenseMessage.REGISTER, "Update License");
            dlg.Show();
        }
    }
}