using E_commerce.Application.AutoMappers;
using E_commerce.Application.DTOs;
using E_commerce.Application.Repositories.BasketRepos;
using E_commerce.Application.Responses.BasketCRUD;
using E_commerce.Domain.Entities.Identity;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.BasketRepos
{
    public sealed class BasketReadRepository : IBasketReadRepository
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly AppDbContext _context;
        public BasketReadRepository(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor= contextAccessor;
            _context= context;
        }
        public async Task<GetBasketResponse> GetBasketItemsAsync()
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            AppUser? appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u=>u.UserName==userName);
            if(appUser== null)
            {
                return new GetBasketResponse { ResponseCode = 2, Message = "User Not Found" };
            }
            BasketItemMapper mapper = new BasketItemMapper();
            return new GetBasketResponse
            {
                ResponseCode = 1,
                Message = "All Basket Items Obtained",
                BasketItems = appUser.BasketItems.Select(bi => mapper.MapToDto(bi))
            };

        }
    }
}
