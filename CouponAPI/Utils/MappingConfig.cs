using AutoMapper;
using CouponAPI.Models;
using CouponAPI.Models.Dto;

namespace CouponAPI.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDto, Coupon>();
            CreateMap<Coupon, CouponDto>();
        }
    }
}
