namespace Travely.Client.Pages;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void StartPlanning(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlanTripPage());
    }
}