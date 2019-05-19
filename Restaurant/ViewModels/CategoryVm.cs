using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class CategoryVm
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a category name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Enter a category order")]
        [Display(Name = "Category Order")]
        public int CategoryOrder { get; set; }

        public string StatusMessage { get; set; }
    }
}
