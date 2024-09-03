using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Center.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }


        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }


        public List<Order> Order { get; set; }


        public string NameAndPrice
        {
            get
            {
                return $"{Name} - {Price} JOD";
            }
        }
    }
}
