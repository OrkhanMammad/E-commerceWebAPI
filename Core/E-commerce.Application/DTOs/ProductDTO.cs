using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs
{
    public sealed class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public double Price { get; set; }

        public List<ProductImageDTO> ProductImages { get; set; }




    }
}
