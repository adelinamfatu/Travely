using CommunityToolkit.Mvvm.Messaging;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
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
        await Navigation.PushAsync(new EditTripPage());
    }
}