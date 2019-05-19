using Restaurant.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class ConfirmedOrderVm
    {
        public Order Order { get; set; }

        public List<PurchasedOrderItem>  PurchasedOrderItemsList { get; set; }
    }
}
