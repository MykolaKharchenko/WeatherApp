using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using WeatherApp.DataModels;
using WeatherApp.Interfaces;

namespace WeatherApp.Services
{
    public class LocalDataLoader : IDataLoader
    {
        public string pathToFile = "CitiesDb.json";

        public IEnumerable<CityWeatherInfo> Load()
        {
            if (!File.Exists(pathToFile))
                return Enumerable.Empty<CityWeatherInfo>();

            using var reader = new StreamReader(pathToFile);
            var json = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<IEnumerable<CityWeatherInfo>>(json);
            if (result == null)
                result = Enumerable.Empty<CityWeatherInfo>();
            return result;
        }

        public void Save(IEnumerable<CityWeatherInfo> cities)
        {
            var result = JsonConvert.SerializeObject(cities);
            File.WriteAllText(pathToFile, result);
        }
    }
}
