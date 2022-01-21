using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FontAwesome;

namespace RQ.UI.HWToast
{
    public class ToastIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object value = values[0];
            object grid = values[1];
            object txt = values[2];
            Grid _grid = grid as Grid;
            TextBlock _txt = txt as TextBlock;
            if (value == null)
            {
                if (_grid != null)
                {
                    _grid.ColumnDefinitions.RemoveAt(0);
                }
                if (_txt != null)
                {
                    _txt.HorizontalAlignment = HorizontalAlignment.Center;
                }
                return FontAwesome.WPF.FontAwesomeIcon.None;
                //return PackIconFontAwesomeKind.Nonex;
            }
            ToastIcons _value;
            try
            {
                _value = (ToastIcons)value;
            }
            catch
            {
                if (_grid != null)
                {
                    _grid.ColumnDefinitions.RemoveAt(0);
                }
                if (_txt != null)
                {
                    _txt.HorizontalAlignment = HorizontalAlignment.Center;
                }
                return FontAwesome.WPF.FontAwesomeIcon.None;
                //return PackIconFontAwesomeKind.None;
            }
            switch (_value)
            {
                case ToastIcons.Information:
                    {
                        return FontAwesome.WPF.FontAwesomeIcon.Check;
                        //return PackIconFontAwesomeKind.CheckSolid;
                    }
                case ToastIcons.Error:
                    {
                        return FontAwesome.WPF.FontAwesomeIcon.Times;
                        //return PackIconFontAwesomeKind.TimesSolid;
                    }
                case ToastIcons.Warning:
                    {
                        return FontAwesome.WPF.FontAwesomeIcon.Exclamation;
                        //return PackIconFontAwesomeKind.ExclamationSolid;
                    }
                case ToastIcons.Busy:
                    {
                        return FontAwesome.WPF.FontAwesomeIcon.ClockOutline;
                        //return PackIconFontAwesomeKind.ClockSolid;
                    }
            }
            if (_grid != null)
            {
                _grid.ColumnDefinitions.RemoveAt(0);
            }
            if (_txt != null)
            {
                _txt.HorizontalAlignment = HorizontalAlignment.Center;
            }
            return FontAwesome.WPF.FontAwesomeIcon.None;
            //return PackIconFontAwesomeKind.None;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
