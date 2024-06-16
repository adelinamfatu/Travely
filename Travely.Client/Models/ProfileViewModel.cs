using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using System.Text.Json;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;
using FileSystem = Microsoft.Maui.Storage.FileSystem;
using System.Text.RegularExpressions;
using Travely.Client.Resources.UIResources;

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
        private Chart? seasonsSpendingChart;

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

            var seasonSpending = statisticService.GetSeasonalSpending();
            var seasonEntries = seasonSpending.Select(kvp => new ChartEntry((float)kvp.Value)
            {
                Label = kvp.Key,
                ValueLabel = kvp.Value.ToString(),
                Color = GenerateRandomColor()
            }).ToArray();

            SeasonsSpendingChart = new BarChart { Entries = seasonEntries, ValueLabelOrientation = Orientation.Horizontal };
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

                await ShowAlert(ValidationResources.SuccesTitleName, ValidationResources.SuccessMessageName, ValidationResources.OK);
            }
            else
            {
                await ShowAlert(ValidationResources.ErrorTitleName, ValidationResources.ErrorMessageName, ValidationResources.OK);
            }
        }

        public Task ShowAlert(string title, string message, string cancel)
        {
            return MainThread.InvokeOnMainThreadAsync(() =>
                Application.Current?.MainPage?.DisplayAlert(title, message, cancel));
        }
    }
}
