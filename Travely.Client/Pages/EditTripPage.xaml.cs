using System.ComponentModel;

namespace Travely.Client.Pages;

public partial class EditTripPage : ContentPage
{
    private bool isExpanded;

    public bool IsExpanded
    {
        get { return isExpanded; }
        set
        {
            if (isExpanded != value)
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;




    public EditTripPage()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool isCollapsed = false;

    private void HandleChevronPlacesDownClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            placesFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            placesFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private void HandleChevronNotesClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            notesFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {

            notesFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    private void HandleChevronRestaurantsClicked(object sender, EventArgs e)
    {
        if (isCollapsed)
        {
            restaurantsFrame.HeightRequest = double.NaN;
            isCollapsed = false;
        }
        else
        {
            restaurantsFrame.HeightRequest = 100;
            isCollapsed = true;
        }
    }

    /*  private void HandleChevronDownClicked(object sender, EventArgs e)
      {
          foreach (var child in Content.FindByName<VerticalStackLayout>("mainLayout").Children)
          {
              if (child is Frame frame)
              {
                  if (isCollapsed)
                  {
                      frame.HeightRequest = double.NaN;
                  }
                  else
                  {
                      frame.HeightRequest = 100;
                  }
              }
          }
          isCollapsed = !isCollapsed;
      }*/

   /* private async void NavigateToPreviousPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }*/

    private async void NavigateToItineraryPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ItineraryPage());
    }
}