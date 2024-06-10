using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using System.Text.Json;
using Travely.Client.Utilities;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace Travely.Client.Models
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private Chart? continentChart;

        [ObservableProperty]
        private Chart? europeanCountriesChart;

        [ObservableProperty]
        private Chart? seasonsFrequencyChart;

        public ProfileViewModel()
        {
            InitializeCharts();
            LoadName();
        }

        private void InitializeCharts()
        {
            var continentEntries = new[]
            {
                new ChartEntry(25) { Label = "Asia", ValueLabel = "25%", Color = SKColor.Parse("#ff6347") },
                new ChartEntry(30) { Label = "Europe", ValueLabel = "30%", Color = SKColor.Parse("#6495ed") },
                new ChartEntry(20) { Label = "Africa", ValueLabel = "10%", Color = SKColor.Parse("#dda0dd") },
                new ChartEntry(15) { Label = "North America", ValueLabel = "20%", Color = SKColor.Parse("#ff4500") },
                new ChartEntry(10) { Label = "South America", ValueLabel = "15%", Color = SKColor.Parse("#228b22") },
                new ChartEntry(5) { Label = "Australia", ValueLabel = "5%", Color = SKColor.Parse("#deb887") },
                new ChartEntry(1) { Label = "Antarctica", ValueLabel = "1%", Color = SKColor.Parse("#b0e0e6") }
            };

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

            ContinentChart = new PieChart { Entries = continentEntries };
            EuropeanCountriesChart = new PieChart { Entries = europeCountryEntries };
            SeasonsFrequencyChart = new BarChart { Entries = seasonEntries, ValueLabelOrientation = Orientation.Horizontal };
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

        [RelayCommand]
        private async Task SaveNameAsync()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var cacheDirectory = FileSystem.CacheDirectory;
                var userNameFilePath = Path.Combine(cacheDirectory, Constants.UserNameCacheFileName);

                string json = JsonSerializer.Serialize(Name);
                await File.WriteAllTextAsync(userNameFilePath, json);
            }
        }
    }
}
