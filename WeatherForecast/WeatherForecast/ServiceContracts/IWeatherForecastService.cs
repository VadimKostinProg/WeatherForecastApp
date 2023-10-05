using WeatherForecast.Models;

namespace WeatherForecast.ServiceContracts
{
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Method for getting weather forecast for current date for specified city.
        /// </summary>
        /// <param name="city">City to get weather forecast.</param>
        /// <returns>Weather forecast data object.</returns>
        Task<WeatherForecastViewModel> GetWeatherForecastForCity(string city);
    }
}
