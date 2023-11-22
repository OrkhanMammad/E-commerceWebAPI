using E_commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public double Price { get; set; }

        public List<ProductImage> ProductImages { get; set; }
        public List<BasketItem> BasketItems { get; set; }


    }
}
