using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using Travely.Client.Resources.UIResources;
using static Travely.Client.Utilities.Messenger;

namespace Travely.Client.Pages;

public partial class TripsPage : ContentPage
{
    private TripsViewModel? viewModel;

    public TripsPage()
    {
        InitializeComponent();
        InitializeViewModel();
    }

    private async void InitializeViewModel()
    {
        var tripService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripService>();
        if (tripService is not null)
        {
            viewModel = new TripsViewModel(tripService);
            await viewModel.LoadTrips();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<ReloadTripsMessage>(this, async (sender, message) =>
            {
                await viewModel.LoadTrips();
            });
        }
    }

    private async void StartPlanning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlanTripPage());
    }

    private async void NavigateToEditTripPage(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is Guid tripId)
        {
            await Navigation.PushAsync(new EditTripPage(tripId));
        }
    }

    private async void OnDeleteTripClicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert(TripsResources.MessageDeleteTrip, TripsResources.ConfirmDeleteTrip, "Yes", "No");
        if (isConfirmed)
        {
            if (sender is ImageButton button && button.CommandParameter is Guid tripId)
            {
                var tripViewModel = (button.BindingContext as TripViewModel);
                tripViewModel?.ExecuteDeleteCommand(tripId);
            }
        }
    }
}