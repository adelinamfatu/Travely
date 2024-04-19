using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class PlanTrip : ContentPage
{
    private MyTrips myTripsPage;

    public PlanTrip(MyTrips myTripsPage)
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

        await Navigation.PopAsync();
    }
}