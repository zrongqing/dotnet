#region

using System.Diagnostics;
using System.Runtime.InteropServices;

#endregion

namespace ZRQ.Utils;

public static class FileUtils
{
    /// <summary>
    /// 复制文件夹
    /// </summary>
    /// <param name="sourceDir"> 原始文件夹 </param>
    /// <param name="destDir"> 目标文件夹 </param>
    /// <exception cref="DirectoryNotFoundException"> </exception>
    public static void CopyDirectory(string sourceDir, string destDir)
    {
        // Get the subdirectories for the specified directory.
        var dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDir);

        var dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);

        // Get the files in the directory and copy them to the new location.
        var files = dir.GetFiles();
        foreach (var file in files)
        {
            var tempPath = Path.Combine(destDir, file.Name);
            file.CopyTo(tempPath, false);
        }

        // Recursively copy subdirectories.
        foreach (var subDir in dirs)
        {
            var tempPath = Path.Combine(destDir, subDir.Name);
            CopyDirectory(subDir.FullName, tempPath);
        }
    }

    /// <summary>
    /// 如果路径不存在就创建目录
    /// </summary>
    public static bool CreateDir(string dirPath)
    {
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        return true;
    }

    /// <summary>
    /// 创建文件 并解除占用
    /// </summary>
    /// <param name="filePath"> 文件的路径 </param>
    /// <returns> 根据filePath的位置 自动创建文件夹以及文件 </returns>
    public static void CreateFile(string filePath)
    {
        var dirPath = Path.GetDirectoryName(filePath);
        if (null == dirPath) return;

        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        using var fileStream = File.Create(filePath);
        fileStream.Close();
    }

    /// <summary>
    /// 得到文件夹下的所有文件(包括字文件夹)
    /// </summary>
    public static string[] GetFilesRecursively(string path)
    {
        // 获取当前目录下的所有文件
        var files = Directory.GetFiles(path);

        // 获取当前目录下的所有子目录
        var directories = Directory.GetDirectories(path);

        // 遍历所有子目录，并递归调用自己获取子目录中的文件
        foreach (var directory in directories)
        {
            var subFiles = GetFilesRecursively(directory);
            files = files.Concat(subFiles).ToArray();
        }

        return files;
    }

    /// <summary>
    /// 判断文件是否正在被使用
    /// </summary>
    public static bool IsFileInUse(string fileName)
    {
        var handle = CreateFile(fileName, 0x80000000, 0, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
        if (handle.ToInt32() == -1)
            return true;

        var bhfi = new BY_HANDLE_FILE_INFORMATION();
        GetFileInformationByHandle(handle, out bhfi);
        CloseHandle(handle);

        return false;
    }

    /// <summary>
    /// 判断文件是否被占用
    /// </summary>
    public static bool IsFileOccupied(string fileName)
    {
        var vHandle = CreateFile(fileName,
            GENERIC_READ | GENERIC_WRITE,
            0,
            IntPtr.Zero,
            OPEN_EXISTING,
            0,
            IntPtr.Zero);
        if (vHandle == new IntPtr(INVALID_HANDLE_VALUE))
        {
            var errorCode = Marshal.GetLastWin32Error();
            if (errorCode == ERROR_SHARING_VIOLATION) return true; // 文件被占用

            return false; // 文件不存在或其他原因
        }

        CloseHandle(vHandle);
        return false; // 文件没有被占用
    }

    /// <summary>
    /// 打开文件选择器
    /// </summary>
    /// <param name="fileFullName"> </param>
    public static void OpenFolderAndSelectFile(string fileFullName)
    {
        var psi = new ProcessStartInfo("Explorer.exe")
        {
            Arguments = "/e,/select," + fileFullName
        };
        Process.Start(psi);
    }

    /// <summary>
    /// 复制文件夹(递归)
    /// </summary>
    /// <param name="sourceFolderName"> 原文件夹路径 </param>
    /// <param name="destFolderName"> 目标文件夹路径 </param>
    /// <param name="overwrite"> 是否覆盖 </param>
    /// <param name="arrExtension"> 指定复制的文件后缀 </param>
    /// <returns> 返回提示信息，成功，返回"" </returns>
    private static bool CopyFolder(string sourceFolderName, string destFolderName, bool overwrite = false,
        string[]? arrExtension = null)
    {
        // 先复制当前文件夹中的文件
        var list = Directory.GetFiles(sourceFolderName);
        if (list.Length > 0)
            foreach (var item in list)
            {
                var strExtenion = Path.GetExtension(item).ToLower();
                if (arrExtension != null && !arrExtension.Contains(strExtenion)) continue;

                var sourceFileName = Path.GetFileName(item);
                File.Copy(item, Path.Combine(destFolderName, sourceFileName), overwrite);
            }


        var listDir = Directory.GetDirectories(sourceFolderName);
        if (listDir.Length == 0) return true;


        foreach (var item in listDir)
        {
            var forlders = item.Split('\\');
            var lastDirectory = forlders[forlders.Length - 1];
            var dest = Path.Combine(destFolderName, lastDirectory);
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);

            CopyFolder(item, dest, overwrite, arrExtension);
        }


        return true;
    }

    #region kernel32

    [StructLayout(LayoutKind.Sequential)]
    public struct FILETIME
    {
        public uint dwLowDateTime;
        public uint dwHighDateTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BY_HANDLE_FILE_INFORMATION
    {
        public uint dwFileAttributes;
        public FILETIME ftCreationTime;
        public FILETIME ftLastAccessTime;
        public FILETIME ftLastWriteTime;
        public uint dwVolumeSerialNumber;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint nNumberOfLinks;
        public uint nFileIndexHigh;
        public uint nFileIndexLow;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern uint GetFileAttributes(string lpFileName);

    [DllImport("kernel32.dll")]
    public static extern bool
        GetFileInformationByHandle(IntPtr hFile, out BY_HANDLE_FILE_INFORMATION lpFileInformation);

    /// <summary>
    /// 判断文件是否是系统文件或者是只读文件
    /// </summary>
    public static bool IsSystemOrReadOnlyFile(string filePath)
    {
        var fileAttributes = GetFileAttributes(filePath);

        if (fileAttributes != uint.MaxValue &&
            ((fileAttributes & 0x02) == 0x02 || (fileAttributes & 0x01) == 0x01)) return true;

        return false;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess,
        uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
        uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    private const uint GENERIC_READ = 0x80000000;
    private const uint GENERIC_WRITE = 0x40000000;
    private const uint OPEN_EXISTING = 3;
    private const int INVALID_HANDLE_VALUE = -1;
    private const int ERROR_SHARING_VIOLATION = 32;

    #endregion
}