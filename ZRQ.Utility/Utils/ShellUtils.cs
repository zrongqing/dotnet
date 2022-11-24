using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZRQ.Util
{
    /// <summary>  
    /// 与控制台交互  
    /// </summary>  
    public static class ShellUtils
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="format"></param>  
        /// <param name="args"></param>  
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public static void WriteLine(string output, ShellMessageType shellMessageType = ShellMessageType.DEFAULT)
        {
            Console.ForegroundColor = GetConsoleColor(shellMessageType);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, output);
        }

        public static void Error(string error)
        {
            Console.ForegroundColor = GetConsoleColor(ShellMessageType.ERROR);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, error);
        }

        public static void Warning(string warning)
        {
            Console.ForegroundColor = GetConsoleColor(ShellMessageType.WARNING);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, warning);
        }

        public static void Notice(string notice)
        {
            Console.ForegroundColor = GetConsoleColor(ShellMessageType.NOTICE);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, notice);
        }

        private static ConsoleColor GetConsoleColor(ShellMessageType shellMessageType)
        {
            if (shellMessageType == ShellMessageType.DEFAULT)
                return ConsoleColor.Gray;
            if (shellMessageType == ShellMessageType.ERROR)
                return ConsoleColor.Red;
            if (shellMessageType == ShellMessageType.WARNING)
                return ConsoleColor.Yellow;
            if (shellMessageType == ShellMessageType.NOTICE)
                return ConsoleColor.Green;

            return ConsoleColor.Gray;
        }
    }
}
