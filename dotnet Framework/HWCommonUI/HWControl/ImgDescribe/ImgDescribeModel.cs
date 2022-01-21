using System;
using RQ.HWComponentModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;


namespace RQ.UI.ImgDescribe
{

    /// <summary>
    /// ImgDescribe绑定的界面数据,禁止添加与界面不相关的数据
    /// </summary>
    public class ImgDescribeModel : NotifyPropertyBase
    {
        private string imgSource;
        private string name;
        private bool btnVisible;

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgSource
        {
            get => imgSource;
            set
            {
                if (value != imgSource)
                {
                    imgSource = value;
                    base.OnPropertyChanged(nameof(ImgSource));
                }
            }
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                base.OnPropertyChanged(nameof(Name));
            }
        }
        /// <summary>
        /// button显隐性
        /// </summary>
        public bool BtnVisible
        {

            get => btnVisible;
            set
            {
                btnVisible = value;
                base.OnPropertyChanged(nameof(BtnVisible));
            }
        }
        #region 跟界面无关的数据
        /// <summary>
        /// 此model的标记,标识一些东西
        /// </summary>
        public object ModelSign { get; set; }
        #endregion
        public ImgDescribeModel()
        {
            // 本地路径 测试
            ImgSource = @"C:\Users\58317\Desktop\色图\知世.jpg";

            Name = "未命名";
            BtnVisible = false;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="imgSource">所展示的图片路径</param>
        /// <param name="name">展示的名字</param>
        /// <param name="btnVisible">右上角按钮的显隐性</param>
        /// <param name="modelSign">数据标识(自定义扩展内容)</param>
        public ImgDescribeModel(string imgSource, string name, bool btnVisible, object modelSign = null)
        {
            this.ImgSource = imgSource;
            this.Name = name;
            this.BtnVisible = btnVisible;
            this.ModelSign = modelSign;
        }
    }

    #region 当前用的转换器
    public class StringToImageSourceConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string path = (string)value;
                if (File.Exists(path))
                {
                    return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isShow = (bool)value;
                if (isShow)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                Visibility visibility = (Visibility)value;
                if (visibility == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    #endregion
}
