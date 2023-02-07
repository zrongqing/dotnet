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
}