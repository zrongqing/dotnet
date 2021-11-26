using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HWCommonUI.UIConverter
{
    /// <summary>
    /// 将Color的颜色转换成指定字符串
    /// </summary>
    /// <see cref="HWCommonUI.HWUserControl.ColorSelButton"/>
    class ColorSelBtnToStrConvt : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color)
            {
                Color color = (Color)value;
                string colorStr = "RGB  " + color.R.ToString() + "-" + color.G.ToString() + "-" + color.B.ToString();
                return colorStr;
            }
            return "<无替换>";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
