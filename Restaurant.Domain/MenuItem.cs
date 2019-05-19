using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Domain
{
   public class MenuItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Spicy SpicyType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 1 ")]
        public decimal Price { get; set; }

        [Required]
        //[Display(Name = "Category")]
        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        //[Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        //[ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
    }
}
