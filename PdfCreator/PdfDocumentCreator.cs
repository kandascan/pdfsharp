using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfCreator
{
    public class PdfDocumentCreator
    {
        public PdfDocument document { get; set; }
        public XGraphics gfx { get; set; }
        public XGraphicsState state { get; set; }
        public int borderWidth { get; set; }
        public XColor shadowColor { get; set; }
        public XPen borderPen { get; set; }
        public PdfDocumentCreator(string title)
        {
            document = new PdfDocument();
            document.Info.Title = title;
            borderWidth = 600;
            shadowColor = XColor.Empty;
            borderPen = new XPen(shadowColor);
        }

        public PdfPage CreateEmptyPage()
        {
            return document.AddPage();
        }

        public void GetGraficObjectToDrawing(PdfPage page)
        {
             gfx = XGraphics.FromPdfPage(page);
        }

        public XFont CreateFont(string fontName, int fontSize)
        {
            return new XFont(fontName, fontSize, XFontStyle.BoldItalic);
        }

        public void DrawText(string text, PdfPage page, XFont font)
        {
            gfx.DrawString(text, font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),
            XStringFormats.Center);
        }

        public void SaveDocument(string path)
        {
            document.Save(path);
        }

        public void ViewDocument(string path)
        {
            Process.Start(path);
        }

        public void DrawImage(XGraphics gfx, int number, string jpegSamplePath)
        {
            BeginBox(gfx, number, "DrawImage (original)");

            XImage image = XImage.FromFile(jpegSamplePath);
            
            // Left position in point

            double x = (250 - image.PixelWidth * 72 / image.HorizontalResolution) / 2;

            gfx.DrawImage(image, x, 0);
            
            EndBox(gfx);
        }
        public void BeginBox(XGraphics gfx, int number, string title)
        {
            const int dEllipse = 15;
            XRect rect = new XRect(0, 20, 300, 200);
            if (number % 2 == 0)
                rect.X = 300 - 5;
            rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
            rect.Inflate(-10, -10);
            XRect rect2 = rect;
            rect2.Offset(borderWidth, borderWidth);
            var backColor = shadowColor;
            var backColor2 = shadowColor;

            gfx.DrawRoundedRectangle(new XSolidBrush(shadowColor), rect2, new XSize(dEllipse + 8, dEllipse + 8));
            XLinearGradientBrush brush = new XLinearGradientBrush(rect, backColor, backColor2, XLinearGradientMode.Vertical);
            gfx.DrawRoundedRectangle(borderPen, brush, rect, new XSize(dEllipse, dEllipse));
            rect.Inflate(-5, -5);

            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            gfx.DrawString(title, font, XBrushes.Navy, rect, XStringFormats.TopCenter);

            rect.Inflate(-10, -5);
            rect.Y += 20;
            rect.Height -= 20;

            state = gfx.Save();
            gfx.TranslateTransform(rect.X, rect.Y);
        }

        public void EndBox(XGraphics gfx)
        {
            gfx.Restore(state);
        }
    }
}
