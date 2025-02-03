using System.Xml.Serialization;

namespace ZRQ.Utils.Config;

public class XmlConfigManage<T> : IXmlConfig<T>
{
    public virtual string XmlFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 加载xml对象
    /// </summary>
    /// <returns> </returns>
    /// <remarks> 返回的xml取决于 <see cref="XmlFilePath" /> </remarks>
    public virtual T? Load()
    {
        var filePath = XmlFilePath;
        return Load(filePath);
    }

    /// <summary>
    /// 从指定的文件加载对象
    /// </summary>
    /// <param name="filePath"> </param>
    /// <returns> </returns>
    public virtual T? Load(string filePath)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        //xmlSerializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
        //xmlSerializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

        FileStream? fs = null;
        var obj = default(T);
        try
        {
            fs = new FileStream(filePath, FileMode.Open);
            obj = (T?)xmlSerializer.Deserialize(fs);
        }
        catch
        {
            // ignored
        }
        finally
        {
            fs?.Close();
        }

        return obj;
    }

    /// <summary>
    /// 保存xml对象在 <see cref="XmlFilePath" /> 文件中
    /// </summary>
    /// <remarks> 保存本类 </remarks>
    public virtual void Save()
    {
        Save(XmlFilePath, this);
    }

    /// <summary>
    /// 保存xml文件
    /// </summary>
    /// <param name="filePath"> 文件路径 </param>
    /// <param name="createFile"> 如果文件不存在是否自动创建文件 </param>
    /// <param name="obj"> 需要保存的可序列化对象 </param>
    public virtual void Save(string filePath, object obj, bool createFile = true)
    {
        try
        {
            if (createFile)
                if (!File.Exists(filePath))
                    FileUtils.CreateFile(filePath);

            var xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(filePath);
            xmlSerializer.Serialize(writer, obj);
            writer.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    protected virtual void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
    {
        var attr = e.Attr;
        Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
    }

    protected virtual void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
    {
        Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
    }
}