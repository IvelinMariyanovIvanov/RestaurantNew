using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class MenuItemVm
    {
        public MenuItem MenuItem { get; set; }


        [Required(ErrorMessage = "Select a Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem> CategoryCollection { get; set; }

        public List<SelectListItem> SubCategoryCollection { get; set; }

        [Required(ErrorMessage = "Select a Sub Category")]
        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        public string StatusMessage { get; set; }

        public MenuItemVm()
        {
            this.MenuItem = new MenuItem();
            CategoryCollection = new List<SelectListItem>();
            SubCategoryCollection = new List<SelectListItem>();
        }

        //[Required]
        //public int Id { get; set; }

        //[Required]
        //public string Name { get; set; }

        //public string Description { get; set; }

        //public string Image { get; set; }

        //public Spicy SpicyType { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 1 ")]
        //public decimal Price { get; set; }

        //[Display(Name = "Category")]
        //public int CategoryId { get; set; }

        ////[ForeignKey("CategoryId")]
        //public Category Category { get; set; }

        //[Display(Name = "SubCategory")]
        //public int SubCategoryId { get; set; }

        ////[ForeignKey("SubCategoryId")]
        //public SubCategory SubCategory { get; set; }
    }
}
