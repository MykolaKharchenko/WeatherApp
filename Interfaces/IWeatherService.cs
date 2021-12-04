using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.DataModels;

namespace WeatherApp.Interfaces
{
    public interface IWeatherService
    {
        public Task<CityWeatherInfo> GetCityByIdAsync(string searchedData);
        public Task<IEnumerable<CityWeatherInfo>> GetMatchedCitiesAsync(string searchedData);
    }
}
