using Travely.BusinessLogic.Services;
using Travely.Client.Models;

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
        if (tripService != null)
        {
            viewModel = new TripsViewModel(tripService);
            await viewModel.LoadTrips();
            BindingContext = viewModel;
        }
    }

    private async void StartPlanning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlanTripPage());
    }

    private async void NavigateToEditTripPage(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TripViewModel selectedTrip)
        {
            await Shell.Current.GoToAsync("///EditTripPage");
        }
    }

}