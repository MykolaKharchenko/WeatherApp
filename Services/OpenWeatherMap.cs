using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public async Task<List<CityWeatherInfo>> GetMatchedCitiesAsync(string searchedData)
        {
            Uri apiUri = new Uri($"{baseUri}find?q={searchedData}&cnt=50&type=like&units=metric{appId}");
            var responseBody = await client.GetStringAsync(apiUri);

            WeatherDataCollection dataCol = new WeatherDataCollection();
            dataCol = JsonConvert.DeserializeObject<WeatherDataCollection>(responseBody);

            return dataCol.List;
        }

        //public CityWeatherInfo GetCityById(string searchedData) 
        //{
        //    var city = new CityWeatherInfo();
        //    WebRequest request = WebRequest.Create($"{baseUri}weather?id={searchedData}&units=metric{appId}");
        //    WebResponse response = request.GetResponse();

        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            string jsonData = reader.ReadToEnd();
        //            city = JsonConvert.DeserializeObject<CityWeatherInfo>(jsonData);
        //        }
        //    }
        //    response.Close();
        //    return city;
        //}

        //public List<CityWeatherInfo> GetMatchedCities(string searchedData)
        //{
        //    WeatherDataCollection dataCol;
        //    WebRequest request = WebRequest.Create($"{baseUri}find?q={searchedData}&cnt=50&type=like&units=metric{appId}");
        //    WebResponse response = request.GetResponse();

        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            string jsonData = reader.ReadToEnd();
        //            dataCol = JsonConvert.DeserializeObject<WeatherDataCollection>(jsonData);
        //        }
        //    }
        //    response.Close();
        //    return dataCol.List;
        //}
    }
}
