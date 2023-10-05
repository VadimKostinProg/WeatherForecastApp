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
            ViewBag.City = Request.Cookies["city"] ?? string.Empty;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string city)
        {
            try
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(2);

                Response.Cookies.Append("city", city, cookieOptions);

                var forecast = await _weatherForecastService.GetWeatherForecastForCity(city);

                ViewBag.ToNotifyAboutRain = forecast.Main == "Rain" && !IsNotificationSent();

                if (ViewBag.ToNotifyAboutRain)
                {
                    Response.Cookies.Append("lastNotificated", DateTime.Now.ToShortDateString(), cookieOptions);
                }

                return View(forecast);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(null);
            }
        }

        private bool IsNotificationSent()
        {
            var lastNotificated = Request.Cookies["lastNotificated"];

            return lastNotificated != null && Convert.ToDateTime(lastNotificated) == DateTime.Today;
        }
    }
}