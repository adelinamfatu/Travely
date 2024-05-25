using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class ItineraryPage : ContentPage
{
    public ItineraryViewModel? viewModel { get; set; }

    public ItineraryPage(Guid tripId)
	{
		InitializeComponent();
        InitializeViewModel(tripId);
    }

    private async void InitializeViewModel(Guid tripId)
    {
        var tripDetailService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripDetailService>();
        if (tripDetailService is not null)
        {
            viewModel = new ItineraryViewModel(tripDetailService);
            await viewModel.InitializeItinerary(tripId);
            BindingContext = viewModel;
        }
    }
}