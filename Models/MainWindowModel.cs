using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using WeatherApp.DataModels;
using WeatherApp.Interfaces;
using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class MainWindowModel
    {
        private IWeatherService _weatherService;
        private IDataLoader _loadDataService;
        private List<CityWeatherInfo> CitiesStorage = new List<CityWeatherInfo>();

        [InjectionConstructor]
        public MainWindowModel(IWeatherService weatherService, IDataLoader loadDataService)
        {
            _loadDataService =loadDataService;
            _weatherService = weatherService;
        }
        public async Task<IEnumerable<CityWeatherInfo>> FindMatchedCities(string inputedValue)
        {
            return await _weatherService.GetMatchedCitiesAsync(inputedValue);
        }

        public async Task<CityWeatherInfo> UpdateCityData(int id)
        {
            return await _weatherService.GetCityByIdAsync(id.ToString());
        }

        public IEnumerable<CityWeatherInfo> LoadCities()
        {
            return _loadDataService.Load();
        }

        public void SaveCities(IEnumerable<CityWeatherInfo> _cities)
        {
            _loadDataService.Save(_cities);
        }

    }
}
