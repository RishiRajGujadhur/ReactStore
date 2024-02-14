using API.DTOs;
using API.DTOs.Invoice;
using API.Entities;
using AutoMapper;

namespace API.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CommentCreateDto, Comment>();  
        CreateMap<CommentUpdateDto, Comment>();  
        CreateMap<Comment, CommentDto>();  
        CreateMap<Invoice, InvoiceDto>();  
        CreateMap<Invoice, InvoiceDetailsDto>(); 
        CreateMap<InvoiceSettingsDto, InvoiceSettings>(); 
        CreateMap<UpdateInvoiceSenderDto, InvoiceSender>(); 
        CreateMap<Receipt, ReceiptDto>();  
    }
}