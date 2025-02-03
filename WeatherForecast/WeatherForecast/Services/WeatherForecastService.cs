using Newtonsoft.Json;
using System.Net.Http.Json;
using WeatherForecast.Helpers;
using WeatherForecast.Models;
using WeatherForecast.ServiceContracts;

namespace WeatherForecast.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherForecastService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<WeatherForecastViewModel> GetWeatherForecastForCity(string city)
    {
        try
        {
            var url = _configuration["OpenWeatherSettings:API_base"] + $"&q={city}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var forecast = JsonConvert.DeserializeObject<WeatherForecastViewModel>(jsonContent, new CustomJsonConverter())!;

                return forecast;
            }

            throw new ArgumentException("Cannot find weather forecast for entered city.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            throw;
        }
    }
}
