using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonProcessingLabPractice
{
    public class Forecast
    {
        public List<WeatherForecast> Forecasts { get; set; }

        public Tuple<int,string> AdditionalData { get; set; }
    }

    public class WeatherForecast
    {
        public double LongNameOfThisDecimalProperty { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public List<int> TemperaturesC { get; set; } = new List<int>() {30, 28, 67};

        public string Summary { get; set; } = "Hot summer day";
    }
}