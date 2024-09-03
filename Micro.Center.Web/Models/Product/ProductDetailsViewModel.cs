using Micro.Center.Entities;

namespace Micro.Center.Web.Models.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ProductTypeName { get; set; }
    }
}
