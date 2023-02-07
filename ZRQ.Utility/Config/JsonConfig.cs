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
    private static T? _inst;

    [JsonIgnore]
    protected virtual string JsonFile { get; set; } = Path.Combine(Environment.CurrentDirectory, $"{nameof(T)}.json");

    public static T Inst
    {
        get
        {
            if (_inst != null) return _inst;

            _inst = new T();
            LoadConfig(_inst);
            return _inst;
        }
    }

    private static void LoadConfig(T inst)
    {
        try
        {
            var jsonFile = inst.JsonFile;
            if (!File.Exists(jsonFile)) return;

            var jsonString = File.ReadAllText(jsonFile);

            var deserialize = JsonSerializer.Deserialize<T>(jsonString);
            if (deserialize != null) inst = deserialize;
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

        var jsonString = JsonSerializer.Serialize(Inst, options);
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