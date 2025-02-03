using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Constants;
using WeatherForecast.ServiceContracts;

namespace WeatherForecast.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    private string GetLastNotifiedCookiesKey(string city) => 
        WeatherForecastConstants.LastNotificatedPrefix + city.Replace(' ', '-');

    public HomeController(ILogger<HomeController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    public IActionResult Index()
    {
        ViewBag.City = Request.Cookies[WeatherForecastConstants.LastCityKey] ?? string.Empty;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Details(string city)
    {
        try
        {
            city = city.Trim();

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2);

            Response.Cookies.Append(WeatherForecastConstants.LastCityKey, city, cookieOptions);

            var forecast = await _weatherForecastService.GetWeatherForecastForCity(city);

            ViewBag.ToNotifyAboutRain = forecast.Main == WeatherForecastConstants.Rain && !IsNotificationSent(city);

            if (ViewBag.ToNotifyAboutRain)
            {
                Response.Cookies.Append(GetLastNotifiedCookiesKey(city), DateTime.Now.ToShortDateString(), cookieOptions);
            }

            return View(forecast);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;

            return View();
        }
    }

    private bool IsNotificationSent(string city)
    {
        var lastNotificated = Request.Cookies[GetLastNotifiedCookiesKey(city)];

        return !string.IsNullOrEmpty(lastNotificated) && Convert.ToDateTime(lastNotificated).Date == DateTime.Today;
    }
}