using System.Text.RegularExpressions;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;
using Travely.Client.Resources.UIResources;

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
            bool isSuccess = viewModel.AddTripMessage.Contains("successfully");

            await DisplayAlert(isSuccess ? 
                ValidationResources.SuccessMessage : ValidationResources.ErrorMessage, viewModel.AddTripMessage, "OK");

            if (isSuccess)
            {
                await Navigation.PopAsync();
            }
        }
    }

    private void ValidateTripTitle(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        var tripTitleText = e.NewTextValue;

        tripTitleText = Regex.Replace(tripTitleText, @"[^a-zA-Z0-9\s]", "");

        entry.Text = tripTitleText;
    }
}