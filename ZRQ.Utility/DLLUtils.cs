using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZRQ.Utils
{
    /// <summary>
    /// DLL的相关操作
    /// </summary>
    public class DllUtils
    {
        public static bool IsDllLoaded(string path)
        {
            return GetModuleHandle(path) != IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
