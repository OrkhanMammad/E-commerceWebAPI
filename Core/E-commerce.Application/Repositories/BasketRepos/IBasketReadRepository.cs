using E_commerce.Application.DTOs;
using E_commerce.Application.Responses.BasketCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.BasketRepos
{
    public interface IBasketReadRepository
    {
        Task<GetBasketResponse> GetBasketItemsAsync();
    }
}
