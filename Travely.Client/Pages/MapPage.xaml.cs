using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using Travely.Client.Resources.UIResources;

namespace Travely.Client.Pages;

public partial class MapPage : ContentPage
{
    public MapViewModel? viewModel { get; set; }

    public event EventHandler<Tuple<double, double>>? LocationPinned;

    private bool isMapPositionUpdated = false;

    public MapPage(Guid tripId)
	{
		InitializeComponent();
        InitializeViewModel(tripId);
    }

    private async void InitializeViewModel(Guid tripId)
    {
        var tripDetailService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripDetailService>();
        if (tripDetailService is not null)
        {
            viewModel = new MapViewModel(tripDetailService);
            await viewModel.InitializeCountry(tripId);
            BindingContext = viewModel;
        }
    }

    private async void PinLocationOnMap(object sender, MapClickedEventArgs e)
    {
        var tappedLocation = e.Location;
        await DisplayConfirmation(tappedLocation.Latitude, tappedLocation.Longitude);
    }

    private async Task DisplayConfirmation(double latitude, double longitude)
    {
        var confirmation = await DisplayAlert(ValidationResources.ConfirmLocationTitle, 
            ValidationResources.ConfirmLocation, 
            ValidationResources.Yes, 
            ValidationResources.No);
        if (confirmation)
        {
            LocationPinned?.Invoke(this, new Tuple<double, double>(latitude, longitude));
            await Navigation.PopAsync();
        }
    }

    private void OnSearchEntryFocused(object sender, FocusEventArgs e)
    {
        if (!isMapPositionUpdated)
        {
            if (viewModel != null && map != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Location(viewModel.CountryLatitude, viewModel.CountryLongitude),
                    Distance.FromMiles(50))
                );
            }
            isMapPositionUpdated = true;
        }
    }
}