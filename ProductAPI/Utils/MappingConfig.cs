using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace ProductAPI.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
