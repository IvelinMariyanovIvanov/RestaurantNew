using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Domain
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public DateTime Orderdate { get; set; }

        [Required(ErrorMessage ="Select a Pickup time")]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Time)]
        public DateTime PickUpTime { get; set; }

        public string CouponCode { get; set; }

        public string Comments { get; set; }

        public string OrderStatus { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public ICollection<PurchasedOrderItem> PurchasedOrderItems { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }


    }
}
