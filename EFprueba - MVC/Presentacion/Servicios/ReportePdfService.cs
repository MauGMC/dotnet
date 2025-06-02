using DinkToPdf;
using DinkToPdf.Contracts;

namespace Presentacion.Servicios
{
    public class ReportePdfService
    {
        private readonly IConverter _converter;
        public ReportePdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GenerarReporte(string html)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings
                    {
                        Top = 20,
                        Bottom = 20,
                        Left = 20,
                        Right = 20
                    }
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent= html,
                        WebSettings = {DefaultEncoding = "utf-8" }
                    }
                }
            };
            return _converter.Convert(doc);
        }
    }
}
