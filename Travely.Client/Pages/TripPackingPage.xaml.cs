using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Pages;

public partial class TripPackingPage : ContentPage
{
    TripPackingViewModel? viewModel {  get; set; }

    public TripPackingPage()
    {
        InitializeComponent();
        InitializeViewModel();
    }

    private async void InitializeViewModel()
    {
        var packingService = Application.Current?.Handler?.MauiContext?.Services.GetService<PackingService>();
        if (packingService is not null )
        {
            viewModel = new TripPackingViewModel(packingService);
            await viewModel.LoadPackingItems();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<ReloadPackingItemsMessage>(this, async (sender, message) =>
            {
                await viewModel.LoadPackingItems();
            });
        }
    }

    private void IsPackedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is PackingItemDTO item)
        {
            viewModel?.UpdatePackingItemIsPacked(item, e.Value);
        }
    }
}