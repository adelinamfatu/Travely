using Microsoft.Maui.Controls;

namespace Travely.Client.Pages;

public partial class MyTrips : ContentPage
{
	public MyTrips()
	{
		InitializeComponent();
	}

    private async void StartPlanningClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new PlanATrip()); 
    }
}