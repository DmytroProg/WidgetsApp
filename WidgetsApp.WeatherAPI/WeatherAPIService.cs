using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WidgetsApp.WeatherAPI.Interfaces;
using WidgetsApp.WeatherAPI.Responses;

namespace WidgetsApp.WeatherAPI;

public class WeatherAPIService : IWeatherAPIService
{
    private readonly IConfiguration _configuration;

    public WeatherAPIService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<WeatherData> GetCurrentWeatherAsync(string cityName)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");

        var response = await client.GetAsync($"?q={cityName}&units=metric&appid={_configuration["API_KEY"]}");

        var json = await response.Content.ReadAsStringAsync();

        var weatherData = JsonSerializer.Deserialize<WeatherData>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return weatherData ?? throw new ArgumentException();
    }
}