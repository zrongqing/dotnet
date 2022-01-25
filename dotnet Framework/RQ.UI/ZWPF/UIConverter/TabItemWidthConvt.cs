using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RQ.UI.ZWPF.UIConverter
{
    /// <summary>
    /// 动态计算TabItem的宽度
    /// </summary>
    public class TabItemWidthConvt : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int || value is double)
            {
                int intParameter = int.Parse(parameter.ToString());
                double itemWith = (double)value / intParameter - 2; //减2是因为直接除的话,还是换行
                if (itemWith < 0)
                {
                    return value;
                }
                return itemWith;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
