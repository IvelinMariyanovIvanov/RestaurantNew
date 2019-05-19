using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.MVC.ViewModels
{
    public class UserLockVm
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string LockUpReason { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LockUpEnd { get; set; }

        public string PhoneNumber { get; set; }
    }
}
