using ZRQ.Utils.Config;

namespace ZRQ.Test;

public class JsonData : JsonConfig<JsonData>
{
    protected override string JsonFile { get; set; } = @$"C:\Users\zrq\Downloads\{nameof(JsonData)}.json";

    public string AA { get; set; } = "123";
}