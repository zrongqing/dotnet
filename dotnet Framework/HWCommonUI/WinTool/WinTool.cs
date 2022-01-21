using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RQ.WinTool
{
    class WinTool
    {
        /// <summary>
        /// 启动一个新窗口
        /// </summary>
        /// <typeparam name="TWindow"></typeparam>
        /// <returns>返回窗口实例对象</returns>
        public static Task RunNewWindowAsync<TWindow>() where TWindow : System.Windows.UIElement, new()
        {
            TaskCompletionSource<TWindow> tc = new TaskCompletionSource<TWindow>();
            // 新线程
            Thread t = new Thread(() =>
            {
                TWindow win = new TWindow();
                //win.Show();
                // Run 方法必须调用，否则窗口一打开就会关闭
                // 因为没有启动消息循环
                System.Windows.Threading.Dispatcher.Run();
                // 这句话是必须的，设置Task的运算结果
                // 返回窗口对象
                tc.SetResult(win);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            // 新线程启动后，将Task实例返回
            // 以便支持 await 操作符
            return tc.Task;
        }
    }
}
