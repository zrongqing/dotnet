namespace ZRQ.Utils;

public static class LocalSystemUtil
{
    public static string GetComputerName()
    {
        return Environment.MachineName;
    }
}