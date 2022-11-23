using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    public class Student : People
    {
        public string SutdentName;

        public Student() : base()
        {
            Console.WriteLine($"{nameof(Student)}");
        }
    }
}
