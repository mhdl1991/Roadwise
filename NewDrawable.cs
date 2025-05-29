using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;

namespace Roadwise
{
    public class NewDrawable : IDrawable
    {   
        private List<(string id, PointF area)> Hotspots;
        public NewDrawable() {
            // What if I were to create a list of hotspots here
            // Define clickable zones
            Hotspots = [
                ("Point1", new(100, 150)),
                ("Point2", new(300, 200)),
                ("Point3", new(200, 400))
            ];
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            IImage image;
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("Roadwise.Resources.Images.map.jpg"))
            {
                image = PlatformImage.FromStream(stream);
            }

            if (image != null)
            {
                canvas.DrawImage(image, 10, 10, image.Width, image.Height);
            }

            foreach ((string id, PointF p) in Hotspots)
            {
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 4;
                canvas.FillColor = Colors.Red;
                canvas.FillEllipse(p.X, p.Y, 25, 25);
                canvas.DrawEllipse(p.X, p.Y, 25, 25);
            }

            canvas.RestoreState();
        }
    }
}
