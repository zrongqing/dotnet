﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public string? Summary { get; set; }
        public int TemperatureCelsius { get; set; }
    }
}
