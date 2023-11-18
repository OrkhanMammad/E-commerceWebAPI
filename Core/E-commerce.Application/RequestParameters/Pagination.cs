using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.RequestParameters
{
    public sealed class Pagination
    {
        public short Size { get; set; } = 5;
        public short PageIndex { get; set; } = 1;
        public short PageCount { get; set; } = 0;

    }
}
