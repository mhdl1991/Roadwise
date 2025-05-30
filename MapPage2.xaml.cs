namespace Roadwise
{
    public partial class MapPage2 : ContentPage
    {
        public MapPage2()
        {
            InitializeComponent();
        }
        private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e)
        {
            // Okay, now what?
            // Position relative to an Image
            //Point? relativeToImagePosition = e.GetPosition(image);

            // or maybe just pretend to react to the "buttons" being tapped for now?

            // Position inside window
            Point? windowPosition = e.GetPosition(null);



        }

    }
}