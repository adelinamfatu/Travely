namespace Travely.Client.Pages;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;

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
        }
    }
}