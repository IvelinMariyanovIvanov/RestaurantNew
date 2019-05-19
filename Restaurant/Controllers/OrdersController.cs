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

namespace Restaurant.MVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Confirm(int orderId)
        {
            ApplicationUser currentLoginuser = await _userManager.GetUserAsync(HttpContext.User);

            Order order = await _db.Orders
                .Include(o => o.PurchasedOrderItems)
                //.Include(o => o.MenuItems)
                .Where(o => o.ApplicationUserId == currentLoginuser.Id && o.Id == orderId).SingleOrDefaultAsync();

            return View(order);
        }

        public async Task<IActionResult> OrderHistory()
        {
            ApplicationUser currentLoginuser = await _userManager.GetUserAsync(HttpContext.User);

            List<Order> orders = await _db.Orders
                .Include(o => o.PurchasedOrderItems)
                .Where(o => o.ApplicationUserId == currentLoginuser.Id)
                .OrderByDescending(o => o.Orderdate)
                .ToListAsync();

            return View(orders);
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        public async Task<IActionResult> PickUpDetails(int orderId)
        {
            Order order = await _db.Orders.Include(d => d.PurchasedOrderItems)
                                              .Include(m => m.MenuItems)
                                              .Include(u => u.ApplicationUser)
                                              .SingleOrDefaultAsync(o => o.Id == orderId);

            return View(order);
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        [HttpPost]
        [ActionName("PickUpDetails")]
        public async Task<IActionResult> PickUpDetailsPost(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            order.OrderStatus = StaticDetails.StatusCompletted;

            await _db.SaveChangesAsync();

            return RedirectToAction("OrderPickUp", "Orders"); ;
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        public async Task<IActionResult> OrderPickup(int? searchOrder, string searchEmail, string searchPhone)
        {
            var orders = new List<Order>();


            if (searchOrder != null || searchEmail !=null || searchPhone != null)
            {
                if (searchOrder != null)
                {
                    orders = await _db.Orders.Include(d => d.PurchasedOrderItems)
                                              .Include(m => m.MenuItems)
                                              .Where(o => o.Id == (searchOrder))
                                              .OrderByDescending(o => o.PickUpTime)
                                              .ToListAsync();
                }
                else if (searchEmail != null)
                {
                    orders = await _db.Orders.Include(d => d.PurchasedOrderItems)
                                              .Include(m => m.MenuItems)
                                              .Where(o => o.ApplicationUser.Email == searchEmail)
                                              .OrderByDescending(o => o.PickUpTime)
                                              .ToListAsync();
                }
                else if (searchPhone != null)
                {
                    orders = await _db.Orders.Include(d => d.PurchasedOrderItems)
                                              .Include(m => m.MenuItems)
                                              .Where(o => o.ApplicationUser.PhoneNumber == searchPhone)
                                              .OrderByDescending(o => o.PickUpTime)
                                              .ToListAsync();
                }
                return View(orders);
            }
        

             orders = await _db.Orders
                  .Include(o => o.PurchasedOrderItems)
                  .Where(o => o.OrderStatus == StaticDetails.StatusReady)
                  .OrderByDescending(o => o.PickUpTime)
                  .ToListAsync();

            return View(orders);
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        public async Task<IActionResult> MenageOrder()
        {
            List<Order> orders = await _db.Orders
              .Include(o => o.PurchasedOrderItems)
              .Where(o => o.OrderStatus == StaticDetails.StatusSubmitted || o.OrderStatus == StaticDetails.StatusInProgress)
              .OrderByDescending(o => o.PickUpTime)
              .ToListAsync();

            return View(orders);
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        public async Task<IActionResult> OrderPrepare(int orderId)
        {
            Order order = await _db.Orders.FindAsync(orderId);

            order.OrderStatus = StaticDetails.StatusInProgress;

            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(MenageOrder));
        }

        [Authorize(Roles = StaticDetails.AdminEnduser)]
        public async Task<IActionResult> OrderReady(int orderId)
        {
            Order order = await _db.Orders.FindAsync(orderId);

            order.OrderStatus = StaticDetails.StatusReady;

            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(MenageOrder));
        }


        public async Task<IActionResult> OrderCancell(int orderId)
        {
            Order order = await _db.Orders.FindAsync(orderId);

            order.OrderStatus = StaticDetails.StatusCancelled;

            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(MenageOrder));
        }
    }
}