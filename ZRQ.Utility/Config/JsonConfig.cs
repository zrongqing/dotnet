using System.Text.Json;
using System.Text.Json.Serialization;
using ZRQ.Utils.ClassTemplate;

namespace ZRQ.Utils.Config;

public static class JsonConfigStatic
{
    public static readonly JsonSerializerOptions Options = GetJsonSerializerOptions();

    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true,                   // 测试对齐
            PropertyNameCaseInsensitive = true,     // 不区分大小写的属性名称
        };
        return options;
    }
}

public class JsonConfig<T>
{
    [JsonIgnore]
    protected virtual string JsonFile { get; set; } = Path.Combine(Environment.CurrentDirectory, $"{nameof(T)}.json");

    public void Save()
    {
        var options = JsonConfigStatic.Options;
        byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this);

        string jsonString = JsonSerializer.Serialize(jsonUtf8Bytes, options);
        File.WriteAllText(JsonFile, jsonString);
    }

    public async Task SaveAsync()
    {
        var options = JsonConfigStatic.Options;
        await using FileStream createStream = File.Create(JsonFile);
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this);
            await JsonSerializer.SerializeAsync(createStream, jsonUtf8Bytes, options);
            await createStream.DisposeAsync();
        }
    }
}

