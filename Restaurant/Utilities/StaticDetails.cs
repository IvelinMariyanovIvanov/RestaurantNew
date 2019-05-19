using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.Utilities
{
    public class StaticDetails
    {
        public const string DefaultFoodImageName = "default_food.png";

        public const string AdminEnduser = "Admin";

        public const string CustomerEnduser = "Customer";

        public const string StatusSubmitted = "Submitted";
        public const string StatusInProgress = "Being prepared";
        public const string StatusReady = "Ready for pick up";
        public const string StatusCompletted = "Completted";
        public const string StatusCancelled = "Cancelled";
    }
}
