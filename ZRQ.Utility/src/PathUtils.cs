using System.Text.RegularExpressions;

namespace ZRQ.Utils;

public static class PathUtils
{
    public static string GetMyDocuments()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public static string GetSystemDirectory()
    {
        return Environment.SystemDirectory;
    }

    public static string GetRoot()
    {
        var path = Path.GetPathRoot(Environment.SystemDirectory);
        return path ?? @"C:\";
    }

    public static string GetCurrentDirectory()
    {
        return Environment.CurrentDirectory;
    }

    /// <summary>
    /// C:\Users\UserName\AppData\Local
    /// </summary>
    public static string GetLocalApplicationData()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }

    public static string NormalizePath(string path)
    {
        // 将相对路径转换为绝对路径
        var absolutePath = Path.GetFullPath(path);

        // 将路径分隔符统一为 "/"
        absolutePath = absolutePath.Replace('\\', '/');

        // 将多个连续的 "/" 替换为单个 "/"
        absolutePath = Regex.Replace(absolutePath, @"\/{2,}", "/");

        // 返回标准化后的路径
        return absolutePath;
    }
}