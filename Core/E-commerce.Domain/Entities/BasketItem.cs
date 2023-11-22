using E_commerce.Domain.Entities.Common;
using E_commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities
{
    public sealed class BasketItem : BaseEntity
    {
        [NotMapped]
        public AppUser AppUser { get; set; }
        public string AppUserID { get; set; }
        [NotMapped]
        public Product? Product { get; set; }
        public int ProductID { get; set; }
        public short Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public byte[] Image { get; set; }
        
    }
}
