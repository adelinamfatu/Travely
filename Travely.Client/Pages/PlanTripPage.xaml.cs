using Travely.Domain.CRUD;
using Travely.Client.Models;
using Travely.Domain.Entities;
using Travely.Domain;

namespace Travely.Client.Pages;

public partial class PlanTripPage : ContentPage
{
    //private Trips myTripsPage;
    private TripData tripData;
    private AppDbContext context;

    public PlanTripPage()
    {
        InitializeComponent();
        //this.myTripsPage = myTripsPage;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        context = this.Handler.MauiContext.Services.GetService<AppDbContext>();
        tripData = new TripData(context);
    }

    private async void StartPlanningClicked_Button(object sender, EventArgs e)
    {
        TripViewModel newTrip = new TripViewModel
        {
            CountryName = entryCountry.Text,
            StartDate = startDatePicker.Date,
            EndDate = endDatePicker.Date
        };
       
        //myTripsPage.AddTrip(newTrip);
        tripData.AddTrip(new TripSqlView
        {
            Country = newTrip.CountryName,
            StartDate = newTrip.StartDate,
            EndDate = newTrip.EndDate,
        });

        //await Navigation.PopAsync();
    }
}