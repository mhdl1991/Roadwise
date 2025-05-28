using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
namespace Roadwise
{
	public partial class MapPage : ContentPage
	{
        public MapThingy Drawable { get; set; }
        public MapPage()
		{
			InitializeComponent();
            // Be careful of variable names
            Drawable = new MapThingy(OnPointClicked);
            BindingContext = this;

            // Add pan and pinch gestures
            var pan = new PanGestureRecognizer();
            pan.PanUpdated += Drawable.OnPanUpdated;
            InteractiveCanvas.GestureRecognizers.Add(pan);

            var pinch = new PinchGestureRecognizer();
            pinch.PinchUpdated += Drawable.OnPinchUpdated;
            InteractiveCanvas.GestureRecognizers.Add(pinch);

            // Tap for hotspots
            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                if (e is TappedEventArgs args)
                {
                    var loc = args.GetPosition(InteractiveCanvas);
                    Drawable.OnTap(new Microsoft.Maui.Graphics.PointF((float)loc.X, (float)loc.Y));
                }
            };
            InteractiveCanvas.GestureRecognizers.Add(tap);
        }

        void OnPointClicked(string id)
        {
            //ChartPanel.IsVisible = true;
            //ChartLabel.Text = $"Displaying data for: {id}";
            // TODO: load specific chart/table here
        }

    }
}