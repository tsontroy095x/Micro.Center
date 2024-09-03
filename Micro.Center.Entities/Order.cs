using System.ComponentModel.DataAnnotations.Schema;

namespace Micro.Center.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; } 


        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; } 

        public string Notes { get; set; }

        
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
