// -----------------------------------------------------------------------
// <copyright file="ColorConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace zadanie3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            double red = (double)values[0];
            double green = (double)values[1];
            double blue = (double)values[2];
            return new SolidColorBrush(
            Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
