using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZRQ.Util.Regular
{
    public class RegularUtil
    {
        // 非负数
        public static readonly string Nonnegative = @"^(0|[1-9][0-9]*)(\.\d+)?$";

        // 非零 非负数
        public static readonly string NonzeroNonnegative = @"^(?!(0[0-9]{0,}$))[0-9]{1,}[.]{0,}[0-9]{0,}$";

        public static Match GetMatch(string strValue, string regularValue)
        {
            return Regex.Match(strValue, regularValue);
        }
    }
}
