using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.ViewModels.ProductVMs
{
    public sealed class ProductUpdateVM
    {
        public int ProductID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public double Price { get; set; }

        public List<byte[]> Images { get; set; }
    }
}
