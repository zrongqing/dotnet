using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZRQ.Utils
{
    public static class LocalSystemUtil
    {
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }
    }
}
