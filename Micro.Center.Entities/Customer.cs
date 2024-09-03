using Micro.Center.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Center.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }



        public List<Order> Orders { get; set; }


        
        public string FullName
        {

            get
            {
                return $"{FirstName} - {LastName}";
            }

        }

        [NotMapped]
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }

    }
}
