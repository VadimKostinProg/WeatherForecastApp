namespace WeatherForecast.Models
{
    public class WeatherForecastViewModel
    {
        public int WeatherId { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconUrl { get => @"http://openweathermap.org/img/w/" + Icon + ".png"; }

        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }

        public double WindSpeed { get; set; }
        public double Cloud { get; set; }

        public string City { get; set; }
    }
}
