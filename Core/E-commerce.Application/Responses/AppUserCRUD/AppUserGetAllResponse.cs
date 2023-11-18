using E_commerce.Application.DTOs;
using E_commerce.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.AppUserCRUD
{
    public class AppUserGetAllResponse : BaseResponse
    {
        public List<AppUserDTO> Appusers { get; set; } = new List<AppUserDTO>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}
