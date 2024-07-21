using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using UiDesktopApp.Helpers;
using UiDesktopApp.ViewModels.Pages;
using UiDesktopApp.Views.Windows;
using Wpf.Ui.Controls;

namespace UiDesktopApp.Views.Pages;

/// <summary>
/// ToolPage.xaml 的交互逻辑
/// </summary>
public partial class ToolPage : INavigableView<ToolViewModel>
{
    public ToolPage(ToolViewModel toolViewModel)
    {
        ViewModel = toolViewModel;
        DataContext = this;

        InitializeComponent();

        var mainWindwos = App.Current.MainWindow;
        var handle = new  WindowInteropHelper(mainWindwos).Handle;

        _nextClipboardViewer = (IntPtr)SetClipboardViewer((int)Process.GetCurrentProcess().Handle);
        _nextClipboardViewer = (IntPtr)SetClipboardViewer((int)handle);

        _source = PresentationSource.FromVisual(mainWindwos) as HwndSource;
    }

    private HwndSource _source;

    #region Clipboard data
    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        switch (msg)
        {
            case WM_DRAWCLIPBOARD:
                OnClipboardChanged();
                SendMessage(_nextClipboardViewer, msg, wParam, lParam);
                break;
            case WM_CHANGECBCHAIN:
                if (wParam == _nextClipboardViewer)
                    _nextClipboardViewer = lParam;
                else
                    SendMessage(_nextClipboardViewer, msg, wParam, lParam);
                break;
        }
        return IntPtr.Zero;
    }
    private void OnClipboardChanged()
    {
        if (Clipboard.ContainsText())
        {
            string text = Clipboard.GetText();
            // Modify the text as needed
            string modifiedText = ModifyText(text);

            // modify before removeHook 避免修改后剪贴板后的再次触发
            _source.RemoveHook(WndProc);
            Clipboard.SetText(modifiedText);
            _source.AddHook(WndProc);
        }

        string ModifyText(string text)
        {
            StringBuilder stringBuilder = new StringBuilder(text);
            stringBuilder.Replace("（", "(");
            stringBuilder.Replace("）", ")");
            // Implement your text modification logic here
            return stringBuilder.ToString();
        }

        //IDataObject iData = Clipboard.GetDataObject();
        //this.ClipboardContainsImage = iData.GetDataPresent(DataFormats.Bitmap);
        //this.ClipboardContainsText = iData.GetDataPresent(DataFormats.Text);
        //this.ClipboardImage = this.ClipboardContainsImage ? iData.GetData(DataFormats.Bitmap) as BitmapSource : null;
        //this.ClipboardText = this.ClipboardContainsText ? iData.GetData(DataFormats.Text) as string : string.Empty;
        //OnRaiseClipboardData(new ClipboardDataEventArgs(ClipboardMonitor.ClipboardDataEvent, iData));
    }
    #endregion

    private IntPtr _nextClipboardViewer;

    #region INavigableView<ToolViewModel> Members

    public ToolViewModel ViewModel { get; }

    #endregion

    #region Win32 API
    private const int WM_DRAWCLIPBOARD = 0x308;
    private const int WM_CHANGECBCHAIN = 0x030D;
    [DllImport("User32.dll")]
    private static extern int SetClipboardViewer(int hWndNewViewer);
    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
    #endregion

    private void CBCharsToEn_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.IsCharacterToEn)
        {
            _source.AddHook(WndProc);
        }
        else
        {
            _source.RemoveHook(WndProc);
        }
    }
}
