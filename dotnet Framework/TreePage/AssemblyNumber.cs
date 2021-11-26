using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePage
{
    public class AssemblyNumber : ICloneable
    {
        public string Prefix { get; set; }
        public string PrefixNum { get; set; }

        public AssemblyNumber(string prefix, string prefixNum)
        {
            this.Prefix = prefix;
            this.PrefixNum = prefixNum;
        }

        public object Clone()
        {
            return new AssemblyNumber(this.Prefix, this.PrefixNum);
        }

        /// <summary>
        /// 转化成部件名字 前缀 + 序号
        /// </summary>
        public string ToAssyNum()
        {
            return string.Format("{0}{1}", this.Prefix, this.PrefixNum);
        }
    }
}
