using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class TripsPage : ContentPage
{
    private TripsViewModel viewModel;

    public TripsPage()
    {
        InitializeComponent();
        var tripService = Application.Current.Handler.MauiContext.Services.GetService<TripService>();
        if (tripService != null)
        {
            viewModel = new TripsViewModel(tripService);
        }
        BindingContext = viewModel;
    }

    private async void StartPlanning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlanTripPage());
    }
}