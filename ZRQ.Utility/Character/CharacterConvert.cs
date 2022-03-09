using System;
using System.Collections.Generic;
using System.Text;

namespace ZRQ.Utility.Character
{
    public class CharacterConvert
    {
        /// <summary>
        /// 英文字符转为中文字符
        /// </summary>
        /// <param name="text">转换的中文字符串</param>
        /// <returns></returns>
        public static string ConvertToEn(string text)
        {
            const string ch = "。；，？！、“”‘’（）—";//中文字符
            const string en = @".;,?!\""""''()-";//英文字符
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int n = ch.IndexOf(c[i]);
                if (n != -1) c[i] = en[n];
            }
            return new string(c);
        }
    }
}
