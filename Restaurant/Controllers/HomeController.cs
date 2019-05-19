using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Restaurant.Data;
using Restaurant.Domain;
using Restaurant.Models;
using Restaurant.MVC.Utilities;
using Restaurant.MVC.ViewModels;

    public class HomeController : Controller
    {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;



    public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    //public async Task<IActionResult> Index(string category, string subCategory)
    //public async Task<IActionResult> Index(HomeVm viewModel)
    //{
    //    HomeVm homeVm = new HomeVm();

    //    string category = null;
    //    string subCategory = null;

    //    if(viewModel != null)
    //    {
    //        category = viewModel.Category;
    //        subCategory = viewModel.SubCategory;
    //    }

    //    if(category == null && subCategory == null)
    //    {
    //        homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory).ToListAsync();

    //        homeVm.CategoryDropDownList = await _db.Categories
    //            .Select(c => new SelectListItem { Text =c.Name, Value=c.Id.ToString() }).ToListAsync();


    //        homeVm.SubCategoryDropDownList = await _db.SubCategories
    //            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToListAsync();

    //        //homeVm.Categories = await _db.Categories.ToListAsync();
    //        //homeVm.Coupons = await _db.Coupons.Where(c => c.IsActive == true).ToListAsync();
    //    }
    //    else if(category != null)
    //    {
    //        homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
    //            .Where(m => m.Category.Name == category).ToListAsync();

    //        //homeVm.Categories = await _db.Categories.ToListAsync();
    //        //homeVm.Coupons = await _db.Coupons.Where(c => c.IsActive == true).ToListAsync();
    //    }
    //    else if(subCategory != null)
    //    {
    //        homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
    //          .Where(m => m.SubCategory.Name == subCategory).ToListAsync();

    //        //homeVm.Categories = await _db.Categories.ToListAsync();
    //        //homeVm.Coupons = await _db.Coupons.Where(c => c.IsActive == true).ToListAsync();
    //    }
    //    else if(category != null && subCategory != null)
    //    {
    //        homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
    //          .Where(m => m.Category.Name == category).ToListAsync();

    //        //homeVm.Categories = await _db.Categories.ToListAsync();
    //        //homeVm.Coupons = await _db.Coupons.Where(c => c.IsActive == true).ToListAsync();
    //    }

    //    return View(homeVm);
    //}

    public async Task<IActionResult> Index(HomeVm viewModel)
    {
        HomeVm homeVm = new HomeVm();



        int categoryId = viewModel.CategoryId;
        int subCategoryId = viewModel.SubCategoryId;

        // ако има филтър i по категория и по суб категория
        if (categoryId != 0 && subCategoryId != 0)
        {
            homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
           .Where(m => m.CategoryId == categoryId && m.SubCategoryId == subCategoryId).ToListAsync();
        }
        // само по категоия
        else if (categoryId != 0)
        {
            homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
           .Where(m => m.CategoryId == categoryId).ToListAsync();
        }
        else if (subCategoryId != 0)
        {
            // търси по име понеже може да има еднакви суб категории в различни Категории
            string subCategoryName = _db.SubCategories.SingleOrDefault(s => s.Id == subCategoryId).Name;

            homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
                .Where(m => m.SubCategory.Name == subCategoryName).ToListAsync();

            // ako суб категориите са уникални
            //.Where(m => m.SubCategoryId == subCategoryId).ToListAsync();

        }
        // иначе вземам всички
        else
        {
            homeVm.MenuItems = await _db.MenuItems.Include(c => c.Category).Include(s => s.SubCategory)
           .ToListAsync();
        }

        homeVm.CategoryDropDownList = await _db.Categories
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToListAsync();

        homeVm.CategoryDropDownList.Insert(0, new SelectListItem { Text = "Select a Category", Value = string.Empty });


        homeVm.SubCategoryDropDownList = await _db.SubCategories
            .GroupBy(c => c.Name).Select(c => c.FirstOrDefault())
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToListAsync();

        homeVm.SubCategoryDropDownList.Insert(0, new SelectListItem { Text = "Select a Sub Category", Value = string.Empty });


        //foreach (var item in homeVm.MenuItems)
        //{
        //    if(item.Description != null && item.Description.Length > 30)
        //    {
        //        item.Description = item.Description.Substring(0, 15) + " ... ";
        //    }
        //}

        return View(homeVm);
    }

    [Authorize]
    public IActionResult Add(int id)
    {
        var menuItemFromDb = _db.MenuItems.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == id);

        ShoppingCartItem purchasedProduct = new ShoppingCartItem()
        {
            MenuItem = menuItemFromDb,
            MenuItemId = menuItemFromDb.Id
        };

        return View(purchasedProduct);
    }


    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ShoppingCartItem model)
    {
        model.Id = 0;

        var loggedUser = await _userManager.GetUserAsync(HttpContext.User);

        if (!ModelState.IsValid)
        {
            // model.id - null ref because is set to 0 -163 line
            var menuItemFromDb = _db.MenuItems.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == model.MenuItemId);

            ShoppingCartItem purchasedProduct = new ShoppingCartItem()
            {
                MenuItem = menuItemFromDb,
                MenuItemId = menuItemFromDb.Id
            };

            return View(purchasedProduct);
        }

        var itemFromDb = await _db.ShoppingCartItems
            .Where(p => p.ApplicationUserId == loggedUser.Id && p.MenuItemId == model.MenuItemId).SingleOrDefaultAsync();

        model.ApplicationUserId = loggedUser.Id;

        if (itemFromDb == null)
        {
            // new purchased item
            _db.ShoppingCartItems.Add(model);

            _db.SaveChanges();
        }
        else
        {
            itemFromDb.Count += model.Count;

            _db.SaveChanges();

        }

        var count = _db.ShoppingCartItems.Where(p => p.ApplicationUserId == loggedUser.Id).Count();

        HttpContext.Session.SetInt32("CartCount", count);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


