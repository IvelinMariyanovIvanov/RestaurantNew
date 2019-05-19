using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Domain;

namespace Restaurant.MVC.Controllers.Api
{
    //[Produces("application/json")]
    [Route("api/CartsItemsApi")]
    public class CartsItemsApiController : ControllerBase
    {

        private ApplicationDbContext _db;

        public CartsItemsApiController(ApplicationDbContext db)
        {
            _db = db;
        }



        // GET: api/CartsItemsApi
  
        [HttpGet]
        public IActionResult Minus(int cartItemId)
        {
            ShoppingCartItem shoppingCartItem = _db.ShoppingCartItems.Find(cartItemId);

            if (shoppingCartItem.Count == 1)
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

            //return RedirectToAction(nameof(Index));

            return null;
        }

    }
}
