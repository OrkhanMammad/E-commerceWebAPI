using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses
{
    public class BaseResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
