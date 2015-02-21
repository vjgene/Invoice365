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

namespace Invoice365
{
    /// <summary>
    /// Interaction logic for PayInvoice.xaml
    /// </summary>
    public partial class PayInvoice : Window
    {        
        public PayInvoice(Invoice inv)
        {
            InitializeComponent();            
        }

        private void payNow_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;            
        }

        private void payLater_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;            
        }
    }
}