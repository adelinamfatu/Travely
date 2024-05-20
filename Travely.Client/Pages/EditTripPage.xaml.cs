using System.ComponentModel;
using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class EditTripPage : ContentPage
{
    public EditTripViewModel? viewModel { get; set; }

    private bool isExpanded;

    public bool IsExpanded
    {
        get { return isExpanded; }
        set
        {
            if (isExpanded != value)
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    public EditTripPage(Guid tripId)
    {
        InitializeComponent();
        InitializeViewModel(tripId);
    }

    private void InitializeViewModel(Guid tripId)
    {
        var tripService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripService>();
        if (tripService is not null)
        {
            viewModel = new EditTripViewModel(tripId, tripService);
            BindingContext = viewModel;
        }
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool isCollapsed = false;

    private void HandleChevronPlacesDownClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            placesFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            placesFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private void HandleChevronNotesClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            notesFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            notesFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private void HandleChevronRestaurantsClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            restaurantsFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            restaurantsFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private void HandleChevronArrivalClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            arrivalFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            arrivalFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private async void NavigateToItineraryPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ItineraryPage());
    }
}