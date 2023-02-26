using System.Diagnostics;

namespace ZRQ.Utils;

public static class FileUtils
{
    /// <summary>
    /// 复制文件夹的递归
    /// </summary>
    /// <param name="sourceFolderName"> </param>
    /// <param name="destFolderName"> </param>
    /// <param name="overwrite"> </param>
    /// <param name="arrExtenion"> 指定复制的文件后缀 </param>
    /// <returns> 返回提示信息，成功，返回"" </returns>
    public static string CopyFun(string sourceFolderName, string destFolderName, bool overwrite = false,
        string[]? arrExtenion = null)
    {
        try
        {
            var listdir = Directory.GetDirectories(sourceFolderName);
            if (listdir != null && listdir.Length > 0)
                foreach (var item in listdir)
                {
                    var forlders = item.Split('\\');
                    var lastDirectory = forlders[forlders.Length - 1];
                    var dest = Path.Combine(destFolderName, lastDirectory);
                    if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);

                    CopyFun(item, dest, overwrite, arrExtenion);
                }

            var list = Directory.GetFiles(sourceFolderName);
            if (list != null && list.Length > 0)
                foreach (var item in list)
                {
                    var strExtenion = Path.GetExtension(item).ToLower();
                    if (arrExtenion != null && !arrExtenion.Contains(strExtenion)) continue;
                    var sourceFileName = Path.GetFileName(item);
                    File.Copy(item, Path.Combine(destFolderName, sourceFileName), overwrite);
                }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "";
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

    public static bool CreateDir(string dirPath)
    {
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        return true;
    }

    public static void OpenFolderAndSelectFile(string fileFullName)
    {
        var psi = new ProcessStartInfo("Explorer.exe");
        psi.Arguments = "/e,/select," + fileFullName;
        Process.Start(psi);
    }

    /// <summary>
    /// 得到文件夹下的所有文件(包括字文件夹)
    /// </summary>
    public static string[] GetFilesRecursively(string path)
    {
        // 获取当前目录下的所有文件
        string[] files = Directory.GetFiles(path);

        // 获取当前目录下的所有子目录
        string[] directories = Directory.GetDirectories(path);

        // 遍历所有子目录，并递归调用自己获取子目录中的文件
        foreach (string directory in directories)
        {
            string[] subFiles = GetFilesRecursively(directory);
            files = files.Concat(subFiles).ToArray();
        }

        return files;
    }
}