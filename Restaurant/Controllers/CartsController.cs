using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Domain;
using Restaurant.Models;
using Restaurant.MVC.Utilities;
using Restaurant.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        private MakeOrderVm _makeOrderVm;

        public CartsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            _makeOrderVm = new MakeOrderVm();
        }

        public async Task<IActionResult> Index()
        {
            var currentLoginuser = await _userManager.GetUserAsync(HttpContext.User);

            var allShoppingCartitemsForUser = await _db.ShoppingCartItems
                .Include(s => s.MenuItem)
                .Where(s => s.ApplicationUserId == currentLoginuser.Id).ToListAsync();

            if(allShoppingCartitemsForUser != null)
            {
                foreach (ShoppingCartItem shoppingcartitem in allShoppingCartitemsForUser)
                {
                    _makeOrderVm.Order.TotalPrice += shoppingcartitem.MenuItem.Price * shoppingcartitem.Count;

                    //if (shoppingcartitem.MenuItem.Description != null && shoppingcartitem.MenuItem.Description.Length > 30)
                    //{
                    //    shoppingcartitem.MenuItem.Description = shoppingcartitem.MenuItem.Description.Substring(0, 10) + "....";
                    //}
                }

                _makeOrderVm.AllPurchasedOrderItemsInCart = allShoppingCartitemsForUser;
            }

           

            _makeOrderVm.Order.PickUpTime = DateTime.Now;

            _makeOrderVm.ApplicationuserId = currentLoginuser.Id;


            return View(_makeOrderVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<IActionResult>IndexPost(MakeOrderVm makeOrderVm)
        {

            //if (!ModelState.IsValid)
            //    return View(_makeOrderVm);

            Order order = new Order()
            {
                ApplicationUserId = makeOrderVm.ApplicationuserId,
                Orderdate = DateTime.Now,
                PickUpTime = makeOrderVm.Order.PickUpTime,
                CouponCode = makeOrderVm.Order.CouponCode,
                Comments = makeOrderVm.Order.Comments,
                OrderStatus = StaticDetails.StatusSubmitted,
                TotalPrice = makeOrderVm.Order.TotalPrice

            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            List<ShoppingCartItem> allShoppingCartItemsForUser = await GetAllShoppingitems(makeOrderVm);

            List<PurchasedOrderItem> purchasedOrderItems = await MappShopItemToPurchasedItem(order, allShoppingCartItemsForUser);

            try
            {
                await _db.PurchasedOrderItems.AddRangeAsync(purchasedOrderItems);

                _db.ShoppingCartItems.RemoveRange(allShoppingCartItemsForUser);

                await _db.SaveChangesAsync();

                HttpContext.Session.SetInt32("CartCount", 0);

                //return RedirectToAction("Index", "Home");

                return RedirectToAction("Confirm", "Orders", new { orderId = order.Id });
            }
            catch (Exception ex)
            {
                return View(_makeOrderVm);

                throw;
            }
        }

        private async Task<List<PurchasedOrderItem>> MappShopItemToPurchasedItem(Order order, List<ShoppingCartItem> allShoppingCartItemsForUser)
        {
            List<PurchasedOrderItem> purchasedOrderItems = new List<PurchasedOrderItem>();

            foreach (ShoppingCartItem itemInCart in allShoppingCartItemsForUser)
            {
                PurchasedOrderItem purchasedOrderItem = new PurchasedOrderItem()
                {
                    OrderId = order.Id,
                    MenuItemId = itemInCart.MenuItemId,
                    Count = itemInCart.Count,
                    Name = itemInCart.MenuItem.Name,
                    Price = itemInCart.MenuItem.Price,
                    Description = itemInCart.MenuItem.Description

                };

                purchasedOrderItems.Add(purchasedOrderItem);
            }

            return purchasedOrderItems;
        }

        private async Task<List<ShoppingCartItem>> GetAllShoppingitems(MakeOrderVm makeOrderVm)
        {
            // get all shoppingcartitems for user
            return await _db.ShoppingCartItems
                .Include(s => s.MenuItem)
                .Where(s => s.ApplicationUserId == makeOrderVm.ApplicationuserId).ToListAsync();
        }

        public IActionResult Plus(int cartItemId)
        {
            ShoppingCartItem shoppingItem = _db.ShoppingCartItems.Find(cartItemId);

            shoppingItem.Count += 1;

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartItemId)
        {
            ShoppingCartItem shoppingCartItem = _db.ShoppingCartItems.Find(cartItemId);

            if(shoppingCartItem.Count == 1)
            {

                _db.ShoppingCartItems.Remove(shoppingCartItem);

                _db.SaveChanges();

                int allItemsInShoppingCart = _db.ShoppingCartItems.Where(s => s.ApplicationUserId == shoppingCartItem.ApplicationUserId).Count();


                HttpContext.Session.SetInt32("CartCount", allItemsInShoppingCart);
            }
            else
            {
                shoppingCartItem.Count--;

                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}