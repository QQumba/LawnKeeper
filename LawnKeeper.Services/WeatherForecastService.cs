using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;

namespace LawnKeeper.Services
{
    public class WeatherForecastService
    {
        private readonly string _forecastUri;
        public WeatherForecastService(string forecastUri)
        {
            _forecastUri = forecastUri;
        }

        public async Task<string> GetForecast()
        {
            var client = new HttpClient();
            var forecastString = await client.GetAsync(_forecastUri);
            return forecastString.Content.ToString();
        }
    }
}