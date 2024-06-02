using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using static Travely.Client.Utilities.Messenger;

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
            await viewModel.LoadItinerary(tripId);
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<ReloadSpotsMessage>(this, async (sender, message) =>
            {
                await viewModel.LoadItinerary(tripId);
            });
        }
    }

    private async void NavigateToMapsPage(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is Guid tripId)
        {
            var mapPage = new MapPage(tripId);
            mapPage.LocationPinned += MapPage_LocationPinned;
            await Navigation.PushAsync(mapPage);
        }
    }

    private void MapPage_LocationPinned(object? sender, Tuple<double, double> coordinates)
    {
        double spotLatitude = coordinates.Item1;
        double spotLongitude = coordinates.Item2;
        if (viewModel is not null)
        {
            viewModel.GetSpotData(spotLatitude, spotLongitude);
        }
    }
}