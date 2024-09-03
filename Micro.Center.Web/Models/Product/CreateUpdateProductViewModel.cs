using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Micro.Center.Web.Models.Product
{
    public class CreateUpdateProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


        [Display(Name = "ProductTypes")]
        public int ProductTypeId { get; set; }


        [ValidateNever]
        public MultiSelectList ProductTypesMultiSelectList { get; set; }
    }
}
