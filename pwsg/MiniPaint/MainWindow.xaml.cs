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
using System.Windows.Media.Effects;

namespace MiniPaint
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
            //ellipse1.MouseDown += ClickedEl;
        }

        private void rect1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Shape s = (Shape)sender;
            startx = Canvas.GetLeft(s);
            starty = Canvas.GetTop(s);
            s.MouseMove += rect1_MouseMove;
            s.CaptureMouse();
            e.Handled = true;
            Point p = e.GetPosition((IInputElement)canvas1);
            deltax = p.X - Canvas.GetLeft(s);
            deltay = p.Y - Canvas.GetTop(s);
            if (last != null)
            {
                if (last.Effect != null)
                {
                    last.Effect = null;
                    Canvas.SetZIndex(last, 0);
                }
            }
            last = s;
            DropShadowEffect ef  = new DropShadowEffect();
            ef.Color = Colors.Red;
            ef.BlurRadius = 30;
            //ef.
            s.Effect = ef;
            Canvas.SetZIndex(last, 10);
            
        }

        public double startx;
        public double starty;
        public double deltax;
        public double deltay;

        Shape last = null;

        private void rect1_MouseMove(object sender, MouseEventArgs e)
        {
            Shape s = (Shape)sender;
            if (s.IsMouseCaptured)
            {
                Point p = e.GetPosition((IInputElement)canvas1);
                //Canvas.Top
                double positionX = Canvas.GetLeft(s);
                double positionY = Canvas.GetTop(s);
                Canvas.SetLeft(s, p.X - deltax);
                Canvas.SetTop(s, p.Y - deltay);
                int a = 1;
            }
            e.Handled = true;
        }

        private void rect1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Shape s = (Shape)sender;
            //s.Effect = null;
            s.MouseMove -= rect1_MouseMove;
            s.ReleaseMouseCapture();
            e.Handled = true;
        }
    }
}
