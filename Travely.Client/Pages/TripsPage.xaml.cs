using Microsoft.Maui.Controls;
using Travely.Client.Models;
using Travely.Domain.CRUD;
using Travely.Domain;
using Travely.Domain.Entities;
using Travely.Client.Utilities;

namespace Travely.Client.Pages;

public partial class TripsPage : ContentPage
{
    private TripData tripData;

    public TripsPage()
	{
		InitializeComponent();
        var context = Application.Current.Handler.MauiContext.Services.GetService<AppDbContext>();
        this.tripData = new TripData(context);
        tripsCollection.ItemsSource = GetTrips();
	}

    public void AddTrip(TripViewModel trip)
    {
        var trips = (List<TripViewModel>)tripsCollection.ItemsSource;
        trips.Add(trip);
        tripsCollection.ItemsSource = null;
        tripsCollection.ItemsSource = trips;
    }

    private List<TripViewModel> GetTrips()
    {
        var trips = tripData.GetTrips();
        var tripViewModels = new List<TripViewModel>();

        foreach (var tripSqlView in trips)
        {
            var tripViewModel = new TripViewModel
            {
                CountryName = tripSqlView.Country,
                StartDate = tripSqlView.StartDate,
                EndDate = tripSqlView.EndDate,
            };

            tripViewModels.Add(tripViewModel);
        }

        return tripViewModels;
    }

    private async void StartPlanningClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlanTripPage());
    }
}