using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "10OFF",
                    DiscountAmount = 10.0,
                    MinAmount = 50
                },
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "20OFF",
                    DiscountAmount = 20.0,
                    MinAmount = 100
                }
            );
        }
    }
}
