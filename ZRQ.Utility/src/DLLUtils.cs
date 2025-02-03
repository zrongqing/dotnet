using System.Runtime.InteropServices;

namespace ZRQ.Utils;

/// <summary>
/// DLL的相关操作
/// </summary>
public class DllUtils
{
    /// <summary>
    /// 通过Win32 来判断是否已经加载了DLL
    /// </summary>
    /// <param name="path"> DLL路径 </param>
    /// <returns> </returns>
    public static bool IsLoadedByWin32(string path)
    {
        return GetModuleHandle(path) != IntPtr.Zero;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}