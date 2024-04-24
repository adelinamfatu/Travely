namespace Travely.Client.Pages;

public partial class EditTripPage : ContentPage
{
	public EditTripPage()
	{
		InitializeComponent();
	}

    private async void NavigateToPreviousPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MyTrips");
    }
}