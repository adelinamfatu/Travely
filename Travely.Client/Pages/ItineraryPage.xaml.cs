namespace Travely.Client.Pages;

public partial class ItineraryPage : ContentPage
{
	public ItineraryPage()
	{
		InitializeComponent();
	}



    private async void NavigateToPreviousPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///EditTripPage");
    }

    private void OnCreateItineraryClicked(object sender, EventArgs e)
    {
        Frame newFrame = new Frame
        {
            Margin = new Thickness(10),
            IsVisible = true,
            Content = new StackLayout
            {
                Children =
            {
                new Label { Text = "Day ", FontSize = 18, FontAttributes = FontAttributes.Bold, Margin = new Thickness(20) },
                new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Children =
                    {
                        new ImageButton { Source = "bx_chevron_down.svg", WidthRequest = 30, HeightRequest = 30 },
                        new Label { Text = "Places", FontSize = 18, FontAttributes = FontAttributes.Bold, Margin = new Thickness(20) }
                    }
                },
                new Frame
                {
                    CornerRadius = 5,
                    Padding = 0,
                    Margin = 5,
                    Content = new Grid
                    {
                        Children =
                        {
                            new Image { Source = "map.png", VerticalOptions = LayoutOptions.Center, WidthRequest = 20, HorizontalOptions = LayoutOptions.Start, Margin = new Thickness(10) },
                            new Entry { Placeholder = "Add Place", Margin = new Thickness(30, 0, 0, 0) }
                        }
                    }
                }
            }
            }
        };

        StackLayout stackLayout = new StackLayout();

        if (Content != null)
        {
            stackLayout = new StackLayout();
        }

        stackLayout.Children.Add(newFrame);

        Content = stackLayout;
    }





}