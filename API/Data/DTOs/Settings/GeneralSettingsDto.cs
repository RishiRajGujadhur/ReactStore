namespace API.DTOs;

public class GeneralSettingsDto
{
    
    public IFormFile File { get; set; }
    public string LogoURL { get; set; }  
    public string PublicId { get; set; }  
    public int Id { get; set; }
    public string AppName { get; set; }  
    public string CompanyName { get; set; }  
    public string DefaultCurrency { get; set; }  
    public string DefaultLanguage { get; set; }  

}