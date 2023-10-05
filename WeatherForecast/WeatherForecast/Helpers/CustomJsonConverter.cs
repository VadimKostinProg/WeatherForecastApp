using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherForecast.Models;

namespace WeatherForecast.Helpers
{
    public class CustomJsonConverter : JsonConverter<WeatherForecastViewModel>
    {
        public override WeatherForecastViewModel? ReadJson(JsonReader reader, Type objectType, WeatherForecastViewModel? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var forecast = new WeatherForecastViewModel();

            forecast.WeatherId = jsonObject["weather"][0]["id"].Value<int>();
            forecast.Description = jsonObject["weather"][0]["description"].Value<string>();
            forecast.Icon = jsonObject["weather"][0]["icon"].Value<string>();
            forecast.Temperature = jsonObject["main"]["temp"].Value<double>();
            forecast.MaxTemperature = jsonObject["main"]["temp_max"].Value<double>();
            forecast.MinTemperature = jsonObject["main"]["temp_min"].Value<double>();
            forecast.Pressure = jsonObject["main"]["pressure"].Value<double>();
            forecast.Humidity = jsonObject["main"]["humidity"].Value<double>();
            forecast.WindSpeed = jsonObject["wind"]["speed"].Value<double>();
            forecast.Cloud = jsonObject["clouds"]["all"].Value<double>();
            forecast.City = jsonObject["name"].Value<string>();

            return forecast;
        }

        public override void WriteJson(JsonWriter writer, WeatherForecastViewModel? value, JsonSerializer serializer)
        {
            var jsonObject = new JObject();

            jsonObject["weather:id"] = value.WeatherId;
            jsonObject["weather:description"] = value.Description;
            jsonObject["weather:icon"] = value.Icon;
            jsonObject["main:temp"] = value.Temperature;
            jsonObject["main:temp_max"] = value.MaxTemperature;
            jsonObject["main:temp_min"] = value.MinTemperature;
            jsonObject["main:pressure"] = value.Pressure;
            jsonObject["main:humidity"] = value.Humidity;
            jsonObject["wind:speed"] = value.WindSpeed;
            jsonObject["clouds:all"] = value.Cloud;
            jsonObject["name"] = value.City;

            jsonObject.WriteTo(writer);
        }
    }
}
