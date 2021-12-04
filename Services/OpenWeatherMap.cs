using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.DataModels;
using WeatherApp.Interfaces;

namespace WeatherApp.Services
{
    public class OpenWeatherMap : IWeatherService
    {
        private static readonly string baseUri = "https://api.openweathermap.org/data/2.5/";
        private static readonly string appId = "&appid=31e9f5500a70ce19e013df137c0e30f6";
        private static HttpClient client = new HttpClient();

        public async Task<CityWeatherInfo> GetCityByIdAsync(string searchedData)
        {
            Uri apiUri = new Uri($"{baseUri}weather?id={searchedData}&units=metric{appId}");
            var responseBody = await client.GetStringAsync(apiUri);
            var city = JsonConvert.DeserializeObject<CityWeatherInfo>(responseBody);

            return city;
        }

        public async Task<IEnumerable<CityWeatherInfo>> GetMatchedCitiesAsync(string searchedData)
        {
            Uri apiUri = new Uri($"{baseUri}find?q={searchedData}&cnt=50&type=like&units=metric{appId}");
            var responseBody = await client.GetStringAsync(apiUri);

            WeatherDataCollection dataCol = new WeatherDataCollection();
            dataCol = JsonConvert.DeserializeObject<WeatherDataCollection>(responseBody);

            return dataCol.List;
        }
    }
}
