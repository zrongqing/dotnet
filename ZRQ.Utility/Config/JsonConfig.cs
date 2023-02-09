using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZRQ.Utils.Config;

public static class JsonConfigStatic
{
    public static readonly JsonSerializerOptions Options = GetJsonSerializerOptions();

    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true, // 测试对齐
            PropertyNameCaseInsensitive = true // 不区分大小写的属性名称
        };
        return options;
    }
}

public class JsonConfig<T> where T : JsonConfig<T>, new()
{
    private static T? _ins;

    [JsonIgnore]
    protected virtual string JsonFile { get; set; } = Path.Combine(Environment.CurrentDirectory, "config", $"{typeof(T).Name}.json");

    public static T Ins
    {
        get
        {
            if (_ins != null) return _ins;

            _ins = new T();
            LoadConfig(ref _ins);
            return _ins;
        }
    }

    private static void LoadConfig(ref T instance)
    {
        try
        {
            var jsonFile = instance.JsonFile;
            if (!File.Exists(jsonFile))
            {
                // 创建一个config
                FileUtils.CreateFile(jsonFile);
                _ = instance.SaveAsync();
                return;
            }

            var jsonString = File.ReadAllText(jsonFile);
            var options = JsonConfigStatic.Options;

            T? deserialize = JsonSerializer.Deserialize<T>(jsonString, options);
            if (deserialize == null) return;

            instance = deserialize;
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public void Save()
    {
        var options = JsonConfigStatic.Options;
        //byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this);

        var jsonString = JsonSerializer.Serialize(Ins, options);
        File.WriteAllText(JsonFile, jsonString);
    }

    public async Task SaveAsync()
    {
        var options = JsonConfigStatic.Options;
        await using var createStream = File.Create(JsonFile);
        {
            //byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this);
            await JsonSerializer.SerializeAsync(createStream, this, options);
            await createStream.DisposeAsync();
        }
    }
}