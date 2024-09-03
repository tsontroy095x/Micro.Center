using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Micro.Center.Web.Models.Order
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        public DateTime DateTime { get; set; }


        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }



        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }
    }
}
