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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RQ.UI.HWProcessBar;

namespace RQ.UI
{
    /// <summary>
    /// HWProgressBar2.xaml 的交互逻辑
    /// </summary>
    public partial class HWProgressBar : UserControl
    {
        #region 当前进度
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register(nameof(Value), typeof(double), typeof(HWProgressBar),
                                          new FrameworkPropertyMetadata((double)50, OnCurValueChanged));
        /// <summary>
        /// 当前进度
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                this.progressBar.Value = value;

                //this.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, HWProgressBar.ValueProperty, value);
                //progressBar.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, ProgressBar.ValueProperty, value);
            }
        }

        private static void OnCurValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HWProgressBar progressBar = obj as HWProgressBar;
            if (progressBar != null)
            {
                var value = (double)e.NewValue;
                progressBar.Value = value;
            }
        }
        #endregion

        #region 总进度
        public static readonly DependencyProperty MaximumProperty
    = DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(HWProgressBar),
                                  new FrameworkPropertyMetadata((double)100, OnMaximumChanged));
        /// <summary>
        /// 最大进度
        /// </summary>
        public double Maximum
        {
            get
            {
                //this.Dispatcher.Invoke(() =>
                //{
                //    value = (double)this.GetValue(MaximumProperty);
                //});
                return (double)this.GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
                progressBar.Maximum = value;
                //progressBar.Dispatcher.BeginInvoke(new Action<DependencyProperty, object>(progressBar.SetValue), System.Windows.Threading.DispatcherPriority.Input, ProgressBar.MaximumProperty, value);
            }
        }

        private static void OnMaximumChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HWProgressBar progressBar = obj as HWProgressBar;
            if (progressBar != null)
            {
                var value = (double)e.NewValue;
                progressBar.Maximum = value;
            }
        }
        #endregion

        #region 进度提示1
        public static readonly DependencyProperty ProgressHintProperty
    = DependencyProperty.Register(nameof(ProgressHint), typeof(string), typeof(HWProgressBar),
                                  new FrameworkPropertyMetadata("", OnProgressHintChanged));

        private static void OnProgressHintChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HWProgressBar progressBar = (HWProgressBar)obj;
            progressBar.ProgressHint = e.NewValue?.ToString() ?? "";
        }

        public string ProgressHint
        {
            get
            {
                string value = (string)GetValue(ProgressHintProperty);
                return value;
            }
            set
            {
                SetValue(ProgressHintProperty, value);
                this.progressHint.Text = value;
            }
        }

        #endregion

        #region 进度提示2 progressDetails
        public static readonly DependencyProperty ProgressDetailsProperty
= DependencyProperty.Register(nameof(ProgressDetails), typeof(string), typeof(HWProgressBar),
                          new FrameworkPropertyMetadata("", OnProgressDetailsChanged));

        public string ProgressDetails
        {
            get { return (string)GetValue(ProgressDetailsProperty); }
            set
            {
                SetValue(ProgressDetailsProperty, value);
                this.progressDetails.Text = value;
            }
        }


        private static void OnProgressDetailsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HWProgressBar progressBar = (HWProgressBar)obj;
            progressBar.progressDetails.Text = e.NewValue.ToString();
        }
        #endregion

        #region 模式选择 HWProgressBarMode
        public static readonly DependencyProperty HWProgressBarModeProperty
= DependencyProperty.Register(nameof(HWProgressBarMode), typeof(HWProgressBarMode), typeof(HWProgressBar),
                  new FrameworkPropertyMetadata(HWProgressBarMode.Normal, OnHWProgressBarModeChanged));

        public HWProgressBarMode HWProgressBarMode
        {
            get
            {
                //HWProgressBarMode progressBarMode = HWProgressBarMode.Other;
                //this.Dispatcher.Invoke(() =>
                //{
                //    progressBarMode = (HWProgressBarMode)this.GetValue(HWProgressBarModeProperty);
                //});
                return (HWProgressBarMode)this.GetValue(HWProgressBarModeProperty);
            }
            set
            {
                SetValue(HWProgressBarModeProperty, value);
            }
        }

        public Thread thread { get; private set; }

        private static void OnHWProgressBarModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HWProgressBar progressBar = (HWProgressBar)obj;
            HWProgressBarMode progressBarMode = (HWProgressBarMode)e.NewValue;

            object parent = progressBar.Parent;
            if (progressBarMode == HWProgressBarMode.Loop)
            {
                progressBar.thread = new Thread(new ThreadStart(() =>
                {
                    double i = 0.0;
                    bool isFool = true;
                    while (isFool)
                    {

                        progressBar.Dispatcher.Invoke(() =>
                        {
                            if (i >= progressBar.Maximum)
                            {
                                i = i - progressBar.Maximum;
                            }
                            i++;
                            progressBar.Value = i;
                        });
                        System.Threading.Thread.Sleep(50);
                    }
                }));
                progressBar.thread.Name = "循环计数线程";
                progressBar.thread.IsBackground = false;
                //progressBar.thread.SetApartmentState(ApartmentState.MTA);
                progressBar.thread.Start();
            }
            else if (progressBarMode == HWProgressBarMode.Normal)
            {
                // 切换到普通模式的时候, 中断自循环流程
                if (progressBar.thread != null && progressBar.thread.IsAlive)
                {
                    progressBar.thread.Abort();
                }
            }
            progressBar.HWProgressBarMode = progressBarMode;
        }
        #endregion

        public delegate void LoopDelegate();

        public void Loop(int millisecondsTimeout = 50)
        {
            while (this.HWProgressBarMode == HWProgressBarMode.Loop)
            {
                this.Dispatcher.Invoke(() =>
                {
                    for (double i = 0; i < progressBar.Maximum; i++)
                    {
                        i = i - progressBar.Maximum;
                        progressBar.Value = i;
                    }
                });
                System.Threading.Thread.Sleep(50);
            }
        }

        public HWProgressBar()
        {
            InitializeComponent();

            FrameworkElement frameworkElement = this.Parent as FrameworkElement;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
        }

        private void UserControl_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
