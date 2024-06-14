using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public class CountriesViewModel: BindableObject
    {
        private readonly CountryService _countryService;
        private ObservableCollection<string> _countries;

        public ObservableCollection<string> Countries
        {
            get => _countries;
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        public CountriesViewModel()
        {
            _countryService = new CountryService();
            Countries = new ObservableCollection<string>();
            LoadCountries();
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

    }
}
