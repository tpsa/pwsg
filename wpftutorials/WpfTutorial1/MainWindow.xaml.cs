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

namespace WpfTutorial1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void resultButton_Click(object sender, RoutedEventArgs e)
        {
            double first, second;
            if(!double.TryParse(firstTextBox.Text,out first) ||
                ! double.TryParse(secondTextBox.Text,out second))
            {
                MessageBox.Show("Incorrect input");
                return;
            }
            switch(operationComboBox.Text)
            {
                case "+":
                    resultBlock.Text=String.Format("{0}",first+second);
                break;
                case "-":
                    resultBlock.Text=String.Format("{0}",first-second);
                break;
                case "*":
                    resultBlock.Text=String.Format("{0}",first*second);
                break;
                case "/":
                    if(second==0){
                        MessageBox.Show("Division by zero");
                        return;
                    }
                    resultBlock.Text=String.Format("{0}",first/second);
                break;
            }
        }
    }
}
