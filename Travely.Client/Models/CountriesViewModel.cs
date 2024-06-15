using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class CountriesViewModel : BindableObject
    {
        private readonly CountryService _countryService;
        private ObservableCollection<string> _countries;
        private ObservableCollection<string> _filteredCountries;
        private ObservableCollection<string> _beachDestinations;

        public ObservableCollection<string> Countries
        {
            get => _countries;
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FilteredCountries
        {
            get => _filteredCountries;
            set
            {
                _filteredCountries = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> BeachDestinations
        {
            get => _beachDestinations;
            set
            {
                _beachDestinations = value;
                OnPropertyChanged();
            }
        }

        public CountriesViewModel()
        {
            _countryService = new CountryService();
            Countries = new ObservableCollection<string>();
            FilteredCountries = new ObservableCollection<string>();
            BeachDestinations = new ObservableCollection<string>();
            LoadCountries();
            LoadBeachDestinations();
        }

        private async void LoadCountries()
        {
            var countries = await _countryService.GetAllCountries();
            foreach (var country in countries)
            {
                Countries.Add(country["name"].ToString());
            }

            Console.WriteLine($"Number of countries added to collection: {Countries.Count}");
        }

        public async void LoadFilteredCountries()
        {
            var countries = await _countryService.GetFilteredCountriesByLatLong();
            foreach (var country in countries)
            {
                FilteredCountries.Add(country["name"].ToString());
            }

            Console.WriteLine($"Number of filtered countries added to collection: {FilteredCountries.Count}");
        }

        private void LoadBeachDestinations()
        {
            var beachDestinations = _countryService.GetPopularBeachDestinations();
            foreach (var destination in beachDestinations)
            {
                BeachDestinations.Add(destination["name"].ToString());
            }

            Console.WriteLine($"Number of beach destinations added to collection: {BeachDestinations.Count}");
        }
    }
}
