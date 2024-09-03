using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Center.Entities
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public List<Product> Product { get; set; }

    }
}
