using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class 测试ListContains比较使用Equal
    {
        public static void Process()
        {
            Person presionA = new Person();
            presionA.Age = "12";
            presionA.Name = "presionA";

            Person presionB = new Person();
            presionB.Age = "15";
            presionB.Name = "presionB";

            List<Person> personList = new List<Person>();
            personList.Add(presionA);

            if (personList.Contains(presionA))
            {
                System.Console.WriteLine("包含A");
            }

            if (personList.Contains(presionB))
            {
                System.Console.WriteLine("包含B");
                //a1
            }
            //aaaaaaaass
        }
    }

    public class Person
    {
        private string name;
        private string age;

        public String Name { get => name; set => name = value; }

        public String Age { get => age; set => age = value; }

        public Person()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
