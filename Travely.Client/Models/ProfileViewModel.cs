using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using System.Text.Json;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;
using FileSystem = Microsoft.Maui.Storage.FileSystem;
using System.Text.RegularExpressions;

namespace Travely.Client.Models
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly StatisticService statisticService;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private Chart? countriesChart;

        [ObservableProperty]
        private Chart? europeanCountriesChart;

        [ObservableProperty]
        private Chart? seasonsFrequencyChart;

        public ProfileViewModel(StatisticService statisticService)
        {
            this.statisticService = statisticService;
            LoadName();
        }

        public void InitializeCharts()
        {
            var countries = statisticService.GetTripCountries();
            var countryEntries = countries.Select(kvp => new ChartEntry(kvp.Value)
            {
                Label = kvp.Key,
                ValueLabel = kvp.Value.ToString(),
                Color = GenerateRandomColor()
            }).ToArray();
            CountriesChart = new PieChart { Entries = countryEntries };

            var europeCountryEntries = new[]
            {
                new ChartEntry(30) { Label = "France", ValueLabel = "30%", Color = SKColor.Parse("#6495ed") },
                new ChartEntry(25) { Label = "Italy", ValueLabel = "25%", Color = SKColor.Parse("#ff6347") },
                new ChartEntry(20) { Label = "Germany", ValueLabel = "20%", Color = SKColor.Parse("#ffa500") },
                new ChartEntry(15) { Label = "Spain", ValueLabel = "15%", Color = SKColor.Parse("#008000") },
                new ChartEntry(10) { Label = "United Kingdom", ValueLabel = "10%", Color = SKColor.Parse("#000080") }
            };

            var seasonEntries = new[]
            {
                new ChartEntry(300) { Label = "Spring", ValueLabel = "300", Color = SKColor.Parse("#ffa500") },
                new ChartEntry(450) { Label = "Summer", ValueLabel = "450", Color = SKColor.Parse("#6495ed") },
                new ChartEntry(200) { Label = "Autumn", ValueLabel = "200", Color = SKColor.Parse("#deb887") },
                new ChartEntry(150) { Label = "Winter", ValueLabel = "150", Color = SKColor.Parse("#b0e0e6") }
            };
            
            EuropeanCountriesChart = new PieChart { Entries = europeCountryEntries };
            SeasonsFrequencyChart = new BarChart { Entries = seasonEntries, ValueLabelOrientation = Orientation.Horizontal };
        }

        private SKColor GenerateRandomColor()
        {
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);
            return new SKColor(colorBytes[0], colorBytes[1], colorBytes[2]);
        }

        private async void LoadName()
        {
            var cacheDirectory = FileSystem.CacheDirectory;
            var userNameFilePath = Path.Combine(cacheDirectory, Utilities.Constants.UserNameCacheFileName);

            if (File.Exists(userNameFilePath))
            {
                string json = await File.ReadAllTextAsync(userNameFilePath);
                Name = JsonSerializer.Deserialize<string>(json);
            }
        }

        private bool ValidateName(string name)
        {
            return Regex.IsMatch(name, "^[A-Z][a-z]+$");
        }

        [RelayCommand]
        private async Task SaveNameAsync()
        {
            if (!string.IsNullOrEmpty(Name) && ValidateName(Name))
            {
                var cacheDirectory = FileSystem.CacheDirectory;
                var userNameFilePath = Path.Combine(cacheDirectory, Constants.UserNameCacheFileName);

                string json = JsonSerializer.Serialize(Name);
                await File.WriteAllTextAsync(userNameFilePath, json);

                await ShowAlert("Success", "Name has been saved successfully!", "OK");
            }
            else
            {
                await ShowAlert("Validation Error", "Please enter a valid name. Names must start with a capital letter followed by lowercase letters and contain no spaces.", "OK");
            }
        }

        public Task ShowAlert(string title, string message, string cancel)
        {
            return MainThread.InvokeOnMainThreadAsync(() =>
                Application.Current?.MainPage?.DisplayAlert(title, message, cancel));
        }
    }
}
