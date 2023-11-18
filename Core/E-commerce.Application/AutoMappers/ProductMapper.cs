using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.AutoMappers
{
    public sealed class ProductMapper
    {
        public ProductDTO MapToDto(Product product) 
        {
            if (product == null)
            {
                return null;
            }

            return new ProductDTO
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ProductImages = product.ProductImages.Select(pi => new ProductImageDTO
                {
                    Id = pi.Id,
                    Image = pi.Image,
                    ProductID = pi.ProductID
                }).ToList()
            };





        }




    }
}
