using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class MapPage : ContentPage
{
    public MapViewModel? viewModel { get; set; }

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

            UpdateMapPosition();
        }
    }

    private void UpdateMapPosition()
    {
        if (viewModel != null && map != null)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(viewModel.Latitude, viewModel.Longitude),
                Distance.FromMiles(50))
            );
        }
    }
}