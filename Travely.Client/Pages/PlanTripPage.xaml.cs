using Travely.Domain.CRUD;
using Travely.Client.Models;
using Travely.Domain.Entities;
using Travely.Domain;

namespace Travely.Client.Pages;

public partial class PlanTripPage : ContentPage
{
    private TripData tripData;
    private AppDbContext context;

    public PlanTripPage()
    {
        InitializeComponent();
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

        tripData.AddTrip(new TripSqlView
        {
            Title = "Aici",
            Country = newTrip.CountryName,
            StartDate = newTrip.StartDate,
            EndDate = newTrip.EndDate,
        });

        await Navigation.PopAsync();
    }
}