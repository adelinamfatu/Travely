using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
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

        public void UpdatePackingItemIsPacked(PackingItemDTO item, bool value)
        {
            packingService.UpdatePackingItem(item.Id, item.IsPacked);
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
        }

        [RelayCommand]
        private void DeleteItem(Guid itemId)
        {
            packingService.DeletePackingItem(itemId);
            WeakReferenceMessenger.Default.Send(new ReloadPackingItemsMessage());
        }
    }
}
