using E_commerce.Application.Responses.BasketCRUD;
using E_commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.BasketRepos
{
    public interface IBasketWriteRepository
    {
        Task<AddToBasketResponse> AddToBasket(int productId);
        Task<RemoveSingleBasketItemResponse> RemoveSingleBasketAsync(int basketItemId);
        Task<RemoveAllBasketitemsResponse> RemoveAllBasketItemsAsync();
        Task<ChangeBasketItemQuantityResponse> IncreaseBasketItemQuantityAsync(int basketItemId);
        Task<ChangeBasketItemQuantityResponse> DecreaseBasketItemQuantityAsync(int basketItemId);

    }
}
