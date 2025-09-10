using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.Configuration;
using WidgetsApp.WeatherAPI;
using WidgetsApp.WeatherAPI.Interfaces;

namespace WidgetsApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IWeatherAPIService _weatherApiService;
    
    public MainWindow()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();

        _weatherApiService = new WeatherAPIService(config);
        
        InitializeComponent();
    }

    private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
        var weather = await _weatherApiService.GetCurrentWeatherAsync("London");

        weatherNameTb.Text = weather.Weather[0].Main;
        tempTb.Text = weather.Main.Temp + " C";
        weatherImg.Source = new BitmapImage(
            new Uri($"https://openweathermap.org/img/wn/{weather.Weather[0].Icon}@4x.png"));
    }
}