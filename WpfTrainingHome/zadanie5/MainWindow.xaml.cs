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

namespace zadanie5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int angle = 15;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContentWebBrowser.Source = new Uri(AddressTextBox.Text);
            }
            catch (UriFormatException Excp)
            {
                MessageBox.Show("Nieznany format");
            }
            catch (Exception Excp)
            {
                MessageBox.Show("Excp");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ContentWebBrowser.CanGoBack)
                ContentWebBrowser.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ContentWebBrowser.CanGoForward)
                ContentWebBrowser.GoForward();
        }
    }
}
