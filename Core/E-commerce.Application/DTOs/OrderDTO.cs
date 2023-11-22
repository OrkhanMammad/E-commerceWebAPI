using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs
{
    public sealed class OrderDTO
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public double TotalAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
