using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class PlanTripPage : ContentPage
{
    private readonly TripViewModel? viewModel;

    public PlanTripPage()
    {
        InitializeComponent();
        var tripService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripService>();
        if (tripService != null)
        {
            viewModel = new TripViewModel(tripService);
        }
        BindingContext = viewModel;
    }

    private async void OnAddTripClicked(object sender, EventArgs e)
    {
        if (viewModel != null)
        {
            viewModel.AddTrip();
            await DisplayAlert(viewModel.LastAddTripMessage.Contains("successfully") ? "Success" : "Error", viewModel.LastAddTripMessage, "OK");

            await Navigation.PopAsync();
        }
    }
}