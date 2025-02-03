using System.Text.Json;

namespace JsonTest;

internal class SystemTestJsonTest
{
    public static void Process()
    {
        var jsonString = string.Empty;

        WeatherForecast weatherForecast = new WeatherForecastDerived();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        jsonString = JsonSerializer.Serialize(weatherForecast, options);
        Console.WriteLine(jsonString);

        jsonString = JsonSerializer.Serialize<object>(weatherForecast, options);
        Console.WriteLine(jsonString);

        jsonString = JsonSerializer.Serialize(weatherForecast, weatherForecast.GetType(), options);
        Console.WriteLine(jsonString);
    }
}