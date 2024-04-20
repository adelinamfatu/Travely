using Travely.Domain.CRUD;
using Travely.Client.Models;
using Travely.Domain.Entities;

namespace Travely.Client.Pages;

public partial class PlanTrip : ContentPage
{
    private Trips myTripsPage;
    private TripData tripData = new TripData();

    public PlanTrip(Trips myTripsPage)
    {
        InitializeComponent();
        this.myTripsPage = myTripsPage;
    }

    private async void StartPlanningClicked_Button(object sender, EventArgs e)
    {
        TripViewModel newTrip = new TripViewModel
        {
            CountryName = entryCountry.Text,
            StartDate = startDatePicker.Date,
            EndDate = endDatePicker.Date
        };
       
        myTripsPage.AddTrip(newTrip);
        tripData.AddTrip(new TripSqlView
        {
            Country = newTrip.CountryName,
            StartDate = newTrip.StartDate,
            EndDate = newTrip.EndDate,
        });

        await Navigation.PopAsync();
    }
}