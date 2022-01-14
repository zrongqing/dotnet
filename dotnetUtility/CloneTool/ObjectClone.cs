#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：DotNetTool.CloneTool
* 项目描述 ：
* 类 名 称 ：ObjectCopier
* 类 描 述 ：
* 命名空间 ：DotNetTool.CloneTool
* 作    者 ：zrq 
* 创建时间 ：2020/11/12 15:18:03
* 更新时间 ：2020/11/12 15:18:03
*******************************************************************
* Copyright @ 58317 2020. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetTool.CloneTool
{
    /// <summary>
    /// 类的克隆扩展
    /// </summary>
    /// <remarks>
    /// 参考 http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// </remarks>
    public static class ObjectClone
    {
        /// <summary>
        /// 字节流的克隆
        /// </summary>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// xml序列化克隆
        /// </summary>
        /// x<typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Clone_XMLSerializer<T>(T t)
        {
            T clone = default(T);
            using (Stream stream = new MemoryStream())
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(stream, t);
                    stream.Seek(0, SeekOrigin.Begin);
                    clone = (T)xmlSerializer.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                }
            }
            return clone;
        }
    }
}
