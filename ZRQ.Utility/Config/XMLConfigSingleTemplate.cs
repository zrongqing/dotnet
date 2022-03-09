/*************************************************************************************
 *
 * 创建人员:  zrq 
 * 创建时间:  2021/12/20 14:46:26
 * 文件描述:  
 * 
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRQ.Utility.Config;

namespace ZRQ.Utility.ConfigTool
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>更改api的使用注意,文件默认采用utf-8格式保存,如果是其他文件格式可能出现打开失败,建议删除xml文件后重新创建</remarks>
    /// <example>
    /// public class  XMLConfigSingleTestClass : XMLConfigSingleTemplate<XMLConfigSingleTestClass>
    /// {
    ///     public override string XMLFilePath { get; set; } = @"";
    /// }
    /// </example>
    public class XMLConfigSingleTemplate<T> : XMLConfigManage<T> where T : new()
    {
        public override string XMLFilePath
        {
            get
            {
                var configPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                return System.IO.Path.Combine(configPath, typeof(T).Name + ".xml");
            }
        }

        #region Instance
        static T _instance;
        static object _locker = new object();
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            T tempInst = new T();
                            IXMLConfig<T> iXMLConfig = (IXMLConfig<T>)tempInst;
                            string filePath = iXMLConfig.XMLFilePath;
                            _instance = iXMLConfig.Load();
                            if (_instance == null)
                            {
                                _instance = new T();
                            }
                        }
                    }
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        #endregion

        /// <summary>
        /// 单例类不支持指定路径加载
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public new T Load(string filePath)
        {
            throw new NotSupportedException("单例类不支持指定路径加载");
        }
    }
}