using Micro.Center.Utils.Enums;

namespace Micro.Center.Web.Models.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }
    }
}
