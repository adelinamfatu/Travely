using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.DTOs;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using Travely.Client.Resources.UIResources;
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
            await viewModel.InitializeCountry(tripId);
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

    private async void ConfirmAddSpot(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is string dayTitle)
        {
            if (string.IsNullOrEmpty(viewModel?.CurrentSpotName))
            {
                await DisplayAlert(ValidationResources.ErrorMessage, ValidationResources.EmptyLocationTitle, ValidationResources.OK);
                return;
            }

            bool isConfirmed = await DisplayAlert(ValidationResources.ConfirmLocationTitle, ValidationResources.ConfirmLocation, ValidationResources.Yes, ValidationResources.No);
            if (isConfirmed && viewModel != null)
            {
                viewModel.AddSpot(dayTitle);
            }
        }
    }

    private async void ConfirmDeleteSpot(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is SpotDTO spot)
        {
            bool isConfirmed = await DisplayAlert(TripsResources.MessageDeleteSpot, TripsResources.ConfirmDeleteSpot, TripsResources.Yes, TripsResources.No);
            if (isConfirmed && viewModel != null)
            {
                viewModel.DeleteSpot(spot);
            }
        }
    }
}