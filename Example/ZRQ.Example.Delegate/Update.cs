using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ZRQ.Example.Delegate
{

    public class Update
    {
        public delegate int Comparison<in T>(T left, T right);

        // Declare an instance of that type:
        public static Comparison<int> UpdateComparator;

        public void Excute()
        {
            UpdateComparator(1, 1);
        }
    }
}
