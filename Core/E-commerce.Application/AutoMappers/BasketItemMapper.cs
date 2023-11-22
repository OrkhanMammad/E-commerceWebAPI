using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.AutoMappers
{
    public sealed class BasketItemMapper
    {
        public BasketItemDTO MapToDto(BasketItem basketItem)
        {
            if (basketItem == null)
            {
                return null;
            }

            return new BasketItemDTO
            {
                Id = basketItem.Id,
                UnitPrice = basketItem.UnitPrice,
                Quantity = basketItem.Quantity,
                ProductID= basketItem.ProductID,
                TotalPrice=basketItem.TotalPrice,
                Image= basketItem.Image,
            };
        }
    }
}
