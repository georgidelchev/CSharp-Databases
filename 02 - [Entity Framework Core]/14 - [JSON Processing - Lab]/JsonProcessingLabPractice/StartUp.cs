using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace JsonProcessingLabPractice
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var weather = new Forecast()
            {
                AdditionalData = new Tuple<int, string>(123, "a"),
                Forecasts = new List<WeatherForecast>()
                {
                    new WeatherForecast(),
                    new WeatherForecast(),
                }
            };

            // SYSTEM.TEXT.JSON
            //var json = JsonSerializer.Serialize(weather, new JsonSerializerOptions()
            //{
            //    WriteIndented = true,
            //});

            //File.WriteAllText("weather.json", json);

            //Console.WriteLine(json);

            //var json = File.ReadAllText("weather.json");

            //var weather = JsonSerializer.Deserialize<WeatherForecast>(json);



            // JSON.NET
            var forecast = new WeatherForecast();

            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Culture = CultureInfo.InvariantCulture,
                DateFormatString = "yyyy-MM-dd"
            };

            Console.WriteLine(JsonConvert.SerializeObject(forecast, jsonSettings));

            // JsonConvertToAnonymousObject();



        }

        private static void JsonConvertToAnonymousObject()
        {
            var json = File.ReadAllText("weather.json");

            var weather1 = JsonConvert.DeserializeObject<WeatherForecast>(json);

            var obj = new
            {
                TemperatureC = 0,
                Summary = string.Empty
            };

            var json1 = File.ReadAllText("weather.json");

            obj = JsonConvert.DeserializeAnonymousType(json1, obj);
        }
    }
}
