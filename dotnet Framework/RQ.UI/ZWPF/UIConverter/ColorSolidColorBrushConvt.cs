using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RQ.UI.ZWPF.UIConverter
{
    /// <summary>
    /// ColorSolidColorBrush的颜色互相转化 主要是button的背景颜色需要
    /// </summary>
    public class ColorSolidColorBrushConvt : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
            {
                SolidColorBrush brush = new
                     SolidColorBrush((Color)value);
                return brush;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                SolidColorBrush brush = (SolidColorBrush)value;
                Color color = brush.Color;
                return color;
            }
            return value;
        }
    }
}
