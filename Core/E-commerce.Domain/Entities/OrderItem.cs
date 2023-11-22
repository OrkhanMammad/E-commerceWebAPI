using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities
{
    public sealed class OrderItem 
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public short Quantity { get; set; }
        public double TotalAmount { get; set; }
        public string ProductName { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
