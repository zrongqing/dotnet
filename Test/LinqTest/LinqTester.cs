using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    internal class LinqTester
    {
        private static List<string>? _lists;

        public static List<string> Lists
        {
            get { return _lists ??= InitLinqData(); }
        }

        public static List<string> InitLinqData()
        {
            List<string> lists = new();

            for (int i = 0; i < 100000; i++)
            {
                lists.Add($"{i}");
            }

            return lists;
        }

    }
}
