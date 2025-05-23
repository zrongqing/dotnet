﻿using System.Text.RegularExpressions;

namespace ZRQ.Utils;

public class RegularUtils
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