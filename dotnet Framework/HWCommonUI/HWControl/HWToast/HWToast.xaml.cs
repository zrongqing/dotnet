using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RQ.UI.HWToast
{
    /// <summary>
    /// 悬浮框的按钮
    /// </summary>
    /// <remarks>
    /// 先默认没有吧， 留个接口
    /// </remarks>
    public enum ToastIcons
    {
        None,
        Information,//CheckSolid
        Error,//TimesSolid
        Warning,//ExclamationSolid
        Busy//ClockSolid
    }

    /// <summary>
    /// 位置枚举
    /// </summary>
    public enum ToastLocation
    {
        /// <summary>
        /// 父容器正中
        /// </summary>
        OwnerCenter,
        /// <summary>
        /// 父容器左中
        /// </summary>
        OwnerLeft,
        /// <summary>
        /// 父容器右中
        /// </summary>
        OwnerRight,
        /// <summary>
        /// 父容器顶左
        /// </summary>
        OwnerTopLeft,
        /// <summary>
        /// 父容器顶中
        /// </summary>
        OwnerTopCenter,
        /// <summary>
        /// 父容器顶右
        /// </summary>
        OwnerTopRight,
        /// <summary>
        /// 父容器下左
        /// </summary>
        OwnerBottomLeft,
        /// <summary>
        /// 父容器下中
        /// </summary>
        OwnerBottomCenter,
        /// <summary>
        /// 父容器下右
        /// </summary>
        OwnerBottomRight,
        /// <summary>
        /// 屏幕正中
        /// </summary>
        ScreenCenter,
        /// <summary>
        /// 屏幕正左
        /// </summary>
        ScreenLeft,
        /// <summary>
        /// 屏幕正右
        /// </summary>
        ScreenRight,
        /// <summary>
        /// 屏幕顶左
        /// </summary>
        ScreenTopLeft,
        /// <summary>
        /// 屏幕顶中
        /// </summary>
        ScreenTopCenter,
        /// <summary>
        /// 屏幕顶右
        /// </summary>
        ScreenTopRight,
        /// <summary>
        /// 屏幕底左
        /// </summary>
        ScreenBottomLeft,
        /// <summary>
        /// 屏幕底左
        /// </summary>
        ScreenBottomCenter,
        /// <summary>
        /// 屏幕底右
        /// </summary>
        ScreenBottomRight,
        /// <summary>
        /// 默认-正中
        /// </summary>
        Default//OwnerCenter
    }
    public class ToastOptions
    {
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 48;
        public int Time { get; set; } = 2000;
        /// <summary>
        /// 这个参数展示不生效
        /// </summary>
        /// <remarks>
        /// 原因， icon没有找到合适的资源
        /// </remarks>
        public ToastIcons Icon { get; set; } = ToastIcons.None;
        public ToastLocation Location { get; set; } = ToastLocation.Default;
        public Brush Foreground { get; set; } = Brushes.White;
        public FontStyle FontStyle { get; set; } = SystemFonts.MessageFontStyle;
        public FontStretch FontStretch { get; set; } = FontStretches.Normal;
        public double FontSize { get; set; } = SystemFonts.MessageFontSize;
        public FontFamily FontFamily { get; set; } = SystemFonts.MessageFontFamily;
        public FontWeight FontWeight { get; set; } = SystemFonts.MenuFontWeight;
        public double IconSize { get; set; } = 26;
        public CornerRadius CornerRadius { get; set; } = new CornerRadius(5);
        public Brush BorderBrush { get; set; }
        public Thickness BorderThickness { get; set; } = new Thickness(1);
        public Brush Background { get; set; } = (Brush)new BrushConverter().ConvertFromString("#2E2929");
        public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Center;
        public VerticalAlignment VerticalContentAlignment { get; set; } = VerticalAlignment.Center;
        public EventHandler<EventArgs> Closed { get; set; }
        public EventHandler<EventArgs> Click { get; set; }
    }

    /// <summary>
    /// Toast.xaml 的交互逻辑
    /// </summary>
    public partial class HWToast : UserControl
    {
        private Window owner;
        private Popup popup = null;
        private DispatcherTimer timer = null;

        public HWToast()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private HWToast(Window owner, string message, ToastOptions options = null)
        {
            Message = message;
            InitializeComponent();
            if (options != null)
            {
                Width = options.Width;
                Height = options.Height;
                Icon = options.Icon;
                Location = options.Location;
                Time = options.Time;
                Closed += options.Closed;
                Click += options.Click;
                Background = options.Background;
                Foreground = options.Foreground;
                FontStyle = options.FontStyle;
                FontStretch = options.FontStretch;
                FontSize = options.FontSize;
                FontFamily = options.FontFamily;
                FontWeight = options.FontWeight;
                IconSize = options.IconSize;
                BorderBrush = options.BorderBrush;
                BorderThickness = options.BorderThickness;
                HorizontalContentAlignment = options.HorizontalContentAlignment;
                VerticalContentAlignment = options.VerticalContentAlignment;
                CornerRadius = options.CornerRadius;
            }
            this.DataContext = this;
            if (owner == null)
            {
                this.owner = Application.Current.MainWindow;
            }
            else
            {
                this.owner = owner;
            }
            this.owner.Closed += Owner_Closed;
        }
        private void Owner_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void Show(string msg, ToastOptions options = null)
        {
            var toast = new HWToast(null, msg, options);
            int time = toast.Time;
            ShowToast(toast, time);
        }

        public static void Show(Window owner, string msg, ToastOptions options = null)
        {
            var toast = new HWToast(owner, msg, options);
            int time = toast.Time;
            ShowToast(toast, time);
        }

        private static void ShowToast(HWToast toast, int time)
        {
            toast.popup = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                toast.popup = new Popup
                {
                    Width = toast.Width,
                    Height = toast.Height,
                    PopupAnimation = PopupAnimation.Fade,
                    AllowsTransparency = true,
                    StaysOpen = true,
                    Placement = PlacementMode.Left,
                    IsOpen = false,
                    Child = toast
                };
                toast.popup.PlacementTarget = GetPopupPlacementTarget(toast);
                toast.owner.LocationChanged += toast.UpdatePosition;
                toast.owner.SizeChanged += toast.UpdatePosition;
                SetPopupOffset(toast.popup, toast);
                toast.popup.Closed += Popup_Closed;
                toast.popup.IsOpen = true;
            }));
            toast.timer = new DispatcherTimer();
            toast.timer.Tick += (sender, e) =>
            {
                toast.popup.IsOpen = false;
                toast.owner.LocationChanged -= toast.UpdatePosition;
                toast.owner.SizeChanged -= toast.UpdatePosition;
            };
            toast.timer.Interval = new TimeSpan(0, 0, 0, 0, time);
            toast.timer.Start();
        }

        private void UpdatePosition(object sender, EventArgs e)
        {
            var up = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (up == null || popup == null)
            {
                return;
            }
            SetPopupOffset(popup, this);
            up.Invoke(popup, null);
        }

        private static void Popup_Closed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            if (popup == null)
            {
                return;
            }
            HWToast toast = popup.Child as HWToast;
            if (toast == null)
            {
                return;
            }
            toast.RaiseClosed(e);
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                RaiseClick(e);
            }
        }

        private static UIElement GetPopupPlacementTarget(HWToast toastm)
        {
            switch (toastm.Location)
            {
                case ToastLocation.ScreenCenter:
                case ToastLocation.ScreenLeft:
                case ToastLocation.ScreenRight:
                case ToastLocation.ScreenTopLeft:
                case ToastLocation.ScreenTopCenter:
                case ToastLocation.ScreenTopRight:
                case ToastLocation.ScreenBottomLeft:
                case ToastLocation.ScreenBottomCenter:
                case ToastLocation.ScreenBottomRight:
                    return null;
            }
            return toastm.owner;
        }

        private static void SetPopupOffset(Popup popup, HWToast toast)
        {
            double owner_width = toast.owner.ActualWidth;
            double owner_height = toast.owner.ActualHeight;
            double x = SystemParameters.WorkArea.X;
            double y = SystemParameters.WorkArea.Y;
            if (popup.PlacementTarget == null)
            {
                owner_width = SystemParameters.WorkArea.Size.Width;
                owner_height = SystemParameters.WorkArea.Size.Height;
            }
            switch (toast.Location)
            {
                case ToastLocation.OwnerLeft:
                    popup.HorizontalOffset = popup.Width;
                    popup.VerticalOffset = (owner_height - popup.Height - 38) / 2;
                    break;
                case ToastLocation.ScreenLeft:
                    popup.HorizontalOffset = popup.Width + x;
                    popup.VerticalOffset = (owner_height - popup.Height) / 2 + y;
                    break;
                case ToastLocation.OwnerRight:
                    popup.HorizontalOffset = owner_width - 16;
                    popup.VerticalOffset = (owner_height - popup.Height - 38) / 2;
                    break;
                case ToastLocation.ScreenRight:
                    popup.HorizontalOffset = owner_width + x;
                    popup.VerticalOffset = (owner_height - popup.Height) / 2 + y;
                    break;
                case ToastLocation.OwnerTopLeft:
                    popup.HorizontalOffset = popup.Width;
                    break;
                case ToastLocation.ScreenTopLeft:
                    popup.HorizontalOffset = popup.Width + x;
                    popup.VerticalOffset = y;
                    break;
                case ToastLocation.OwnerTopCenter:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width - 16) / 2;
                    break;
                case ToastLocation.ScreenTopCenter:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width) / 2 + x;
                    popup.VerticalOffset = y;
                    break;
                case ToastLocation.OwnerTopRight:
                    popup.HorizontalOffset = owner_width - 16;
                    break;
                case ToastLocation.ScreenTopRight:
                    popup.HorizontalOffset = owner_width + x;
                    popup.VerticalOffset = y;
                    break;
                case ToastLocation.OwnerBottomLeft:
                    popup.HorizontalOffset = popup.Width;
                    popup.VerticalOffset = owner_height - popup.Height - 38;
                    break;
                case ToastLocation.ScreenBottomLeft:
                    popup.HorizontalOffset = popup.Width + x;
                    popup.VerticalOffset = owner_height - popup.Height + y;
                    break;
                case ToastLocation.OwnerBottomCenter:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width - 16) / 2;
                    popup.VerticalOffset = owner_height - popup.Height - 38;
                    break;
                case ToastLocation.ScreenBottomCenter:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width) / 2 + x;
                    popup.VerticalOffset = owner_height - popup.Height + y;
                    break;
                case ToastLocation.OwnerBottomRight:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width - 16);
                    popup.VerticalOffset = owner_height - popup.Height - 38;
                    break;
                case ToastLocation.ScreenBottomRight:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width) + x;
                    popup.VerticalOffset = owner_height - popup.Height + y;
                    break;
                case ToastLocation.ScreenCenter:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width) / 2 + x;
                    popup.VerticalOffset = (owner_height - popup.Height) / 2 + y;
                    break;
                case ToastLocation.OwnerCenter:
                case ToastLocation.Default:
                    popup.HorizontalOffset = popup.Width + (owner_width - popup.Width - 16) / 2;
                    popup.VerticalOffset = (owner_height - popup.Height - 38) / 2;
                    break;
            }
        }

        public void Close()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            popup.IsOpen = false;
            owner.LocationChanged -= UpdatePosition;
            owner.SizeChanged -= UpdatePosition;
        }

        private event EventHandler<EventArgs> Closed;
        private void RaiseClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        private event EventHandler<EventArgs> Click;
        private void RaiseClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        #region 注册控件属性
        /// <summary>
        /// 消息
        /// </summary>
        private string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(HWToast), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 圆角
        /// </summary>
        private CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        private static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(HWToast), new PropertyMetadata(new CornerRadius(5)));

        /// <summary>
        /// 图标的大小
        /// </summary>
        private double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        private static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(HWToast), new PropertyMetadata(26.0));

        /// <summary>
        /// 边框的画笔
        /// </summary>
        private new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        private static new readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(HWToast), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#FFFFFF")));

        /// <summary>
        /// 边框的粗细
        /// </summary>
        private new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        private static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(HWToast), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 背景颜色
        /// </summary>
        private new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        private static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(HWToast), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#2E2929")));

        /// <summary>
        /// 内容水平对齐
        /// </summary>
        private new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        private static new readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(HWToast), new PropertyMetadata(HorizontalAlignment.Left));

        /// <summary>
        /// 内容垂直对齐
        /// </summary>
        private new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        private static new readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(HWToast), new PropertyMetadata(VerticalAlignment.Center));

        /// <summary>
        /// 宽度
        /// </summary>
        private new double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        private new static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(HWToast), new PropertyMetadata(200.0));

        /// <summary>
        /// 高度
        /// </summary>
        private new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        private new static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(HWToast), new PropertyMetadata(48.0));

        /// <summary>
        /// 图标
        /// </summary>
        private ToastIcons Icon
        {
            get { return (ToastIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ToastIcons), typeof(HWToast), new PropertyMetadata(ToastIcons.None));

        /// <summary>
        /// 消失的时间
        /// </summary>
        private int Time
        {
            get { return (int)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        private static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(int), typeof(HWToast), new PropertyMetadata(2000));

        /// <summary>
        /// 出现的位置
        /// </summary>
        private ToastLocation Location
        {
            get { return (ToastLocation)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        private static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(ToastLocation), typeof(HWToast), new PropertyMetadata(ToastLocation.Default));
        #endregion

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
