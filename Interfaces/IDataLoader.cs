using System.Collections.Generic;
using WeatherApp.DataModels;

namespace WeatherApp.Interfaces
{
    public interface IDataLoader
    {
        IEnumerable<CityWeatherInfo> Load();

        void Save(IEnumerable<CityWeatherInfo> _cityId);
    }
}
