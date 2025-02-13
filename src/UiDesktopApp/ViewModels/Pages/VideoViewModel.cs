using System.IO;
using System.Text;
using FFMpegCore;
using FFMpegCore.Enums;
using Microsoft.Extensions.Logging;
using UiDesktopApp.Helpers;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiDesktopApp.ViewModels.Pages;

public partial class VideoViewModel : ViewModel
{
    private readonly ISnackbarService  _snackbar;
    private          ClipboardMonitor? _clipbordMonitor;

    [ObservableProperty]
    private bool _isCharacterToEn;

    [ObservableProperty]
    private string _convertMessage;

    private ILogger? _logger;

    private readonly ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;

    [ObservableProperty]
    private int _snackbarTimeout = 1;

    [ObservableProperty]
    private string _videoDirPath;

    public VideoViewModel(ISnackbarService snackbar)
    {
        _snackbar = snackbar;
    }

    public Task OnNavigatedToAsync()
    {
        return Task.CompletedTask;
    }

    public Task OnNavigatedFromAsync()
    {
        return Task.CompletedTask;
    }

    partial void OnVideoDirPathChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _videoDirPath = "请输出转换的视频地址";
        }
    }

    [RelayCommand]
    private async Task VideoToMp4()
    {
        string videoDirPath = this.VideoDirPath;

        // setting global options
        GlobalFFOptions.Configure(new FFOptions { BinaryFolder = @"C:\ProgramData\chocolatey\bin", TemporaryFilesFolder = "/tmp" });
        
        videoDirPath = Path.Combine(@"C:\Users\zrq\Videos\kurogames\Wuthering Waves");
        
        if (string.IsNullOrEmpty(videoDirPath))
        {
            _snackbar.Show("ConvertMp4", "选择的路径不对", _snackbarAppearance, null, TimeSpan.FromSeconds(SnackbarTimeout));
            return;
        }

        if (!Directory.Exists(videoDirPath))
        {
            _snackbar.Show("ConvertMp4", "选择的路径不对", _snackbarAppearance, null, TimeSpan.FromSeconds(SnackbarTimeout));
            return;
        }

        var outVideoDirPath = Path.Combine(videoDirPath, "out_mp4");
        Directory.CreateDirectory(outVideoDirPath);
        
        StringBuilder stringBuilder = new();

        foreach (var filePath in Directory.GetFiles(videoDirPath, "*", SearchOption.AllDirectories))
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            
            var inputFilePath = filePath;
            var outputPath = Path.Combine(outVideoDirPath, $"{fileName}.mp4");
            
            this.ConvertMessage = stringBuilder.AppendLine($"ConvertMp4 {fileName}").ToString();
            var isConvert = FFMpeg.Convert(inputFilePath, outputPath, FFMpegCore.Enums.VideoType.Mp4);
            if (isConvert)
            {
                this.ConvertMessage = stringBuilder.AppendLine($"ConvertMp4 {fileName} 转换成功 {outputPath}").ToString();
            }
            else
            {
                this.ConvertMessage = stringBuilder.AppendLine($"ConvertMp4 {fileName} 转换失败 {outputPath}").ToString();
            }
        }
        
        _snackbar.Show(
            "Don't Blame Yourself.",
            "No Witcher's Ever Died In His Bed.",
            _snackbarAppearance,
            new SymbolIcon(SymbolRegular.Fluent24),
            TimeSpan.FromSeconds(SnackbarTimeout)
        );
    }
}