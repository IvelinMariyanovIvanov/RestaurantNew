using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class MakeOrderVm
    {
        //public string StatusMessage { get; set; }

        public string ApplicationuserId { get; set; }

        public Order Order { get; set; }

        public List<ShoppingCartItem> AllPurchasedOrderItemsInCart { get; set; }

        public MakeOrderVm()
        {
            this.Order = new Order();

            this.AllPurchasedOrderItemsInCart = new List<ShoppingCartItem>();
        }
    }
}
