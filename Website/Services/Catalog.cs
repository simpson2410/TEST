using GrapeCity.Documents.Drawing;
using GrapeCity.Documents.Imaging;
using GrapeCity.Documents.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using Image = GrapeCity.Documents.Drawing.Image;

namespace Website.Services
{
    public class Catalog
    {
        private const int width = 1200;
        private const int height = 1200;
        private const int heightFB = 628;
        private readonly string FolderStored = "images";
        private readonly string rootPath = @"wwwroot\Templetes\images";
        public string GenerateCertificate(string name, int numOfBadges)
        {
           
            string filename = GetFileNameByBadges(numOfBadges);

            using (var bmp = new GcBitmap(width, height, true))
            using (var g = bmp.CreateGraphics(Color.White))
            {
                using (var img = Image.FromFile(@$"{rootPath}\{filename}"))
                {
                    //Draw the product image on target bitmap
                    g.DrawImage(img, new RectangleF(0, 0, width, height), null, ImageAlign.ScaleImage);
                }

                /* //Render the discount tag
                 var semiTransparentColor = Color.FromArgb(100, Color.Red);                
                 var path = CreatePath(new RectangleF(200, 15, 210, 210), g);
                 g.FillPath(path, semiTransparentColor);
                 g.DrawPath(path, semiTransparentColor);*/

                //Add text to the dicount tag
                // TextFormat class is used throughout all GcImaging text rendering to specify
                // font and other character formatting:
                /* string colorcode = "#ff000000";
                 int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                 Color clr = Color.FromArgb(argb);*/
                var conv = new ColorConverter();
                var color = (Color)conv.ConvertFromString(GetColor(numOfBadges));
                var tf = new TextFormat()
                {
                    Font = GrapeCity.Documents.Text.Font.FromFile(@"wwwroot\Templetes\Fonts\ProximaNovaCond-Medium.ttf"),
                    FontSize = 40,
                    ForeColor = color,
                    FontBold = true
                };
                g.DrawString(
                    name,
                    tf,
                    new RectangleF(600, 280, 500, 130), // the layout rectangle                    
                    TextAlignment.Center,   // Center text align
                    ParagraphAlignment.Center, // Center para align
                    true); // word wrap
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    FolderStored);
                bool folderExists = Directory.Exists(path);
                if (!folderExists)
                    Directory.CreateDirectory(path);
                var extension = Path.GetExtension(filename);
                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(path, uniqueFileName);
                bmp.SaveAsPng(filePath);
                return $"{FolderStored}/{uniqueFileName}";
            }
        }

        public string GenerateFBShareCertificate(string name, int numOfBadges)
        {
    
            string filename = GetFileNameFBShareByBadges(numOfBadges);
            using (var bmp = new GcBitmap(width, heightFB, true))
            using (var g = bmp.CreateGraphics(Color.White))
            {
                using (var img = Image.FromFile(@$"{rootPath}\{filename}"))
                {
                    //Draw the product image on target bitmap
                    g.DrawImage(img, new RectangleF(0, 0, width, heightFB), null, ImageAlign.ScaleImage);
                }

                /* //Render the discount tag
                 var semiTransparentColor = Color.FromArgb(100, Color.Red);                
                 var path = CreatePath(new RectangleF(200, 15, 210, 210), g);
                 g.FillPath(path, semiTransparentColor);
                 g.DrawPath(path, semiTransparentColor);*/

                //Add text to the dicount tag
                // TextFormat class is used throughout all GcImaging text rendering to specify
                // font and other character formatting:
                /* string colorcode = "#ff000000";
                 int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                 Color clr = Color.FromArgb(argb);*/
                var conv = new ColorConverter();
                var color = (Color)conv.ConvertFromString(GetColor(numOfBadges));
                var tf = new TextFormat()
                {
                    Font = GrapeCity.Documents.Text.Font.FromFile(@"wwwroot\Templetes\Fonts\ProximaNovaCond-Medium.ttf"),
                    FontSize = 40,
                    ForeColor = color,
                    FontBold = true
                };
                int length = name.Length;
                g.DrawString(
                    name,
                    tf,
                    new RectangleF(420, 85, 750, 100), // the layout rectangle                    
                    TextAlignment.Center,   // Center text align
                    ParagraphAlignment.Center, // Center para align
                    true); // word wrap
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot",
                    FolderStored);
                bool folderExists = Directory.Exists(path);
                if (!folderExists)
                    Directory.CreateDirectory(path);
                var extension = Path.GetExtension(filename);
                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(path, uniqueFileName);
                bmp.SaveAsPng(filePath);
                return $"{FolderStored}/{uniqueFileName}";
            }
        }

        //Define and return the graphic path for creating discount tag
        public IPath CreatePath(RectangleF rc, GcGraphics g)
        {
            var c = new PointF(310, 123);
            float r1 = 100f;
            float r2 = 65f;
            var degreesToRadians = Math.PI / 180;

            var path = g.CreatePath();
            path.BeginFigure(new PointF(c.X + r1, c.Y));
            for (int i = 0; i < 15; i++)
            {
                var angle = (24 * i + 12) * degreesToRadians;
                path.AddLine(new PointF((float)(r2 * Math.Cos(angle)) + c.X, (float)(r2 * Math.Sin(angle)) + c.Y));
                angle = (24 * i + 24) * degreesToRadians;
                path.AddLine(new PointF((float)(r1 * Math.Cos(angle)) + c.X, (float)(r1 * Math.Sin(angle)) + c.Y));
            }
            path.EndFigure(FigureEnd.Open);

            return path;
        }

        private string GetFileNameByBadges(int numOfBadges)
        {
            if (numOfBadges < 3)
            {
                return "chungnhan1.png";
            }
            else if (numOfBadges == 3)
            {
                return "chungnhan2.png";

            }
            else
            {
                return "chungnhan3.png";
            }
        }

        private string GetFileNameFBShareByBadges(int numOfBadges)
        {
            if (numOfBadges < 3)
            {
                return "ShareFB1.png";
            }
            else if (numOfBadges == 3)
            {
                return "ShareFB2.png";

            }
            else
            {
                return "ShareFB3.png";
            }
        }

        private string GetColor(int numOfBadges)
        {
            if (numOfBadges > 3)
            {
                return "#1dffe7";
            }
            else
            {
                return "#fbfe22";
            }
        }
    }
}
