using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(AppUser appUser);
        string CreateRefreshToken();

    }
}
