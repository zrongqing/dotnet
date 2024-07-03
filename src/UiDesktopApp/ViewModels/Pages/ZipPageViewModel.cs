using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesktopApp.ViewModels.Pages
{
    public partial class ZipPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _pageLog = "This is page log";

        [ObservableProperty]
        private string _openedFilePath = string.Empty;

        [RelayCommand]
        private void OnOpenZipFile()
        {
            OpenFileDialog openFileDialog =
                new()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "ZIP files (*.ZIP)|*.ZIP"
                };

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            if (!File.Exists(openFileDialog.FileName))
            {
                return;
            }

            OpenedFilePath = openFileDialog.FileName;
        }
    }
}
