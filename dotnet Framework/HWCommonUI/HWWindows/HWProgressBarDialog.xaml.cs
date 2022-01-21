using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using RQ.HWComponentModel;

namespace RQ.HWWindows
{
    /// <summary>
    /// HWProgressBarTemp.xaml 的交互逻辑
    /// </summary>
    /// <remarks>
    /// 内部已经做好数据绑定, 数据给HWProgressBarDialogData就可以
    /// </remarks>
    public partial class HWProgressBarDialog : Window
    {
        /// <summary>
        /// 数据接口
        /// </summary>
        public HWProgressBarDialogData HWProgressBarDialogData { get; set; } = new HWProgressBarDialogData();

        public static bool IsClose = false;

        #region 静态 假单例
        private static HWProgressBarDialog _Instance { get; set; }
        static object _locker = new object();
        public static HWProgressBarDialog Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_Instance == null)
                    {
                        HWProgressBarDialog.IsClose = false;
                        _Instance = HWProgressBarDialog.CreateInst();
                    }
                    else
                    {
                        return _Instance;
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
        public static HWProgressBarDialog CreateInst()
        {
            HWProgressBarDialog progressBar = null;

            HWProgressBarDialog.StaticThread = new Thread(new ThreadStart(() =>
            {
                // 建立一个循环线程

                progressBar = new HWProgressBarDialog();

                // 启动消息循环
                System.Windows.Threading.Dispatcher.Run();
            }));

            HWProgressBarDialog.StaticThread.SetApartmentState(ApartmentState.STA);
            HWProgressBarDialog.StaticThread.Name = "ProgressBarThread";//$"{nameof(HWProgressBarDialog)}_{nameof(HWProgressBarDialog.CreateInst)}";
            HWProgressBarDialog.StaticThread.Priority = ThreadPriority.Normal;
            HWProgressBarDialog.StaticThread.IsBackground = true;
            HWProgressBarDialog.StaticThread.Start();

            // 在这里需要卡死主线程等在线程中的UI创建完毕
            Task task = Task.Run(() =>
            {
                while (progressBar == null)
                {
                    Thread.Sleep(100);
                }
            });
            bool isCreateUI = task.Wait(2000);

            if (isCreateUI && progressBar != null)
            {
                return progressBar;
            }
            else
            {
                throw new System.NotImplementedException("创建进度条失败界面失败");    //2s等待应该是创建好了才对
            }
        }
        /// <summary>
        /// 线程封装
        /// </summary>
        /// <returns></returns>
        /// <remarks>这个函数在这里并不是适合, 先放在这, 删除也没什么</remarks>
        private static Task<object> RunNewWindowAsync<TWindow>() where TWindow : System.Windows.Window, new()
        {
            TaskCompletionSource<object> tc = new TaskCompletionSource<object>();
            // 新线程
            Thread t = new Thread(() =>
            {
                TWindow win = new TWindow();
                win.Show();
                // 启动消息循环队列
                System.Windows.Threading.Dispatcher.Run();
                // 这句话是必须的，设置Task的运算结果
                // 但由于此处不需要结果, 返回窗口的对象
                tc.SetResult(win);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            // 新线程启动后，将Task实例返回
            // 以便支持 await 操作符
            return tc.Task;
        }
        #endregion

        /// <summary>
        /// 普通启动
        /// </summary>
        public new void Show()
        {
            Dispatcher.Invoke(() =>
            {
                base.Show();
            });
        }

        /// <summary>
        /// 自循环启动
        /// </summary>
        public void StartLoop()
        {
            this.Show();

            // 再启动一个线程进行自循环
            this.hwBar.Dispatcher.Invoke(new Action<DependencyProperty, object>(hwBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, UI.HWProgressBar.HWProgressBarModeProperty, RQ.UI.HWProcessBar.HWProgressBarMode.Loop);
        }

        public HWProgressBarDialog()
        {
            InitializeComponent();

            this.DataContext = HWProgressBarDialogData;
        }
        public string Hint { get => HWProgressBarDialogData.Hint; set => HWProgressBarDialogData.Hint = value; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            HWProgressBarDialogData.CurValue++;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((ThreadStart)delegate
            {
                // 退出线程
                if (HWProgressBarDialog.StaticThread != null && HWProgressBarDialog.StaticThread.IsAlive)
                {
                    HWProgressBarDialog.StaticThread.Abort();
                }
            }, DispatcherPriority.Normal);

            if (this.hwBar.thread != null && this.hwBar.thread.IsAlive)
            {
                this.hwBar.thread.Abort();
            }
            HWProgressBarDialog.IsClose = true;
            _Instance = null;   // 置位null
        }
    }

    public class HWProgressBarDialogData : NotifyPropertyBase
    {
        private double curValue = 20;
        private double maxValue = 100;
        private string hint = "在做什么";
        private string details = "生成的咋样了";


        /// <summary>
        /// 当前的值
        /// </summary>
        public double CurValue
        {
            get => curValue;
            set
            {
                curValue = value;
                base.OnPropertyChanged(nameof(CurValue));
            }
        }

        /// <summary>
        /// 最大的值
        /// </summary>
        public double MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                base.OnPropertyChanged(nameof(MaxValue));
            }
        }

        /// <summary>
        /// 进度条提示
        /// </summary>
        public string Hint
        {
            get => hint; set
            {
                hint = value;
                base.OnPropertyChanged(nameof(Hint));
            }
        }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Details
        {
            get => details; set
            {
                details = value;
                base.OnPropertyChanged(nameof(Details));
            }
        }

        public HWProgressBarDialogData()
        {
        }
    }

    public class HWProgressBarExction : Exception
    {
        public HWProgressBarExction(string message) : base(message)
        {

        }
    }
}
