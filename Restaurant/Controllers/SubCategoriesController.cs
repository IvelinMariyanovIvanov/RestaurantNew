using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Domain;
using Restaurant.MVC.Utilities;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    [Authorize(Roles = StaticDetails.AdminEnduser)]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        private string StatusMessage { get; set; }

        public SubCategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var subCategoriesList = await _db.SubCategories.Include(s => s.Category).ToListAsync();

            return View(subCategoriesList);
        }

        private async Task<SubCategoryVm> GenerateSubCategoryViewModel()
        {
            var categories = await _db.Categories.ToListAsync();
            var subCategories = await _db.SubCategories.ToListAsync();

            SubCategoryVm subCategoryVm = new SubCategoryVm()
            {
                SubCategory = new SubCategory(),
                CategorySelectList = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList(),

                SubCategoryNamesList = subCategories.Select(s => s.Name).Distinct().ToList(),

            };

            subCategoryVm.CategorySelectList.Insert(0, new SelectListItem { Text = "Select a Category", Value = string.Empty });

            return subCategoryVm;
        }

        [HttpGet]
        public JsonResult GetSearchValue(string search, int categoryId)
        {

            var allsearch = _db.SubCategories
                  .Where(x => x.CategoryId == categoryId && x.Name.Contains(search))
                  .Select(x => new
                  {
                      label = x.Name,
                      value = x.Id
                  })
                  .ToArray();


            return Json(allsearch);


        }

        public JsonResult GetAllSubcategories(string search)
        {
            var allsearch = _db.SubCategories
                 .Where(x =>x.Name.Contains(search))
                 .Select(x => new
                 {
                     label = x.Name,
                     value = x.Id
                 })
                 .ToArray();


            return Json(allsearch);
        }


        public async Task<IActionResult>Create()
        {
            SubCategoryVm subCategoryVm = await GenerateSubCategoryViewModel();

            return View(subCategoryVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(SubCategoryVm model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = await _db.SubCategories.Where(s => s.Name == model.SubCategory.Name).CountAsync();

                var doesPairSubcategoryAndcategoryExists = await _db.SubCategories.Where
                                                           (s => s.Name == model.SubCategory.Name &&
                                                            s.CategoryId == model.SubCategory.CategoryId).CountAsync();

                //if (doesSubCategoryExists > 0 && model.IsNew)
                //{

                //    StatusMessage = "Error : SubCategory alredy exists";
                //}
                //if (!model.IsNew && doesSubCategoryExists == 0)
                //{
                //    StatusMessage = "Error : SubCategory does not exist";
                //}
                if (!model.IsNew && doesPairSubcategoryAndcategoryExists == 0)
                {
                    StatusMessage = "Error : SubCategory does not exist";
                }
                else if (doesPairSubcategoryAndcategoryExists > 0)
                {
                    StatusMessage = "Error: Category and Subcategory already exist";
                }
                else
                {
                    _db.Add(model.SubCategory);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }

            SubCategoryVm subCategoryVm = await GenerateSubCategoryViewModel();

            // update status message
            subCategoryVm.StatusMessage = this.StatusMessage;

            return View(subCategoryVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var subCatFromDb = await _db.SubCategories.Include(s => s.Category).SingleOrDefaultAsync(s => s.Id == id);
            if(subCatFromDb == null)
            {
                return NotFound();
            }

            SubCategoryVm subCategoryVm = await GenerateSubCategoryViewModel();

            subCategoryVm.SubCategory = subCatFromDb;

            // set the category in dropdown
            subCategoryVm.CategoryId = subCatFromDb.CategoryId;

            //subCategoryVm.SubCategory.Name = subCatFromDb.Name;
            //subCategoryVm.SubCategory.CategoryId = subCatFromDb.CategoryId;
           

            return View(subCategoryVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, SubCategoryVm subCategoryVm)
        {
            if(ModelState.IsValid)
            {
                var subCategoryExist = _db.SubCategories.Where(s => s.Name == subCategoryVm.SubCategory.Name).Count();

                // заради SubCategoryVm където добавих CategoryId
                //var doesPairSubCategoryAndCategoryExist = _db.SubCategories
                //    .Where(s => s.CategoryId == subCategoryVm.SubCategory.CategoryId && s.Name == subCategoryVm.SubCategory.Name).Count();

                var doesPairSubCategoryAndCategoryExist = _db.SubCategories
                 .Where(s => s.CategoryId == subCategoryVm.CategoryId && s.Name == subCategoryVm.SubCategory.Name).Count();

                string sub = subCategoryVm.SubCategory.Name;
                string cat = subCategoryVm.SubCategory.Category.Name;

                if (subCategoryExist ==  0)
                {
                    this.StatusMessage = $"Error : {sub} is new. You can not add a new SubCategory here";
                }
                else if (doesPairSubCategoryAndCategoryExist > 0)
                {
                    this.StatusMessage = $"Error : subcategory {sub} and category {cat} aleredy exist";
                }
                else
                {
                    var subCategoryFromDb = _db.SubCategories.Find(id);

                    subCategoryFromDb.Name = subCategoryVm.SubCategory.Name;
                    subCategoryFromDb.CategoryId = subCategoryVm.CategoryId;
                    //subCategoryFromDb.CategoryId = subCategoryVm.SubCategory.CategoryId;


                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }


            SubCategoryVm subCategory = await GenerateSubCategoryViewModel();

            // update status message
            subCategory.StatusMessage = this.StatusMessage;

            return View(subCategory);

        }

        public async Task<IActionResult>Details(int? id)
        {
            if (id == null)
                return NotFound();

            var subCategoryFromDb = await _db.SubCategories.Include(c =>c.Category).SingleAsync(s => s.Id == id);

            if (subCategoryFromDb == null)
                return NotFound();


            return View(subCategoryFromDb);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var subCategoryFromDb = await _db.SubCategories.Include(c => c.Category).SingleAsync(s => s.Id == id);

            if (subCategoryFromDb == null)
                return NotFound();


            return View(subCategoryFromDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmedDelete(int? id)
        //public async Task<IActionResult> ConfirmedDelete(SubCategory subCategory)
        {
            var subCategoryFromDb = await _db.SubCategories.FindAsync(id);

            _db.Remove(subCategoryFromDb);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}