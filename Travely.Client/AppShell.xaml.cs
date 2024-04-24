namespace Travely.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OpenEditTripPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("EditTripPage");
        }

    }


}
