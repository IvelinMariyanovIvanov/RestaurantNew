using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Domain
{
    public class SubCategory
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public string Name { get; set; }

        [Required]
        [Display(Name= "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
