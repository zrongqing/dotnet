/*************************************************************************************
 *
 * 创建人员:  zrq 
 * 创建时间:  2021/12/20 14:48:02
 * 文件描述:  
 * 
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRQ.Utility.ConfigTool;

namespace ZRQ.Example.XML
{
    internal class ExampleClass
    {
        public double Diameter { get; set; }
        public double Radius { get { return Diameter / 2; } set { Diameter = value * 2; } }

        /// <summary>
        /// 这将导致Radius不被序列化，但仍允许对其进行反序列化。
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// XMLSerializer必须公开此方法，以便避免污染名称空间，您可以添加EditorBrowsable属性以将其从IDE中隐藏。 不幸的是，只有当程序集在当前项目中被引用为DLL时，这种隐藏方式才有效，但如果您使用此代码编辑实际项目，则此隐藏方式将无效。
        /// </remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerializeRadius() { return false; }
    }

    /// <summary>
    /// 使用例子说明
    /// </summary>
    internal class XMLConfigExample : XMLConfigSingleTemplate<XMLConfigExample>
    {
        // 自定义 路径 可选
        public override string XMLFilePath { get; set; } = string.Empty;
    }
}