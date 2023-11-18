using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Nullable<DateTime> UpdatedDate { get; set; }
        public virtual string? UpdatedBy { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }
        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
