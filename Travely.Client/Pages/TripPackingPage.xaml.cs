namespace Travely.Client.Pages;

using System.Collections.ObjectModel;
using Travely.Client.Models;

public partial class TripPackingPage : ContentPage
{
    public TripPackingPage()
    {
        InitializeComponent();
        BindingContext = new PackingListViewModel();
    }
}