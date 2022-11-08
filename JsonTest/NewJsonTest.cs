using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonTest
{
    internal class NewJsonTest
    {
        public static void Process()
        {
            string jsonString = string.Empty;

            WeatherForecast weatherForecast = new WeatherForecastDerived();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            jsonString = JsonSerializer.Serialize<WeatherForecast>(weatherForecast, options);
            Console.WriteLine(jsonString);

            jsonString = JsonSerializer.Serialize<object>(weatherForecast, options);
            Console.WriteLine(jsonString);

            jsonString = JsonSerializer.Serialize(weatherForecast, weatherForecast.GetType(), options);
            Console.WriteLine(jsonString);

        }
    }
}
