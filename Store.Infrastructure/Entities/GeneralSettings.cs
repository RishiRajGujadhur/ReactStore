using System.ComponentModel.DataAnnotations;

namespace Store.Infrastructure.Entities
{
    public class GeneralSettings : StoreEntry
    {
        [Key]
        public int Id { get; set; }
        public string LogoURL { get; set; } = "default.png";
        public string AppName { get; set; } = "My App";
        public string CompanyName { get; set; } = "My Company";
        public string DefaultCurrency { get; set; } = "USD";
        public string DefaultLanguage { get; set; } = "en";
        public string PublicId { get; internal set; }
    }
}