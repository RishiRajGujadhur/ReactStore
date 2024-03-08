  
using AutoMapper;
using Store.Infrastructure.Data.DTOs.Invoice;
using Store.Infrastructure.Data.DTOs.Product;
using Store.Infrastructure.Data.DTOs.Receipt;
using Store.Infrastructure.Data.DTOs.Settings;
using Store.Infrastructure.Entities;

namespace API.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>(); 
        CreateMap<CommentCreateDto, Comment>();  
        CreateMap<CommentUpdateDto, Comment>();  
        CreateMap<Comment, CommentDto>();  
        CreateMap<Invoice, InvoiceDto>();  
        CreateMap<Invoice, InvoiceDetailsDto>(); 
        CreateMap<InvoiceSettingsDto, InvoiceSettings>();   
        CreateMap<GeneralSettingsDto,GeneralSettings>();  
        CreateMap<Receipt, ReceiptDto>();  
    }
}