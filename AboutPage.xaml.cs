namespace Roadwise;

public partial class NewPage1 : ContentPage
{
    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }

    public NewPage1()
	{
		InitializeComponent();
	}
}