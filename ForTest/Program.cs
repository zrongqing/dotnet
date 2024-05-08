using System.Diagnostics;

namespace ForTest
{
    internal class Program
    {
        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            // 计时
            stopwatch.Start();

            int dataSize = 1000000000;
            Console.WriteLine($"测试数据大小 {dataSize}");

            int testNum = 10;
            Console.WriteLine($"测试次数: {testNum}");

            Console.WriteLine($"准备数据,{stopwatch.Elapsed}");
            var ints = new int[dataSize];
            for ( int i = 0; i < dataSize; i++ )
            {
                ints[i] = i;
            }
            var lists = new List<int>(dataSize);
            for ( int i = 0;i < dataSize; i++ )
            {
                lists.Add(i);
            }
            Console.WriteLine($"准备数据完成,{stopwatch.Elapsed}");

            for ( int i = 0;i<testNum;i++ )
            {
                Console.WriteLine($"");
                Console.WriteLine($"第{i}次比较");

                Console.WriteLine($"Array 比较");
                ForToArrayTest(ints);
                ForeachToArrayTest(ints);

                //Console.WriteLine($"List 比较");
                //ForToListTest(lists);
                //ForeachToListTest(lists);
            }

 
        }

        public static void ForToArrayTest(int[] ints)
        {
            var result = 0;

            var startTime = stopwatch.Elapsed;
            for (var i = 0; i < ints.Length; i++)
            {
                result = ints[i];
            }
            var endTime = stopwatch.Elapsed;

            // 获取经过的时间
            TimeSpan elapsedTime = endTime - startTime;
            Console.WriteLine("For耗时 经过时间: {0} 毫秒", elapsedTime.TotalMilliseconds);
        }

        public static void ForeachToArrayTest(int[] ints)
        {
            var result = 0;

            var startTime = stopwatch.Elapsed;
            foreach (var i in ints)
            {
                result = i;
            }
            var endTime = stopwatch.Elapsed;

            // 获取经过的时间
            TimeSpan elapsedTime = endTime - startTime;
            Console.WriteLine("Foreach耗时 经过时间: {0} 毫秒", elapsedTime.TotalMilliseconds);
        }

        public static void ForToListTest(List<int> lists)
        {
            var result = 0;

            var startTime = stopwatch.Elapsed;
            for (var i = 0; i < lists.Count; i++)
            {
                result = lists[i];
            }
            var endTime = stopwatch.Elapsed;

            // 获取经过的时间
            TimeSpan elapsedTime = endTime - startTime;
            Console.WriteLine("For耗时 经过时间: {0} 毫秒", elapsedTime.TotalMilliseconds);
        }

        public static void ForeachToListTest(List<int> lists)
        {
            var result = 0;

            var startTime = stopwatch.Elapsed;
            foreach (var item in lists)
            {
                result = item;
            }
            var endTime = stopwatch.Elapsed;

            // 获取经过的时间
            TimeSpan elapsedTime = endTime - startTime;
            Console.WriteLine("Foreach耗时 经过时间: {0} 毫秒", elapsedTime.TotalMilliseconds);
        }
    }
}
