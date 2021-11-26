using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFCommonUI.Windows
{
    /// <summary>
    /// ProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class HWProgressBar : Window
    {
        /// <summary>
        /// 当前进度
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                progressBar.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, ProgressBar.ValueProperty, _value);
            }
        }
        private double _value;
        /// <summary>
        /// 总进度值
        /// </summary>
        public double Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                progressBar.Dispatcher.BeginInvoke(new Action<DependencyProperty, object>(progressBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, ProgressBar.MaximumProperty, _maximum);
            }
        }
        private double _maximum;
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string StrTitle { get; set; }
        /// <summary>
        /// 进度条上方文字
        /// </summary>
        public string StrTitle1 { get; set; }
        /// <summary>
        /// 进度条下方文字
        /// </summary>
        public string StrTitle2 { get; set; }
        public HWProgressBarMode Model { get; set; }

        /// <summary>
        /// 循环计数_标识
        /// </summary>
        private bool IsReput { get; set; }
        /// <summary>
        /// 循环进度条线程
        /// </summary>

        #region 静态 假单例
        private static HWProgressBar _Instance;
        static object _locker = new object();
        public static HWProgressBar Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_Instance == null)
                    {
                        _Instance = HWProgressBar.CreateInstance();
                    }
                    else
                    {
                        return _Instance;
                        //if (!_Instance.IsLoaded)
                        //{
                        //    _Instance = ProgressBar.CreateInstance();
                        //}
                    }
                    return _Instance;
                }
            }
        }

        /// <summary>
        /// 单例所使用的线程
        /// </summary>
        private static Thread StaticThread = null;

        /// <summary>
        /// 该方法创建的实例对线在线程中,创建完成后打开窗口
        /// </summary>
        /// <returns></returns>
        private static HWProgressBar CreateInstance()
        {
            HWProgressBar progressBar = null;

            HWProgressBar.StaticThread = new Thread(new ThreadStart(() =>
            {
                progressBar = new HWProgressBar();
                progressBar.Show();
                // 启动消息循环
                System.Windows.Threading.Dispatcher.Run();
            }));
            HWProgressBar.StaticThread.SetApartmentState(ApartmentState.STA);
            HWProgressBar.StaticThread.Name = $"{nameof(HWProgressBar)}_{nameof(HWProgressBar.CreateInstance)}";
            HWProgressBar.StaticThread.Priority = ThreadPriority.Highest;
            HWProgressBar.StaticThread.IsBackground = true;
            HWProgressBar.StaticThread.Start();

            // 在这里需要卡死主线程等在线程中的UI创建完毕
            Task task = Task.Run(() =>
            {
                while (progressBar == null)
                {
                    Thread.Sleep(500);
                }
            });
            bool isCreateUI = task.Wait(2000);

            if (isCreateUI && progressBar != null)
            {
                return progressBar;
            }
            else
            {
                throw new System.AggregateException("创建UI界面失败");
            }
        }
        #endregion

        /// <summary>
        /// 开始进度条循环
        /// <see cref="HWProgressBar.CloseLoop">停止进度条循环</see>
        /// </summary>
        public void StartLoop()
        {
            if(!this.IsActive)
            {
                this.Show();
            }

            Model = HWProgressBarMode.Loop;
            IsReput = true;
            HWProgressBar.StaticThread = new Thread(new ThreadStart(() =>
            {
                double i = 0.0;
                while (IsReput)
                {
                    if (i >= Maximum)
                    {
                        i = i - Maximum;
                    }
                    i++;
                    Value = i;
                    System.Threading.Thread.Sleep(50);
                }
            }));
            HWProgressBar.StaticThread.SetApartmentState(ApartmentState.STA);
            HWProgressBar.StaticThread.Name = $"{nameof(HWProgressBar)}_{nameof(HWProgressBar.StartLoop)}";
            HWProgressBar.StaticThread.Start();
        }
        /// <summary>
        /// 关闭循环进度条
        /// <see cref="HWProgressBar.StartLoop">开始进度条循环</see>
        /// </summary>
        public void CloseLoop()
        {
            Dispatcher.Invoke(() => Close());
        }
        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="title1">进度条上方文字    $"正在生成 {title1} 中......"</param>
        /// <param name="stitle2">进度条下方文字    $"已经生成 {this.Value} / {this.Maximum} {StrTitle2}";</param>
        public void SetPromptMessage(string title, string title1, string stitle2)
        {
            StrTitle = title;
            StrTitle1 = title1;
            StrTitle2 = stitle2;
            UpdataPromptMessage();
        }

        private void UpdataPromptMessage()
        {
            Dispatcher.Invoke(() =>
            {
                this.Title = StrTitle;
                this.labHint.Content = $"正在生成{StrTitle1}中......";
                switch (Model)
                {
                    case HWProgressBarMode.Normal:
                        labProgress.Content = $"已经生成 {this.Value}/{this.Maximum} {StrTitle2}";
                        break;
                    case HWProgressBarMode.Loop:
                        labProgress.Content = $"正在生成中,请耐心等待..";
                        break;
                    default:
                        break;
                }
            });
        }
        private HWProgressBar()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            StrTitle = "进度条";
            StrTitle1 = string.Empty;
            StrTitle2 = string.Empty;
            Model = HWProgressBarMode.Normal;
            Maximum = 100;
            IsReput = false;
            btnOK.Visibility = Visibility.Hidden;
            Value = 0;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            _Instance = null;
            System.Windows.Threading.Dispatcher.ExitAllFrames();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseLoop();
        }
        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdataPromptMessage();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 退出线程
            if (HWProgressBar.StaticThread != null && HWProgressBar.StaticThread.IsAlive)
            {
                HWProgressBar.StaticThread.Abort();
                HWProgressBar.StaticThread.Join();
            }

            IsReput = false;
        }
    }
}
