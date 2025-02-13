using System.Text;
using Microsoft.Extensions.Logging;
using UiDesktopApp.Helpers;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp.ViewModels.Pages;

public partial class ToolViewModel : ViewModel
{
    private ClipboardMonitor? _clipbordMonitor;

    [ObservableProperty] private bool _isCharacterToEn;

    private ILogger? _logger;

    public Task OnNavigatedToAsync()
    {
       return Task.CompletedTask;
    }

    public Task OnNavigatedFromAsync()
    {
        return Task.CompletedTask;
    }


    [RelayCommand]
    private void OnChangeCharsToEn()
    {
        if (IsCharacterToEn)
        {
            _clipbordMonitor = new ClipboardMonitor();
            _clipbordMonitor.ClipboardData += OnClipboardChanged;
        }
        else if (_clipbordMonitor != null)
        {
            _clipbordMonitor.ClipboardData -= OnClipboardChanged;
            _clipbordMonitor.Close();
            _clipbordMonitor = null;
        }
    }

    private void OnClipboardChanged(object sender, RoutedEventArgs e)
    {
        var args = e as ClipboardDataEventArgs;
        if (null == args) return;
        //MessageBox.Show($"Clipboard Changed! {args.Data.ToString()}");
        var clipboardContainsText = args.Data.GetDataPresent(DataFormats.Text);
        if (!clipboardContainsText) return;

        var clipboardText = args.Data.GetData(DataFormats.Text) as string;
        if (null == clipboardText) return;

        // 讲中文字符转换成英语字符
        var stringBuilder = new StringBuilder(clipboardText);
        stringBuilder.Replace("（", "(");
        stringBuilder.Replace("）", ")");

        Clipboard.SetText(stringBuilder.ToString());
        e.Handled = true;
    }
}