using WidgetsApp.WeatherAPI.Responses;

namespace WidgetsApp.WeatherAPI.Interfaces;

public interface IWeatherAPIService
{
    Task<WeatherData> GetCurrentWeatherAsync(string cityName);
}