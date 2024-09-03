﻿using Micro.Center.Utils.Enums;
using Micro.Center.Web.Models.Order;

namespace Micro.Center.Web.Models.Customer
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }

        public List<OrderViewModel> Orders { get; set; }

    }
}
