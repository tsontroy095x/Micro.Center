using Micro.Center.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace Micro.Center.Web.Models.Customer
{
    public class CreateUpdateCustomerViewModel
    {
        public int Id { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        public string Email { get; set; }
        public Gender Gender { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
