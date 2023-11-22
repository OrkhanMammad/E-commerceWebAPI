using E_commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities
{
    public sealed class Order
    {
        public int Id { get; set; }
        [NotMapped]
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public string AddressLine { get; set; }        
        public double TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Status { get; set; }
    }
}
