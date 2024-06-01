using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Pages;

public partial class EditTripPage : ContentPage
{
    public EditTripViewModel? viewModel { get; set; }

    public EditTripPage(Guid tripId)
    {
        InitializeComponent();
        InitializeViewModel(tripId);
    }

    private async void InitializeViewModel(Guid tripId)
    {
        var tripService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripService>();
        if (tripService is not null)
        {
            viewModel = new EditTripViewModel(tripId, tripService);
            await viewModel.LoadTrip();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<ReloadFlightsMessage>(this, async (sender, message) =>
            {
                await viewModel.LoadTrip();
            });
        }
    }

    private void HandleChevronHotelsDownClicked(object sender, EventArgs e)
    {
        viewModel?.ToggleHotelsExpanded();
    }

    private void HandleChevronNotesClicked(object sender, EventArgs e)
    {
        viewModel?.ToggleNotesExpanded();
    }

    private void HandleChevronDepartureClicked(object sender, EventArgs e)
    {
        viewModel?.ToggleDepartureExpanded();
    }

    private void HandleChevronArrivalClicked(object sender, EventArgs e)
    {
        viewModel?.ToggleArrivalExpanded();
    }

    private async void NavigateToItineraryPage(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Guid tripId)
        {
            await Navigation.PushAsync(new ItineraryPage(tripId));
        }
    }
}