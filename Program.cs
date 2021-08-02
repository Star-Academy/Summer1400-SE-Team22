using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Summer1400_SE_Team22
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader("Database/Students.json"))
            {
                WeatherForecast weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString);
                var serializedItem = JsonSerializer.;
            }
        }
    }


}
