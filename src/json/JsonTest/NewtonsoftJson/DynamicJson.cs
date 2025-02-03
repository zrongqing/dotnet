using Newtonsoft.Json;

namespace JsonTest.NewtonsoftJson;

internal class DynamicJson
{
    public static void Process()
    {
        var strA = @"{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }";
        var results = JsonConvert.DeserializeObject<dynamic>(strA);
        if (null == results) return;
        Console.WriteLine($"{strA}");
        Console.WriteLine($"Name: {results.Name} Address:{results.Address}");

        var strB = "{\"ok\":true,\"msg\":\"\",\"data\":[\"HWG_BUPIN\",\"HWT150655\"]}";
        results = JsonConvert.DeserializeObject<dynamic>(strB);
        if (results == null) return;
        var isOk = results.ok;
        var name = results.Name;
        var value = results.aaaaa;
    }
}