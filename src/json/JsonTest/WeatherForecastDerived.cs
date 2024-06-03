using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    public class WeatherForecastDerived : WeatherForecast
    {
        public int WindSpeed { get; set; }
    }
}
