using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Roadwise
{
    // this class is used to hold the fake map that the demo will use
    // it can be zoomed in or out and panned
    // it has a number of hotspots that can be clicked
    public class MapThingy : IDrawable
    {
        private readonly Microsoft.Maui.Graphics.IImage img;
        private readonly List<(string id, RectF area)> Hotspots;
        private float scale = 1f;
        private PointF translation = new PointF(0, 0);
        private readonly Action<string> onClick;

        public MapThingy(Action<string> onClickCallback)
        {
            onClick = onClickCallback;
            // Load image from resources
            img = (Microsoft.Maui.Graphics.IImage?)ImageSource.FromFile("map.jpg");

            // Define clickable zones
            Hotspots = new List<(string, RectF)>
            {
                ("Point1", new RectF(100, 150, 50, 50)),
                ("Point2", new RectF(300, 200, 50, 50))
            };
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            canvas.Translate(translation.X, translation.Y);
            canvas.Scale(scale,scale);
            canvas.DrawImage(img, 0, 0, 1000, 1000);

            // Optional: draw hotspots overlay for debugging
            foreach (var (id, area) in Hotspots)
            {
                canvas.FillColor = Colors.Red.WithAlpha(0.3f);
                canvas.FillRectangle(area);
            }

            canvas.RestoreState();
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


    }
}
