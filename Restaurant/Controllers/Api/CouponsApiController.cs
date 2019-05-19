using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Domain;
using Restaurant.MVC.Models;

namespace Restaurant.MVC.Controllers.Api
{
    //[Produces("application/json")]
    //[ApiController]
    [Route("api/CouponsApi")]
    public class CouponsApiController : ControllerBase  // Controller
    {
        private ApplicationDbContext _db;

        public CouponsApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/CouponsApi
        [HttpGet]
        public IActionResult Get(decimal orderTotal, string couponCode)
        {
            var result = string.Empty;

            //CouponDto couponDto = new CouponDto();

            if (couponCode == null)
            {            
                result = orderTotal + ":E"  + ":Enter a valid coupon code";

                return Ok(result);
            }

            var couponFromDb = _db.Coupons.SingleOrDefault(c => c.Name == couponCode);

            if(couponFromDb == null)
            {
                result = orderTotal + ":E" + ":Enter a valid coupon code";

                return Ok(result);

            }

            if(orderTotal < couponFromDb.MinAmount)
            {
                //result = orderTotal + ":E" ;

                result = orderTotal + ":E" + $":Total price {orderTotal} is under {couponFromDb.MinAmount}";

                //result = $"Error: total price {orderTotal} is under {couponFromDb.MinAmount}" + orderTotal;

                //couponDto.DiscountOrdertotal = orderTotal;
                //couponDto.StatusMessage = "Error : total price orderTotal is under coupon min amount";

                //return couponDto;

                return Ok(result);
            }

            if(Convert.ToInt32(couponFromDb.CouponType) == (int)CouponType.Dollar)
            {
                orderTotal = orderTotal - couponFromDb.Discount;

                result = orderTotal + ":S" + $":You have successfully used a {couponFromDb.Discount} dollars discount";

                return Ok(result);
            }
            if(Convert.ToInt32(couponFromDb.CouponType) == (int)CouponType.Percentage)
            {
                orderTotal = orderTotal - (orderTotal * couponFromDb.Discount / 100);

                result = orderTotal + ":S" + $":You have successfully used a {orderTotal * couponFromDb.Discount / 100} dollars discount";

                return Ok(result);
            }
            return Ok(result);
        }

        // GET: api/CouponsApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
