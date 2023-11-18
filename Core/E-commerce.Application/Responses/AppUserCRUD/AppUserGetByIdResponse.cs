using E_commerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.AppUserCRUD
{
    public class AppUserGetByIdResponse : BaseResponse
    {
       public AppUserDTO appUser { get; set; } = new AppUserDTO();

    }
}
