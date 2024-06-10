using Travely.BusinessLogic.Services;
using Travely.Client.Models;

namespace Travely.Client.Pages;

public partial class ProfilePage : ContentPage	
{
    public ProfileViewModel? viewModel { get; set; }

    public ProfilePage()
	{
		InitializeComponent();
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        var statisticService = Application.Current?.Handler?.MauiContext?.Services.GetService<StatisticService>();
        if (statisticService is not null)
        {
            viewModel = new ProfileViewModel(statisticService);
            BindingContext = viewModel;
            viewModel.InitializeCharts();
        }
    }
}