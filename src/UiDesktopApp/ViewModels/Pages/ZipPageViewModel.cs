using System.IO;
using Microsoft.Win32;

namespace UiDesktopApp.ViewModels.Pages;

public partial class ZipPageViewModel : ViewModel
{
    [ObservableProperty] private string _openedFilePath = string.Empty;

    [ObservableProperty] private string _pageLog = "This is page log";

    [RelayCommand]
    private void OnOpenZipFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "ZIP files (*.ZIP)|*.ZIP"
            };

        if (openFileDialog.ShowDialog() != true) return;

        if (!File.Exists(openFileDialog.FileName)) return;

        OpenedFilePath = openFileDialog.FileName;
    }
}