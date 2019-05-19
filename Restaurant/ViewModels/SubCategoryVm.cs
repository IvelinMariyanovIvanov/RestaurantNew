using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class SubCategoryVm
    {
        //public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }


        public List<SelectListItem> CategorySelectList { get; set; }

        [Required(ErrorMessage = "Select a Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<string> SubCategoryNamesList { get; set; }

        [Display(Name = "New SubCategory")]
        public bool IsNew { get; set; }

        public string StatusMessage { get; set; }
    }
}
