using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }


        public string Category { get; set; }
        public string SubCategory { get; set; }

        public List<SelectListItem> CategoryDropDownList { get; set; }
        public int CategoryId { get; set; }

        public List<SelectListItem> SubCategoryDropDownList { get; set; }
        public int SubCategoryId { get; set; }

        public string StatusMessage { get; set; }
    }
}
