using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext appDbContext,IMapper mapper) 
        {
           _context = appDbContext;
           _mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponseDto> GetCoupons()
        {
            IEnumerable<Coupon> coupons = await _context.Coupons.ToListAsync();
            // Map the list of coupons to a list of CouponDto
            IEnumerable<CouponDto> couponDtos = _mapper.Map<IEnumerable<CouponDto>>(coupons);

            ResponseDto response = new ResponseDto
            {
                Result = couponDtos,
                IsSuccess = true,
                Message = "Coupons retrieved successfully."
            };
            if (coupons == null || !coupons.Any())
            {
                response.IsSuccess = false;
                response.Message = "No coupons found.";
                return response;
            }
            return response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> GetCouponByCode(int id)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
            // Map the coupon to a CouponDto
            CouponDto couponDto = _mapper.Map<CouponDto>(coupon);
            ResponseDto response = new ResponseDto();
            if (coupon == null)
            {
                response.IsSuccess = false;
                response.Message = "Coupon not found.";
            }
            else
            {
                response.Result = couponDto;
                response.IsSuccess = true;
                response.Message = "Coupon retrieved successfully.";
            }
            return response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<ResponseDto> GetCouponByCode(string code)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
            ResponseDto response = new ResponseDto();
            // Map the coupon to a CouponDto
            CouponDto couponDto = _mapper.Map<CouponDto>(coupon);
            if (coupon == null)
            {
                response.IsSuccess = false;
                response.Message = "Coupon not found.";
            }
            else
            {
                response.Result = couponDto;
                response.IsSuccess = true;
                response.Message = "Coupon retrieved successfully.";
            }
            return response;
        }
        [HttpPost]
        public async Task<ResponseDto> CreateCoupon([FromBody] CouponDto couponDto)
        {
            ResponseDto response = new ResponseDto();
            if (couponDto == null || string.IsNullOrEmpty(couponDto.CouponCode) || couponDto.DiscountAmount <= 0)
            {
                response.IsSuccess = false;
                response.Message = "Invalid coupon data.";
                return response;
            }
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            
            response.Result = couponDto;
            response.IsSuccess = true;
            response.Message = "Coupon created successfully.";
            return response;
        }
        [HttpPut]
        public async Task<ResponseDto> UpdateCoupon([FromBody] CouponDto couponDto)
        {
            ResponseDto response = new ResponseDto();
            Coupon? existingCoupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == couponDto.CouponId);

            if (existingCoupon == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid coupon data.";
                return response;
            }
            // Map the changes onto the tracked entity
            _mapper.Map(couponDto, existingCoupon);
            await _context.SaveChangesAsync();
            
            response.Result = existingCoupon;
            response.IsSuccess = true;
            response.Message = "Coupon updated successfully.";
            return response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseDto> DeleteCoupon(int id)
        {
            ResponseDto response = new ResponseDto();
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
            if (coupon == null)
            {
                response.IsSuccess = false;
                response.Message = "Coupon not found.";
                return response;
            }
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            response.IsSuccess = true;
            response.Message = "Coupon deleted successfully.";
            return response;
        }

    }
}
