using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RQ.UIConverter
{
    /// <summary>
    /// 将图片路径转换成Bit取消对文件的占用
    /// </summary>
    public class ImgPathToBitConvt : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string path = (string)value;
                if (File.Exists(path))
                {

                    BitmapImage bitmap = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                    {
                        bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;//设置缓存模式
                        bitmap.StreamSource = ms;//通过StreamSource加载图片
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }
                    return bitmap;
                }
                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
