using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.MVC.Components
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public UserNameViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var loggedUser = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == claim.Value);

            return View(loggedUser);
        }
    }
}
