using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonTest
{
    [JsonObject]
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public string? Summary { get; set; }
        public int TemperatureCelsius { get; set; }
    }
}
