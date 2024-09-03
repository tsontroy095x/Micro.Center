using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Micro.Center.Web.Models.Order
{
    public class CreateUpdateOrderViewModel
    {
        public int Id { get; set; }
        public string? Notes { get; set; }


        [Display(Name = "Customer")]
        public int CustomerId { get; set; }


        [Display(Name = "Products")]
        public List<int> ProductIds { get; set; }


        [ValidateNever]
        public SelectList CustomerSelectList { get; set; }


        [ValidateNever]
        public MultiSelectList ProductMultiSelectList { get; set; }
    }
}
