using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Domain
{
    public class PurchasedOrderItem
    {
        [Required]
        public int Id { get; set; }

        public Order Order { get; set; }

        [Required]
        public int OrderId { get; set; }

        public MenuItem MenuItem { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }



        //public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
