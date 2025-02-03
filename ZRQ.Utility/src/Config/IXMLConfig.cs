namespace ZRQ.Utils.Config;

public interface IXmlConfig<out T>
{
    /// <summary>
    /// Xml文件路径
    /// </summary>
    string XmlFilePath { get; set; }

    T? Load();

    T? Load(string filePath);

    void Save();

    void Save(string filePath, object obj, bool createFile = true);
}