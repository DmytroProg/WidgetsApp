using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WidgetsApp.Storage;
using WidgetsApp.Storage.Interfaces;
using WidgetsApp.Storage.Repositories;
using WidgetsApp.WeatherAPI;
using WidgetsApp.WeatherAPI.Interfaces;

namespace WidgetsApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IWeatherAPIService _weatherApiService;
    private readonly ITodoListRepository _repository;
    
    public MainWindow()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();

        _weatherApiService = new WeatherAPIService(config);

        var options = new DbContextOptionsBuilder<WidgetsContext>()
            .UseSqlServer(config["ConnectionString"])
            .Options;
        
        var context = new WidgetsContext(options);
        _repository = new TodoListRepository(context);
        
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

    private async void TodosLB_OnLoaded(object sender, RoutedEventArgs e)
    {
        var todoLists = await _repository.GetTodoListsAsync();

        foreach (var list in todoLists)
        {
            todosLB.Items.Add(list.Title);
        }
    }
}