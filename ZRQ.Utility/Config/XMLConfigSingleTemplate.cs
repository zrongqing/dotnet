/*************************************************************************************
 *
 * 创建人员:  zrq
 * 创建时间:  2021/12/20 14:46:26
 * 文件描述:
 *
*************************************************************************************/

namespace ZRQ.Utils.Config
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>更改api的使用注意,文件默认采用utf-8格式保存,如果是其他文件格式可能出现打开失败,建议删除xml文件后重新创建</remarks> <example>
    /// </example>
    public class XmlConfigSingleTemplate<T> : XmlConfigManage<T> where T : new()
    {
        public override string XmlFilePath
        {
            get
            {
                var configPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                return System.IO.Path.Combine(configPath, typeof(T).Name + ".xml");
            }
        }

        #region Inst

        private static T? _instance;
        private static readonly object _locker = new();

        public static T Inst
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_locker)
                {
                    if (_instance != null) return _instance;

                    T tempInst = new();
                    IXmlConfig<T> iXmlConfig = (IXmlConfig<T>)tempInst;
                    string filePath = iXmlConfig.XmlFilePath;

                    _instance = iXmlConfig.Load() ?? new T();
                }
                return _instance;
            }
            set => _instance = value;
        }

        #endregion Inst

        /// <summary>
        /// 单例类不支持指定路径加载
        /// </summary>
        /// <param name="filePath"> </param>
        /// <returns> </returns>
        public new T Load(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            throw new NotSupportedException("单例类不支持指定路径加载");
        }
    }
}
