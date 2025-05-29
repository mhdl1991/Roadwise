using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using System.Windows.Input;

namespace Roadwise
{
    public class NewDrawable : IDrawable, ICommand
    {
        protected GraphicsView _view = new();

        // clickable points on the image
        private List<(string id, PointF area)> Hotspots;
        public NewDrawable() {

            _view.Drawable = this;

            _view.GestureRecognizers.Add(new TapGestureRecognizer { Command = this });

            // What if I were to create a list of hotspots here
            // Define clickable zones
            Hotspots = [
                ("Point1", new(100, 150)),
                ("Point2", new(300, 200)),
                ("Point3", new(200, 400))
            ];
        }

        event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object cmdObject)
        {
            return true;
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

        public void Execute(object cmdObject)
        {
            // handle the clicked event here
            // display some kind of message?

        }
    }
}
