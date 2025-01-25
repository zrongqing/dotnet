using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UiDesktopApp.Helpers;
using Wpf.Ui.Controls;

namespace UiDesktopApp.ViewModels.Pages
{
    public partial class ToolViewModel : ObservableObject, INavigationAware
    {
        private ILogger? _logger;

        public ToolViewModel() 
        { 
        }

        [ObservableProperty]
        private bool _isCharacterToEn = false;

        private ClipboardMonitor? _clipbordMonitor;

        public void OnNavigatedTo() 
        {

        }

        public void OnNavigatedFrom()
        {
            
        }

        [RelayCommand]
        private void OnChangeCharsToEn()
        {
            if (IsCharacterToEn == true)
            {
                _clipbordMonitor = new ClipboardMonitor();
                _clipbordMonitor.ClipboardData += OnClipboardChanged;
            }
            else if(_clipbordMonitor != null)
            {
                _clipbordMonitor.ClipboardData -= OnClipboardChanged;
                _clipbordMonitor.Close();
                _clipbordMonitor = null;
            }
        }

        private void OnClipboardChanged(object sender, RoutedEventArgs e)
        {
            ClipboardDataEventArgs? args = e as ClipboardDataEventArgs;
            if (null == args)
            {
                return;
            }
            //MessageBox.Show($"Clipboard Changed! {args.Data.ToString()}");
            bool clipboardContainsText = args.Data.GetDataPresent(DataFormats.Text);
            if (!clipboardContainsText) return;

            string? clipboardText = args.Data.GetData(DataFormats.Text) as string;
            if (null == clipboardText) return;

            // 讲中文字符转换成英语字符
            StringBuilder stringBuilder = new StringBuilder(clipboardText);
            stringBuilder.Replace("（", "(");
            stringBuilder.Replace("）", ")");

            Clipboard.SetText(stringBuilder.ToString());
            e.Handled = true;
        }
    }
}
