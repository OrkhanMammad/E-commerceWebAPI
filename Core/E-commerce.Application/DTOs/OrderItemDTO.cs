using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs
{
    public sealed class OrderItemDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public short Quantity { get; set; }
        public double TotalAmount { get; set; }
        public string ProductName { get; set; }
    }
}
