using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace UiDesktopApp.Helpers;

/// <summary>
/// 
/// </summary>
/// <remarks></remarks>
/// <inheritdoc>https://github.com/mrousavy/ClipboardMonitor/tree/master</inheritdoc>
public class ClipboardMonitor : Window
{
    private IntPtr _nextClipboardViewer;
    private HwndSource _source;

    public ClipboardMonitor()
    {
        //"show" the window in order to obtain hwnd to process WndProc messages in WPF
        Top = -10;
        Left = -10;
        Width = 0;
        Height = 0;
        WindowStyle = WindowStyle.None;
        ShowInTaskbar = false;
        ShowActivated = false;
        Show();
        Hide();
    }

    private IntPtr handle =>
        new WindowInteropHelper(this).Handle;

    #region Dependency properties

    public static readonly DependencyProperty ClipboardContainsImageProperty =
        DependencyProperty.Register("ClipboardContainsImage",
            typeof(bool),
            typeof(Window),
            new FrameworkPropertyMetadata(false));

    public static readonly DependencyProperty ClipboardContainsTextProperty =
        DependencyProperty.Register("ClipboardContainsText",
            typeof(bool),
            typeof(Window),
            new FrameworkPropertyMetadata(false));

    public static readonly DependencyProperty ClipboardTextProperty =
        DependencyProperty.Register("ClipboardText",
            typeof(string),
            typeof(Window),
            new FrameworkPropertyMetadata(string.Empty));

    public static readonly DependencyProperty ClipboardImageProperty =
        DependencyProperty.Register("ClipboardImage",
            typeof(BitmapSource),
            typeof(Window),
            new FrameworkPropertyMetadata(null));

    public bool ClipboardContainsImage
    {
        get =>
            (bool)GetValue(ClipboardContainsImageProperty);
        set =>
            SetValue(ClipboardContainsImageProperty, value);
    }
    public bool ClipboardContainsText
    {
        get =>
            (bool)GetValue(ClipboardContainsTextProperty);
        set =>
            SetValue(ClipboardContainsTextProperty, value);
    }
    public string? ClipboardText
    {
        get =>
            (string)GetValue(ClipboardTextProperty);
        set =>
            SetValue(ClipboardTextProperty, value);
    }
    public BitmapSource? ClipboardImage
    {
        get =>
            (BitmapSource)GetValue(ClipboardImageProperty);
        set =>
            SetValue(ClipboardImageProperty, value);
    }

    #endregion

    #region Routed Events

    public static readonly RoutedEvent ClipboardDataEvent = EventManager.RegisterRoutedEvent("ClipboardData", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Window));
    /// <summary>
    /// Fires upon Clipboard Content change
    /// </summary>
    public event RoutedEventHandler ClipboardData
    {
        add =>
            AddHandler(ClipboardDataEvent, value);
        remove =>
            RemoveHandler(ClipboardDataEvent, value);
    }

    protected virtual void OnRaiseClipboardData(ClipboardDataEventArgs e)
    {
        RaiseEvent(e);
    }

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

    #region overrides

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _nextClipboardViewer = SetClipboardViewer((int)handle);
        _source = PresentationSource.FromVisual(this) as HwndSource;
        _source.AddHook(WndProc);
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        ChangeClipboardChain(handle, _nextClipboardViewer);
        if (null != _source)
            _source.RemoveHook(WndProc);
    }

    #endregion

    #region Clipboard data

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        switch (msg)
        {
            case WM_DRAWCLIPBOARD:
                clipboardData();
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

    private void clipboardData()
    {
        var clipboardData = Clipboard.GetDataObject();
        if (null == clipboardData) return;

        OnRaiseClipboardData(new ClipboardDataEventArgs(ClipboardDataEvent, clipboardData));

        if (clipboardData.GetDataPresent(DataFormats.Bitmap))
        {
            ClipboardImage = ClipboardContainsImage ? clipboardData.GetData(DataFormats.Bitmap) as BitmapSource : null;
        }
        if(clipboardData.GetDataPresent(DataFormats.Text))
        {
            ClipboardText = ClipboardContainsText ? clipboardData.GetData(DataFormats.Text) as string : string.Empty;
        }
    }

    #endregion
}

public class ClipboardDataEventArgs(RoutedEvent routedEvent, IDataObject data) : RoutedEventArgs(routedEvent)
{
    public IDataObject Data { get; set; } = data;
}
