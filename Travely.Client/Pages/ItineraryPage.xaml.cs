using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class ItineraryPage : ContentPage
{
    public ItineraryViewModel? viewModel { get; set; }

    public ItineraryPage()
	{
		InitializeComponent();
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        var tripService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripService>();
        if (tripService != null)
        {
            viewModel = new ItineraryViewModel(tripService);
            
            viewModel.AddDay("Day 1");
            viewModel.AddPlace("Day 1", "Place 1");
            viewModel.AddPlace("Day 1", "Place 2");

            viewModel.AddDay("Day 2");
            viewModel.AddPlace("Day 2", "Place 3");
            viewModel.AddPlace("Day 2", "Place 4");
            
            BindingContext = viewModel;
        }
    }
}