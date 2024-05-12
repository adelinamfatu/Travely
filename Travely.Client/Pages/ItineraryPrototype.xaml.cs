using System.Collections.ObjectModel;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class ItineraryPrototype : ContentPage
{
    public ItineraryViewModel? viewModel { get; set; }

    public ItineraryPrototype()
	{
		InitializeComponent();
        viewModel = new ItineraryViewModel();

        viewModel.AddDay("Day 1");
        viewModel.AddPlace("Day 1", "Place 1");
        viewModel.AddPlace("Day 1", "Place 2");

        viewModel.AddDay("Day 2");
        viewModel.AddPlace("Day 2", "Place 3");
        viewModel.AddPlace("Day 2", "Place 4");

        BindingContext = viewModel;
    }
}