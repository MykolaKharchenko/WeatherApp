using System.Collections.Generic;

namespace WeatherApp.DataModels
{
    public class WeatherDataCollection
    {
        public string Message { get; set; }
        public string Cod { get; set; }
        public int Count { get; set; }
        public List<CityWeatherInfo> List { get; set; }
    }
    public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int? Sea_level { get; set; }
        public int? Grnd_level { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    public class Sys
    {
        public string Country { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class CityWeatherInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public Main Main { get; set; }
        public int Dt { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public object Rain { get; set; }
        public object Snow { get; set; }
        public Clouds Clouds { get; set; }
        public List<Weather> Weather 
        {       // rewrite  method -> ctor must return  Single First Weather value
            get;
            set;
        }
    }

}
