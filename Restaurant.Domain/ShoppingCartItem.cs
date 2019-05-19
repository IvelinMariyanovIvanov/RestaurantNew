using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Restaurant.Models;

namespace Restaurant.Domain
{
   public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greter than 0")]
        public int Count { get; set; }

        [NotMapped]
        public string StatusMessage { get; set; }
    }
}
