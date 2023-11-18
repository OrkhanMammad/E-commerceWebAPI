using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.AutoMappers
{
    public class AppUserMapper
    {
        public AppUserDTO MapToDto(AppUser appuser)
        {
            if (appuser == null)
            {
                return null;
            }

            return new AppUserDTO
            {
                Id = appuser.Id,
                Email = appuser.Email,
                Name = appuser.Name,
                Surname = appuser.Surname,
                UserName = appuser.UserName,
            };
        }
    }
}
