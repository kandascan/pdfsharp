using PdfCreator;

namespace PdfSharpHelloWorld
{
    class Program
    {
        const string fontName = "Tahoma";
        const int fontSize = 20;
        const string pdfPath = @"C:\Users\bboczkowski\Desktop\test.pdf";
        static void Main(string[] args)
        {
            var title = "New PDF document";
            var textBody = "Hello from PDF Creator";
            var pdfDocument = new PdfDocumentCreator(title);
            var page = pdfDocument.CreateEmptyPage();
            pdfDocument.GetGraficObjectToDrawing(page);
            var font = pdfDocument.CreateFont(fontName, fontSize);
            pdfDocument.DrawText(textBody, page, font);
            pdfDocument.SaveDocument(pdfPath);
            pdfDocument.ViewDocument(pdfPath);
        }
    }
}
