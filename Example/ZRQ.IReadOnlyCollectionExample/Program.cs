using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZRQ.IReadOnlyCollectionExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IReadOnlyList<int> readOnlylist = new List<int>()
            {
                1,2,3, 4, 5, 6, 7,
            };
            TestIReadOnlyList(readOnlylist);
            readOnlylist.ToList().Count();

            IReadOnlyList<ReadOnlyClass> readOnlylist2 = new List<ReadOnlyClass>()
            {
                new ReadOnlyClass(){Id = 1},
                new ReadOnlyClass(){Id = 2},
                new ReadOnlyClass(){Id = 3},
                new ReadOnlyClass(){Id = 4},
            };
            foreach (var item in readOnlylist2)
            {
                Console.WriteLine(item.GetHashCode());
            }
            TestReadOnlyClass(readOnlylist2);
            foreach (var item in readOnlylist2)
            {
                Console.WriteLine(item.GetHashCode());
            }

            Console.ReadKey();
        }

        private static void TestIReadOnlyList(IReadOnlyList<int> list)
        {
            //list[0] = 2;
        }

        private static void TestReadOnlyClass(IReadOnlyList<ReadOnlyClass> readOnlylist2)
        {
            readOnlylist2[0].Id = 2;
        }
    }

    internal class ReadOnlyClass
    {
        public ReadOnlyClass()
        { }

        public int Id { get; set; }
    }
}
