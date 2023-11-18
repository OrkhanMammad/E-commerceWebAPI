using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.AppUserCRUD
{
    public sealed class AppUserAddResponse 
    {
        public short ResponseCode { get; set; }
        public string Message { get; set; }

        public string Description { get; set; } = "";
    }
}
