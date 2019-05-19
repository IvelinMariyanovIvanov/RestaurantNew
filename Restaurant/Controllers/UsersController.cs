using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Domain;
using Restaurant.Models;
using Restaurant.MVC.Utilities;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    [Authorize(Roles = StaticDetails.AdminEnduser)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult>  Index()
        {
            var users = await _db.ApplicationUsers.ToListAsync();

            var currentLoginuser = await _userManager.GetUserAsync(HttpContext.User);

            users.Remove(currentLoginuser);


            //// faster
            //var currentLoginuserId =  _userManager.GetUserAsync(HttpContext.User).Id;

            //var users = await _db.ApplicationUsers.Where(u => u.Id != currentLoginuserId.ToString()).ToListAsync();


            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {

            var userFromDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userFromDb == null)
                return NotFound();

            return View(userFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult>EditPost(ApplicationUser model)
        {
            if (model.Id == null)
                return NotFound();

            var userFromDb = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == model.Id);

            if (userFromDb == null)
                return NotFound();

            userFromDb.LockoutEnd = model.LockoutEnd;
            userFromDb.LockUpReason = model.LockUpReason;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Lock(string id)
        {

            var userFromDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userFromDb == null)
                return NotFound();

            ;

            UserLockVm userLockVm = new UserLockVm()
            {
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                LockUpEnd = ConvertFromDateTimeOffset(userFromDb.LockoutEnd),
                LockUpReason = userFromDb.LockUpReason,
                PhoneNumber = userFromDb.PhoneNumber
            };

            return View(userLockVm);
        }

        private static DateTime ConvertFromDateTimeOffset(DateTimeOffset? lockUpEnd)
        {


            //DateTimeOffset? offset = lockUpEnd;

            var isLockUp = lockUpEnd.HasValue;

            DateTime dateTime;

            dateTime = lockUpEnd.Value.DateTime;
           

            return dateTime;


            //DateTimeOffset? offset = lockUpEnd;


            //var isLockUp = lockUpEnd.HasValue;

            //DateTime dateTime ;

            //if (isLockUp == true)
            //{
            //    dateTime = lockUpEnd.Value.DateTime;
            //}
            //else
            //{
            //    dateTime = null;
            //}

            //return dateTime;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Lock")]
        //public async Task<IActionResult> LockPost(ApplicationUser model)
        public async Task<IActionResult> LockPost(UserLockVm model)
        {
            if (model.Id == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var userFromDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == model.Id);

            if (userFromDb == null)
                return NotFound();

            userFromDb.LockoutEnd = ConvertFromDateTimeOffset(model.LockUpEnd).AddYears(1);

            //userFromDb.LockoutEnd = DateTime.Now.AddYears(1);

            await _db.SaveChangesAsync();

            //return View(userFromDb);
            return RedirectToAction(nameof(Index));
        }
    }
}