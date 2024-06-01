using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using static Travely.Client.Utilities.Messenger;
using Travely.Client.Resources.UIResources;

namespace Travely.Client.Models
{
    public partial class TripPackingViewModel : ObservableObject
    {
        private readonly PackingService packingService;

        [ObservableProperty]
        private ObservableCollection<PackingItemDTO> packingItems;

        [ObservableProperty]
        private string errorMessage;

        private string packingItem;

        public string PackingItem
        {
            get => packingItem;
            set
            {
                SetProperty(ref packingItem, value);
                if (string.IsNullOrEmpty(value))
                {
                    ErrorMessage = string.Empty;
                }
            }
        }

        public TripPackingViewModel(PackingService packingService)
        {
            this.packingService = packingService;
            this.PackingItems = new ObservableCollection<PackingItemDTO>();
            this.packingItem = string.Empty;
            this.errorMessage = string.Empty;
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
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(PackingItem))
            {
                ErrorMessage = ValidationResources.EmptyItemError;
                return;
            }

            if (PackingItems.Any(item => item.Title.Equals(PackingItem, StringComparison.OrdinalIgnoreCase)))
            {
                ErrorMessage = ValidationResources.ExistItemError;
                return;
            }

            if (!Regex.IsMatch(PackingItem, @"^[a-zA-Z0-9 ]+$"))
            {
                ErrorMessage = ValidationResources.InvalidItemError;
                return;
            }
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
