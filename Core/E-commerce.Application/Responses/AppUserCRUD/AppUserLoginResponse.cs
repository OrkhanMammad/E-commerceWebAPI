using E_commerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.AppUserCRUD
{
    public sealed class AppUserLoginResponse : BaseResponse
    {
        public TokenDTO Token { get; set; } = null;
    }
}
