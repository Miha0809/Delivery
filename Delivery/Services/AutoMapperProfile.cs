using AutoMapper;
using Delivery.Models;
using Delivery.Models.DTOs;

namespace Delivery.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
    }
}