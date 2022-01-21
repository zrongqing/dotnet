using System;
using System.Collections.Generic;
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

namespace RQ.ImgDescribe
{
    using System.ComponentModel;
    using RQ.UI.ImgDescribe;
    /// <summary>
    /// ImgDescribe.xaml 的交互逻辑
    /// </summary>
    public partial class ImgDescribe : UserControl
    {
        #region Properties
        /// <summary>
        /// 控件所绑定的值
        /// </summary>
        public static readonly DependencyProperty ImgDescribeModelProperty =
            DependencyProperty.RegisterAttached(nameof(ImgDescribe.Model), typeof(ImgDescribeModel), typeof(ImgDescribe), new PropertyMetadata(new ImgDescribeModel(), OnImgDescribeDataChanged));
        private static void OnImgDescribeDataChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as ImgDescribe;
            if (element != null)
            {
                var value = e.NewValue as ImgDescribeModel;
                if (value != null)
                {
                    element.Model = value;
                    element.DataContext = value;
                }
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// 注册右上角的button的点击事件
        /// </summary>
        /// <remarks>这个返回的object对象是 <see cref="ImgDescribe()"/></remarks>
        public static readonly RoutedEvent TopRightBtnClickEvent = EventManager.RegisterRoutedEvent(nameof(TopRightBtnClickEvent), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventArgs<Object>), typeof(ImgDescribe));
        /// <summary>
        /// 处理各种路由事件的方法 
        /// </summary>
        public event RoutedEventHandler TopRightBtnClick
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(TopRightBtnClickEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(TopRightBtnClickEvent, value); }
        }
        protected virtual void OnTopRightBtnClick(object sender, RoutedEventArgs e)
        {
            TopRightBtnRoutedEvent = new RoutedEventArgs(TopRightBtnClickEvent, this);
            this.RaiseEvent(TopRightBtnRoutedEvent);
        }
        #endregion
        #region 私有变量
        private RoutedEventArgs TopRightBtnRoutedEvent;
        #endregion
        public ImgDescribeModel Model { get; set; }
        /// <summary>
        /// 图片展示 描述 右上角按钮
        /// </summary>
        /// <remarks>图片和描述默认按照 4:1 的比例展示, 右上角有一个button按钮 包含事件<see cref="TopRightBtnClick"/></remarks>
        public ImgDescribe()
        {
            InitializeComponent();
            Loaded += ImgDescribe_Loaded;
            InitEvent();
        }
        private void InitEvent()
        {
            this.btn_Extend.Click += OnTopRightBtnClick;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 为什么要在loaded中在绑定DataContext;
        /// 结论:
        /// 在构造函数中bingding会造成以下问题:
        /// 此构件为用户自定义控件,在listview初始的过程中 会调用构造函数,会覆盖xml中的绑定逻辑
        /// </remarks>
        private void ImgDescribe_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model == null)
            {
                Model = new ImgDescribeModel();
                DataContext = Model;
            }
        }
    }
}
