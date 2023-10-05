using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherForecast.Models;
using WeatherForecast.ServiceContracts;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromForm] string city)
        {
            try
            {
                var forecast = await _weatherForecastService.GetWeatherForecastForCity(city);

                return View(forecast);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(null);
            }
        }
    }
}