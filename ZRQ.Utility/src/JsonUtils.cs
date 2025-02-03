using System.Text.Encodings.Web;
using System.Text.Json;

namespace ZRQ.Utils;

public static class JsonUtils
{
    public static readonly JsonSerializerOptions Options = GetJsonSerializerOptions();

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true, // 对齐写入
            PropertyNameCaseInsensitive = true // 不区分大小写的属性名称
        };
        return options;
    }

    public static void SaveJsonFile(string filePath, object jsonObject)
    {
        if (!File.Exists(filePath))
            // 创建一个config
            FileUtils.CreateFile(filePath);

        var jsonString = JsonSerializer.Serialize(jsonObject, Options);
        File.WriteAllText(filePath, jsonString);
    }

    public static T? Deserialize<T>(string jsonFile)
    {
        var jsonString = File.ReadAllText(jsonFile);
        var options = Options;

        var deserialize = JsonSerializer.Deserialize<T>(jsonString, options);
        return deserialize;
    }
}