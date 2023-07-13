using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        //public const string API_KEY = "fbvsYPxbAjBzAIJ3YicLZCDAlXwUEiWf";

        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return ConfigurationManager.AppSettings[key] ?? "Not Found";
                //return Properties.Settings.Default.SettingsKey;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                throw;
            }
        }

        //var API_KEY = ConfigurationManager.AppSettings["APIKey"];

        public static async Task<List<City>> GetCities(string query)
        {
            var cities = new List<City>();

            var url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, ReadSetting("AccuWeatherAppKey"), query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
                else
                {
                    Console.WriteLine("Internal server Error: " + response.StatusCode);
                }

                return cities;
            }
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string citiKey)
        {
            var url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, citiKey, ReadSetting("AccuWeatherAppKey"));

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();

                return conditions;
            }
        }
    }
}
