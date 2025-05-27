namespace Roadwise
{
    public partial class AboutPage : ContentPage
    {

        // this takes you to the MAUI.NET page.
        private async void LearnMore_Clicked(object sender, EventArgs e)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync("https://aka.ms/maui");
        }

        public AboutPage()
    	{
    		InitializeComponent();
    	}
    }
}