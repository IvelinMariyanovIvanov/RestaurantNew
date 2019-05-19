

namespace Restaurant.DAL
{
    //using Microsoft.AspNetCore.Mvc.Rendering;
    //using Microsoft.EntityFrameworkCore;
    //using Restaurant.DAL.Interfaces;
    //using Restaurant.Data;
    //using Restaurant.Domain;
    //using Restaurant.MVC.ViewModels;
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;

    //public class SubCategoryRepository : ISubCategoryRepository
    //{
    //    private readonly ApplicationDbContext _db;

    //    public SubCategoryRepository(ApplicationDbContext db)
    //    {
    //        _db = db;
    //    }

    //    public  SubCategoryVm GenerateSubCategoryViewModel()
    //    {
    //        var categories =  _db.Categories.ToList();
    //        var subCategories =  _db.SubCategories.ToList();

    //        SubCategoryVm subCategoryVm = new SubCategoryVm()
    //        {
    //            SubCategory = new SubCategory(),
    //            CategorySelectList = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList(),
    //            SubCategoryNamesList = subCategories.OrderBy(s => s.Name).Select(s => s.Name).Distinct().ToList()
    //        };

    //        subCategoryVm.CategorySelectList.Insert(0, new SelectListItem { Text = "Select a Category", Value = string.Empty });

    //        return subCategoryVm;
    //    }
    //}
}
