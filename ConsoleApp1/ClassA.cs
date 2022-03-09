using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ClassA : IEqualityComparer<ClassA>
    {
        private int Index = 0;

        public ClassA(int v)
        {
            this.Index = v;
        }

        public bool Equals(ClassA? x, ClassA? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] ClassA obj)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

    }
}
