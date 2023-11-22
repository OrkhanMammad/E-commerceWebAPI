using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.AutoMappers
{
    public sealed class OrderMapper
    {
        public OrderDTO MapToDto(Order order)
        {
            if (order == null)
            {
                return null;
            }

            return new OrderDTO
            {
                Id = order.Id,
                TotalAmount= order.TotalAmount,
                AddressLine = order.AddressLine,
                OrderItems = order.OrderItems.Select(pi => new OrderItemDTO
                {
                    Id = pi.Id,
                    TotalAmount = pi.TotalAmount,
                    Price = pi.Price,
                    Quantity= pi.Quantity,
                    ProductName= pi.ProductName,
                }).ToList()
            };
        }

    }
}
