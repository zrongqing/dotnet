using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    /// <summary>
    /// 接线端子的类型
    /// </summary>
    /// <remarks>
    /// 按照工业标准来说，分为
    /// 单相二线制，单相三线制
    /// 三相三线制，三相四线制，三相五线制
    /// 但是由于此时并没有对单相 三相做区分，所以采用线制分类
    /// </remarks>
    public enum TerminalBlockType
    {
        /// <summary>
        /// 无效类型
        /// </summary>
        Invalid = -1,
        /// <summary>
        /// 三线制
        /// </summary>
        ThreeWireSystem,
        /// <summary>
        /// 四线制
        /// </summary>
        FourWireSystem,
        /// <summary>
        /// 五线制
        /// </summary>
        FiveWireSystem,
    }

    /// <summary>
    /// 接线端子的规格类
    /// </summary>
    public class TerminalBlockSize
    {
        public TerminalBlockType Type = TerminalBlockType.Invalid;

        public string FireWireA = String.Empty;
        public string FireWireB = String.Empty;
        public string FireWireC = String.Empty;
        public string EarthWire = String.Empty;
        public string NeutralWire = String.Empty;

        /// <summary>
        /// 转换成 数量x横截面积+数量x横截面积的形式
        /// </summary>
        /// <returns></returns>
        public string ToSize()
        {
            List<int> list = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();

            if (FireWireA != null || FireWireA != string.Empty)
            {
                AddDic(dict, FireWireA);
            }
            if (FireWireB != null || FireWireB != string.Empty)
            {
                AddDic(dict, FireWireB);
            }
            if (FireWireC != null || FireWireC != string.Empty)
            {
                AddDic(dict, FireWireC);
            }
            if (EarthWire != null || EarthWire != string.Empty)
            {
                AddDic(dict, EarthWire);
            }
            if (NeutralWire != null || NeutralWire != string.Empty)
            {
                AddDic(dict, NeutralWire);
            }

            string str = null;
            foreach (var pair in dict)
            {
                if (str == null)
                {
                    str += string.Format("{0}x{1}", pair.Value, pair.Key);
                }
                else
                {
                    str += string.Format("+{0}x{1}", pair.Value, pair.Key);
                }
            }
            return str;
        }

        private void AddDic(Dictionary<int, int> dict, string str)
        {
            int num = int.Parse(str);
            if (dict.ContainsKey(num))
            {
                dict[num]++;
            }
            else
            {
                dict.Add(num, 1);
            }
        }

        public override string ToString()
        {
            return string.Format("火线A{0}\n火线B{1}\n火线C{2}\n地线{3}\n零线A{4}\n",
                this.FireWireA,
                this.FireWireB,
                this.FireWireC,
                this.EarthWire,
                this.NeutralWire);
        }
    }

    /// <summary>
    /// 正则表达式
    /// </summary>
    /// <remarks>
    /// 接线端子的规格支持：
    /// 4x15；4x（1x15）；4H15;4X(1H15);4(1H15);4(1X15);H16
    /// </remarks>
    internal class TerminalBlockSizeHelper
    {
        /// <summary>
        /// 4x(1x15)
        /// ^\d+[x|\*]*\(\d*[a-zA-Z]+\d+\)
        /// </summary>
        public static string OneMode = @"^\d+[x|\*]*\(\d*[a-zA-Z]+[\d(.\d)?]+\)";
        /// <summary>
        /// 4x(1H15)
        /// </summary>
        public static string D = null;
        /// <summary>
        /// 4(1H15)
        /// </summary>
        public static string E = null;
        /// <summary>
        /// 4(1X15)
        /// </summary>
        public static string F = null;
        // 以上匹配作为统一匹配 按照 ( )拆分，取 （ 前面连续的数字  取 ）连续的数字，从遇到第一个数字开始，到不是第一个数字结束

        /// <summary>
        /// [数字] x 字母 x 数字
        /// 4x15
        /// 4H15
        /// ^\d*[a-zA-Z|\*]+\d+
        /// </summary>
        public static string TwoMode = @"^\d*[a-zA-Z|\*]+[\d(.\d)?]+";

        /// <summary>
        /// 字母 x 数字
        /// H16
        /// </summary>
        public static string TwoMode_1 = @"^[a-zA-Z|\*]+[\d(.\d)?]+";
        // 以上匹配作为统一匹配 按照中间的英语字母进行拆分 前面为空则补1。

        /// <summary>
        /// 乘法的匹配方式
        /// </summary>
        public static string Multiply = @"[x|\*]";

 
        public static TerminalBlockSize DoSplit(string str)
        {
            TerminalBlockSize terminalBlockSize = new TerminalBlockSize();

            List<double> sectionArea = new List<double>();

            // 先判断是否有加号，对每个加号进行分割，再别分处理
            String[] splitPlus = str.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitPlus.Length > 2)
            {
                //log4netTool.Error(string.Format("线管规格:{0},存在1个以上的+号,匹配失败"));
            }
            else if (splitPlus.Length == 1)
            {   // 只要一个部分
                string splitPlusOne = splitPlus[0];
                DoSpliSingleWithMode(splitPlusOne, sectionArea);
            }
            else if (splitPlus.Length == 2)
            {
                string splitPlusOne = splitPlus[0];
                string splitPlusTwo = splitPlus[1];
                DoSpliSingleWithMode(splitPlusOne, sectionArea);
                DoSpliSingleWithMode(splitPlusTwo, sectionArea);
            }
            // 排序
            sectionArea.Sort();

            if (sectionArea.Count == 3)
            {
                terminalBlockSize.Type = TerminalBlockType.ThreeWireSystem;
                terminalBlockSize.FireWireA = sectionArea[0].ToString();
                terminalBlockSize.EarthWire = sectionArea[1].ToString();
                terminalBlockSize.NeutralWire = sectionArea[2].ToString();
            }
            if (sectionArea.Count == 4)
            {
                terminalBlockSize.Type = TerminalBlockType.FourWireSystem;
                terminalBlockSize.FireWireA = sectionArea[0].ToString();
                terminalBlockSize.FireWireB = sectionArea[1].ToString();
                terminalBlockSize.FireWireC = sectionArea[2].ToString();
                terminalBlockSize.EarthWire = sectionArea[3].ToString();
            }
            if (sectionArea.Count == 5)
            {
                terminalBlockSize.Type = TerminalBlockType.FiveWireSystem;
                terminalBlockSize.FireWireA = sectionArea[0].ToString();
                terminalBlockSize.FireWireB = sectionArea[1].ToString();
                terminalBlockSize.FireWireC = sectionArea[2].ToString();
                terminalBlockSize.NeutralWire = sectionArea[3].ToString();
                terminalBlockSize.EarthWire = sectionArea[4].ToString();
            }

            return terminalBlockSize;
        }

        /// <summary>
        /// 拆分单独的一组数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <remarks>
        /// 举例：4x(1H15)
        /// 先按照 '('')'拆分成 4x 1H14 "" 强制拆分成3份
        /// 将第一份数据 4x 按照单词继续拆分成 4 x 取数字
        /// 如果只有1H14 继续拆分单词 'H'  1  14
        ///     取14作为横截面积
        /// 如果有两个 分别拆分，4作为条数，14作为横截面积
        /// </remarks>
        private static void DoSpliSingleWithMode(string str, List<double> sectionAreaList)
        {
            Match match;

            // 数字 [乘] 左括号 [数字] [乘] [字母] 数字 右括号
            match = Regex.Match(str, @"\d[x|\*]?\(\d?[x|\*]?[a-zA-Z]?\d+\)");
            if (match.Success)
            {
                // 先把满足的部分给截取出来, 默认取第一部分
                string strSplit = match.Value;
                String[] strSplits = strSplit.Split(new char[] { '(', ')' }, StringSplitOptions.None);

                // 第一部分
                double num = 1;    // 默认有一条线
                string oneSplit = strSplits[0];
                DoSplitEndNum(oneSplit, ref num);

                // 第二部分
                double sectionArea = 0;
                string twoSplit = strSplits[1];
                DoSplitEndNum(twoSplit, ref sectionArea);

                // 第三部分 不管
                string threeSplit = strSplits[2];

                for (int i = 0; i < num; i++)
                {
                    sectionAreaList.Add(sectionArea);
                }

                return;
            }

            // 不带括号 但是有乘号
            match = Regex.Match(str, @"\d[x|*]\d*[a-zA-Z]*\d");
            if (match.Success)
            {
                string strSplit = match.Value;
                String[] strSplits = strSplit.Split(new char[] { 'x', '*' }, StringSplitOptions.None);

                double num = 1;    // 默认有一条线
                double sectionArea = 0;

                // 第一部分应该是数字
                if (int.TryParse(strSplits[0], out int onenum))
                {
                    num = onenum;
                }
                DoSplitEndNum(strSplits[1], ref sectionArea);

                for (int i = 0; i < num; i++)
                {
                    sectionAreaList.Add(sectionArea);
                }
                return;
            }

            // 没有乘
            match = Regex.Match(str, @"\d*[a-zA-Z]+\d+");
            if (match.Success)
            {
                double num = 1;    // 默认有一条线
                double sectionArea = 0;

                DoSplitFrontNum(str, ref num);
                DoSplitEndNum(str, ref sectionArea);

                for (int i = 0; i < num; i++)
                {
                    sectionAreaList.Add(sectionArea);
                }
                return;
            }
        }

        /// <summary>
        /// 拆分 数字 字母 数字的集合体 取后面的数字
        /// 类式于 H16
        /// </summary>
        /// <param name="splitStr"></param>
        /// <param name="num"></param>
        private static void DoSplitFrontNum(string splitStr, ref double num)
        {
            if (Regex.IsMatch(splitStr, TerminalBlockSizeHelper.TwoMode_1))
            {
                num = 1;
                return;
            }

            string[] numberSplit = Regex.Split(splitStr, @"[a-zA-Z]+");
            for (int i = 0; i >= 0; i++)
            {
                if (numberSplit[i] != string.Empty)
                {
                    if (double.TryParse(numberSplit[i], out double tNum))
                    {
                        num = tNum;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 拆分 数字 字母 数字的集合体 取后面的数字
        /// </summary>
        private static void DoSplitEndNum(string splitStr, ref double num)
        {
            string[] numberSplit = Regex.Split(splitStr, @"[a-zA-Z]+");
            for (int i = numberSplit.Length - 1; i >= 0; i--)
            {
                if (numberSplit[i] != string.Empty)
                {
                    if (double.TryParse(numberSplit[i], out double tNum))
                    {
                        num = tNum;
                        break;
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = "4x30+PN50";
            string str1 = "750V4(1x16)";

            TerminalBlockSize terminalBlockSize = TerminalBlockSizeHelper.DoSplit(str);
        }
    }
}
