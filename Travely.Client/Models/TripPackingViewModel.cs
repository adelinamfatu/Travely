using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using Travely.Client.Resources.UIResources;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Models
{
    public partial class TripPackingViewModel : ObservableObject
    {
        private readonly PackingService packingService;

        [ObservableProperty]
        private string packingItem;

        [ObservableProperty]
        private ObservableCollection<PackingItemDTO> packingItems;

        private string AddPackingItemMessage = "";

        public TripPackingViewModel(PackingService packingService)
        {
            this.packingService = packingService;
            this.PackingItems = new ObservableCollection<PackingItemDTO>();
            this.packingItem = string.Empty;
        }

        public async Task LoadPackingItems()
        {
            if (PackingItems.Any())
            {
                PackingItems.Clear();
            }

            var items = await packingService.GetPackingItems();

            foreach (var item in items)
            {
                PackingItems.Add(item);
            }
        }

        [RelayCommand]
        private void AddItem()
        {
            packingService.AddPackingItem(new PackingItemDTO
            {
                Title = PackingItem,
                IsPacked = false,
            });

            PackingItem = string.Empty;

            WeakReferenceMessenger.Default.Send(new ReloadPackingItemsMessage());
            AddPackingItemMessage = ValidationResources.AddPackingItemSuccess;
        }

        [RelayCommand]
        private void DeleteItem(string itemTitle)
        {
            packingService.DeletePackingItem(itemTitle);
            WeakReferenceMessenger.Default.Send(new ReloadPackingItemsMessage());
        }
    }
}
