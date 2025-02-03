namespace ZRQ.Utils.Character;

/// <summary>
/// 字符转换
/// </summary>
public class CharacterConvert
{
    /// <summary>
    /// 英文字符转为中文字符
    /// </summary>
    /// <param name="text"> 转换的中文字符串 </param>
    /// <returns> </returns>
    public static string EnToCh(string text)
    {
        const string ch = "。；，？！、“”‘’（）—"; //中文字符
        const string en = @".;,?!\""""''()-"; //英文字符
        var c = text.ToCharArray();
        for (var i = 0; i < c.Length; i++)
        {
            var n = ch.IndexOf(c[i]);
            if (n != -1) c[i] = en[n];
        }

        return new string(c);
    }
}