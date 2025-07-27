using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ResponseDto> CreateCoupon([FromBody] Coupon coupon)
        {
            ResponseDto response = new ResponseDto();
            if (coupon == null || string.IsNullOrEmpty(coupon.CouponCode) || coupon.DiscountAmount <= 0)
            {
                response.IsSuccess = false;
                response.Message = "Invalid coupon data.";
                return response;
            }
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            // Map the created coupon to a CouponDto
            CouponDto couponDto = _mapper.Map<CouponDto>(coupon);
            response.Result = couponDto;
            response.IsSuccess = true;
            response.Message = "Coupon created successfully.";
            return response;
        }
        [HttpPut]
        public async Task<ResponseDto> UpdateCoupon([FromBody] Coupon updatedCoupon)
        {
            ResponseDto response = new ResponseDto();
            Coupon? existingCoupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponId == updatedCoupon.CouponId);

            if (existingCoupon == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid coupon data.";
                return response;
            }

            _context.Coupons.Update(updatedCoupon);
            await _context.SaveChangesAsync();
            response.Result = updatedCoupon;
            response.IsSuccess = true;
            response.Message = "Coupon updated successfully.";
            return response;
        }

    }
}
