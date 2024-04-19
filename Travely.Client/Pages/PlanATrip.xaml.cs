using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class PlanATrip : ContentPage
{
    private MyTrips myTripsPage;

    public PlanATrip(MyTrips myTripsPage)
    {
        InitializeComponent();
        this.myTripsPage = myTripsPage;
    }

    private async void StartPlanningClicked_Button(object sender, EventArgs e)
    {
       
        Trips newTrip = new Trips
        {
            CountryName = entryCountry.Text,
            StartDate = startDatePicker.Date,
            EndDate = endDatePicker.Date
        };

       
        myTripsPage.AddTrip(newTrip);

      
        await Navigation.PopAsync();
    }
}