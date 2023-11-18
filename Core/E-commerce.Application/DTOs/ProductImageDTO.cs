using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.DTOs
{
    public sealed class ProductImageDTO
    {
        public int Id { get; set; }
        public int ProductID { get; set; }

        public byte[] Image { get; set; }


    }
}
