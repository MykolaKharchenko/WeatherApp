using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.DataModels;
using WeatherApp.Interfaces;
using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class MainWindowModel
    {
        private IWeatherService weatherService;
        private IDataLoader loadDataService;
        private List<CityWeatherInfo> CitiesStorage = new List<CityWeatherInfo>();

        public MainWindowModel()
        {
            loadDataService = new LocalDataLoader();
            weatherService = new OpenWeatherMap();
        }
        public async Task<List<CityWeatherInfo>> FindMatchedCities(string inputedValue)
        {
            return await weatherService.GetMatchedCitiesAsync(inputedValue);
        }

        public async Task<CityWeatherInfo> UpdateCityData(int id)
        {
            return await weatherService.GetCityByIdAsync(id.ToString());
        }

        public IEnumerable<CityWeatherInfo> LoadCities()
        {
            return loadDataService.Load();         
        }

        public void SaveCities(IEnumerable<CityWeatherInfo> _cities)
        {
            loadDataService.Save(_cities);
        }

    }
}
