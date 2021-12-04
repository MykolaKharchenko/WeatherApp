using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.DataModels;

namespace WeatherApp.Interfaces
{
    public interface IWeatherService
    {
        //public CityWeatherInfo GetCityById(string inputedValue);
        //public List<CityWeatherInfo> GetMatchedCities(string searchedData);



        public Task<CityWeatherInfo> GetCityByIdAsync(string searchedData);
        public Task<List<CityWeatherInfo>> GetMatchedCitiesAsync(string searchedData);
    }
}
