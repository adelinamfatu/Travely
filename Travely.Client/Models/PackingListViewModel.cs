using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Travely.Client.Models
{
    public partial class PackingListViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? itemInput;

        [ObservableProperty]
        private ObservableCollection<string> items = new();

        [RelayCommand]
        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(ItemInput))
            {
                Items.Add(ItemInput);
                ItemInput = string.Empty;
            }
        }
    }
}
