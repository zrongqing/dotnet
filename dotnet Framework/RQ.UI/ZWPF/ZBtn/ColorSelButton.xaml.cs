using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RQ.UI.ZWPF.ZBtn
{
    /// <summary>
    /// ColorSelButton.xaml 的交互逻辑
    /// </summary>
    /// <remarks>
    /// 颜色选择控件. 通过点击,打开颜色表进行选择, 颜色表是.net自带的
    /// </remarks>
    public partial class ColorSelButton : UserControl, INotifyPropertyChanged
    {
        #region properties

        #region 选择的颜色
        public static readonly DependencyProperty SelColorProperty = DependencyProperty.Register(nameof(ColorSelButton.SelColor), typeof(System.Windows.Media.Color), typeof(ColorSelButton), new PropertyMetadata(new System.Windows.Media.Color(), OnSelColorChanged));

        private static void OnSelColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ColorSelButton;
            if (element != null)
            {
                Color color = (System.Windows.Media.Color)e.NewValue;
                if (color != null)
                {
                    element.SelColor = color;

                    SolidColorBrush brush = new SolidColorBrush(color);
                    element.Btn_LineColor.Foreground = brush;

                    string colorStr = "RGB  " + color.R.ToString() + "-" + color.G.ToString() + "-" + color.B.ToString();
                    element.Btn_LineColor.Content = colorStr;
                }
                else
                {
                    element.Btn_LineColor.Content = "<无替换>";
                }
            }
        }
        #endregion

        #endregion

        public System.Windows.Media.Color SelColor
        {
            get
            {
                return (System.Windows.Media.Color)GetValue(SelColorProperty);
            }

            set
            {
                SetValue(SelColorProperty, value);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorSelButton()
        {
            InitializeComponent();
            this.SelColor = System.Windows.Media.Color.FromRgb(0, 0, 0);
            this.Loaded += ColorSelButton_Loaded;
        }

        private void ColorSelButton_Loaded(object sender, RoutedEventArgs e)
        {
            // 这里赋值是为了触发 OnSelColorChanged 事件,让字符串能够刷新
        }

        private void btn_LineColor_Click(object sender, RoutedEventArgs e)
        {
            byte R = 128;
            byte G = 128;
            byte B = 128;

            System.Windows.Forms.ColorDialog colordlg = new System.Windows.Forms.ColorDialog();
            colordlg.Color = System.Drawing.Color.FromArgb(R, G, B);
            //允许使用该对话框的自定义颜色  
            colordlg.AllowFullOpen = true;
            colordlg.FullOpen = true;
            if (colordlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获取所选择的颜色
                System.Drawing.Color colorChoosed = colordlg.Color;

                SelColor = Color.FromArgb(colorChoosed.A, colorChoosed.R, colorChoosed.G, colorChoosed.B);
            }
        }


    }

    /// <summary>
    /// 将Color的颜色转换成指定字符串
    /// </summary>
    /// <see cref="RQ.HWUserControl.ColorSelButton"/>
    public class ColorSelBtnToStrConvt : System.Windows.Data.IValueConverter
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
