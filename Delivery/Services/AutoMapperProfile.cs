using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;

namespace Delivery.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserPrivateInfoDto>();
        CreateMap<User, UserDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Rebate, RebateDto>();
        CreateMap<DatailsProduct, DatailsProductDto>();
        CreateMap<Image, ImageDto>();
        CreateMap<CatalogFirst, CatalogFirstDto>();
        CreateMap<CatalogSecond, CatalogSecondDto>();
        CreateMap<Category, CategoryDto>();
    }
}