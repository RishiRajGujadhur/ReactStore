using API.DTOs;
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
        CreateMap<Receipt, ReceiptDto>();  
    }
}