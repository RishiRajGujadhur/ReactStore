namespace Store.Infrastructure.Data.DTOs.Invoice
{
    public class InvoiceSettingsDto
    {
        public string Currency { get; set; } = "MUR";
        public string Locale { get; set; } = "en-US";
        public string TaxNotation { get; set; } = "vat";
        public double? MarginTop { get; set; } = 10;
        public double? MarginRight { get; set; } = 10;
        public double? MarginLeft { get; set; } = 10;
        public double? MarginBottom { get; set; } = 10;
        public string Format { get; set; } = "A4";
        public string Height { get; set; } = "210mm";
        public string Width { get; set; } = "297mm";
        public string Orientation { get; set; } = "portrait";
    }
}