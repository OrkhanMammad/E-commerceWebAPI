using E_commerce.Application.Repositories.BasketRepos;
using E_commerce.Application.Responses.BasketCRUD;
using E_commerce.Domain.Entities;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin", Roles = "ADMİN, USER")]
    //[Authorize(Roles = "USER")]
    public class BasketsController : ControllerBase
    {
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketReadRepository _basketReadRepository;

        public BasketsController(IBasketWriteRepository basketWriteRepository, IBasketReadRepository basketReadRepository)
        {
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;
        }

        [HttpGet]
        [Route("GetBasket")]
        public async Task<GetBasketResponse> GetBasket()
        {
            return await _basketReadRepository.GetBasketItemsAsync();
        }

        [HttpGet]
        [Route("AddToBasket")]
        public async Task<AddToBasketResponse> AddToBasket(int productId)
        {
            return await _basketWriteRepository.AddToBasket(productId);
        }

        [HttpGet]
        [Route("RemoveBasketItem")]
        public async Task<RemoveSingleBasketItemResponse> RemoveBasketItem(int basketId)
        {
           return await _basketWriteRepository.RemoveSingleBasketAsync(basketId);
        }
        [HttpGet]
        [Route("RemoveAllBaskets")]
        public async Task<RemoveAllBasketitemsResponse> RemoveAllBaskets()
        {
            return await _basketWriteRepository.RemoveAllBasketItemsAsync();
        }
        [HttpGet]
        [Route("IncreaseBasketCount")]
        public async Task<ChangeBasketItemQuantityResponse> IncreaseBasketCount(int basketItemId)
        {
           return await _basketWriteRepository.IncreaseBasketItemQuantityAsync(basketItemId);
        }
        [HttpGet]
        [Route("DecreaseBasketCount")]
        public async Task<ChangeBasketItemQuantityResponse> DecreaseBasketCount(int basketItemId)
        {
            return await _basketWriteRepository.DecreaseBasketItemQuantityAsync(basketItemId);
        }

    }
}
