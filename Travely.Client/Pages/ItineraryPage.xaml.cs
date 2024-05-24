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
        var tripDetailService = Application.Current?.Handler?.MauiContext?.Services.GetService<TripDetailService>();
        if (tripDetailService is not null)
        {
            viewModel = new ItineraryViewModel(tripDetailService);
            
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