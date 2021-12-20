using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class List_Sort测试
    {

        public void Process()
        {
            try
            {
                // 空list测试
                List<string> list = new List<string>();
                list.Sort((a, b) => a.CompareTo(b));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                // list.count<2测试
                List<string> list = new List<string>();
                list.Add("a");
                list.Sort((a, b) => a.CompareTo(b));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Result()
        {
            Console.WriteLine(String.Format("空list可排序，list.count<2可排序"));
        }
    }
}
