using E_commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities
{
    public sealed class ProductImage : BaseEntity
    {
       [NotMapped]
       public Product Product { get; set; }
       public int ProductID { get; set; }

       public byte[] Image { get; set; }
       



    }
}
