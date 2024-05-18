using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;

namespace Travely.Client.Models
{
    public partial class TripPackingViewModel : ObservableObject
    {
        private readonly PackingService packingService;

        [ObservableProperty]
        private ObservableCollection<PackingItemDTO> packingItems;

        public TripPackingViewModel(PackingService packingService)
        {
            this.packingService = packingService;
            this.PackingItems = new ObservableCollection<PackingItemDTO>();
        }

        public async Task LoadPackingItems()
        {
            var items = await packingService.GetPackingItems();

            foreach (var item in items)
            {
                PackingItems.Add(item);
            }
        }

        /*[RelayCommand]
        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(ItemInput))
            {
                Items.Add(ItemInput);
                ItemInput = string.Empty;
            }
        }*/
    }
}
