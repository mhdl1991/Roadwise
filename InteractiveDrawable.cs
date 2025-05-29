using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;

namespace Roadwise
{
    // this class is used to hold the fake map that the demo will use
    // it can be zoomed in or out and panned
    // it has a number of hotspots that can be clicked
    public class InteractiveDrawable : IDrawable
    {
        private IImage image;
        private readonly List<(string id, RectF area)> Hotspots;
        private float scale = 1f;
        private PointF translation = new(0f, 0f);
        private readonly Action<string> onClick;

        public InteractiveDrawable(Action<string> onClickCallback)
        {
            onClick = onClickCallback;
            // Load image from resources

            var get_img = ImageSource.FromFile("Resources/Images/map.jpg") as IImage;
            if (get_img is IImage) {
                image = get_img;
            }

            // Define clickable zones
            Hotspots = [
                ("Point1", new(100, 150, 50, 50)), 
                ("Point2", new(300, 200, 50, 50))
            ];
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var get_img = ImageSource.FromFile("Resources/Images/map.jpg") as IImage;
            if (get_img is IImage)
            {
                image = get_img;
            }

            canvas.SaveState();
            canvas.Translate(translation.X, translation.Y);
            canvas.Scale(scale,scale);

            // Somehow adding this check made the app stop crashing
            // I think this means that the app is somehow failing to load the image
            // So that's some progress right there
            if (image != null)
            {
                canvas.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            // Optional: draw hotspots overlay for debugging
            foreach ((string id, RectF area) in Hotspots)
            {
                canvas.FillColor = Colors.Red.WithAlpha(0.3f);
                canvas.FillRectangle(area);
            }
            canvas.RestoreState();
            return;
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                translation.X += (float)e.TotalX;
                translation.Y += (float)e.TotalY;
                ((GraphicsView)sender).Invalidate();
            }
        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Running)
            {
                scale *= (float)e.Scale;
                ((GraphicsView)sender).Invalidate();
            }
        }
        public void OnTap(PointF tap)
        {
            // Adjust for pan/zoom
            var x = (tap.X - translation.X) / scale;
            var y = (tap.Y - translation.Y) / scale;

            foreach ((string id, RectF area) in Hotspots)
            {
                if (area.Contains(x, y))
                {
                    onClick(id);
                    break;
                }
            }
        }

    }
}
