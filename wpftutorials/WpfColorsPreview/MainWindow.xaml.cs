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
using System.Reflection;

namespace WpfColorsPreview
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var props = typeof(Colors).GetProperties(BindingFlags.Static |
            BindingFlags.Public);
            List<ColorInfo> colorsInfos = new List<ColorInfo>();
            foreach (var item in props)
            {
                Color c = (Color)item.GetValue(null, null);
                colorsInfos.Add(new ColorInfo()
                {
                    Name = item.Name,
                    Rgb = c,
                    RgbInfo = String.Format("R:{0} G:{1} B:{2}", c.R, c.G, c.B)
                });
            }
            this.DataContext = colorsInfos;
        }
    }
}
