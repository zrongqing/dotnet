using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZRQ.Util
{
    /// <summary>
    /// DLL的相关操作
    /// </summary>
    public class DLLUtils
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public static bool IsDllLoaded(string path)
        {
            return GetModuleHandle(path) != IntPtr.Zero;
        }
    }
}
