using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.Models
{
    public class CouponDto
    {
        public decimal DiscountOrdertotal { get; set; }

        public string StatusMessage { get; set; }
    }
}
