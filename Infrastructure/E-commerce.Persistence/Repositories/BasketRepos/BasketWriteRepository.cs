using E_commerce.Application.AutoMappers;
using E_commerce.Application.DTOs;
using E_commerce.Application.Repositories.BasketRepos;
using E_commerce.Application.Responses.BasketCRUD;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Entities.Identity;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.BasketRepos
{
    public sealed class BasketWriteRepository : IBasketWriteRepository
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly AppDbContext _context;
        public BasketWriteRepository(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<AddToBasketResponse> AddToBasket(int productId)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;
                AppUser? appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u => u.UserName == userName);
                
                
                if (appUser == null)
                {
                    return new AddToBasketResponse { ResponseCode = 404, Message = "User Not Found" };
                }
                Product? product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);
               
                if (product == null || product.Stock < 1 || product.IsDeleted)
                {
                    return new AddToBasketResponse { ResponseCode = 2, Message = "Product No More Exists" };
                }

                if (!appUser.BasketItems.Any(bi => bi.ProductID == product.Id))
                {
                    BasketItem basketItem = new BasketItem
                    {
                        ProductID = productId,
                        ProductName=product.Name,
                        Image = product.ProductImages.FirstOrDefault()?.Image,
                        UnitPrice = product.Price,
                        TotalPrice = product.Price,
                        Quantity = 1,
                        AppUserID = appUser.Id
                    };

                    appUser.BasketItems.Add(basketItem);
                    await _context.SaveChangesAsync();
                    stopwatch.Stop();
                    TimeSpan elapsed = stopwatch.Elapsed;
                    return new AddToBasketResponse { ResponseCode = 1, Message = $"{product.Name} added to basket{elapsed.TotalSeconds}" };
                }
                
                return new AddToBasketResponse { ResponseCode = 3, Message = $"{product.Name} already exists in basket " };

            }
            catch (Exception ex)
            {
                return new AddToBasketResponse { ResponseCode = 404, Message = ex.Message };
            }
        }       
       
        public async Task<RemoveSingleBasketItemResponse> RemoveSingleBasketAsync(int basketItemId)
        {
            try
            {
                var userName = _contextAccessor?.HttpContext?.User.Identity.Name;
                AppUser appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u => u.UserName == userName);
                if (appUser == null)
                {
                    return new RemoveSingleBasketItemResponse { ResponseCode = 404, 
                                                                Message = "User Not Found"
                                                              };
                }

                BasketItem basketItem = appUser.BasketItems.FirstOrDefault(b => b.Id == basketItemId);
                if (basketItem != null)
                {
                    appUser.BasketItems.Remove(basketItem);
                    await _context.SaveChangesAsync();
                }
                
                BasketItemMapper mapper = new BasketItemMapper();
                
                return new RemoveSingleBasketItemResponse { ResponseCode = 1, 
                                                            Message = "Basket Item Removed", 
                                                            BasketItems = appUser.BasketItems.Select(b=>mapper.MapToDto(b)),
                                                           };
            }
            catch (Exception ex)
            {
                return new RemoveSingleBasketItemResponse { ResponseCode = 404, 
                                                            Message = ex.Message 
                                                           };
            }
            
        }
        
        public async Task<RemoveAllBasketitemsResponse> RemoveAllBasketItemsAsync()
        {
            try
            {
                var userName = _contextAccessor?.HttpContext?.User.Identity.Name;
                AppUser appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u => u.UserName == userName);
                if (appUser == null)
                {
                    return new RemoveAllBasketitemsResponse
                    {
                        ResponseCode = 404,
                        Message = "User Not Found"
                    };
                }
                appUser.BasketItems.Clear();
                await _context.SaveChangesAsync();
                return new RemoveAllBasketitemsResponse { ResponseCode = 1, Message = "All Baskets Removed" };
            }
            catch (Exception ex)
            {
                return new RemoveAllBasketitemsResponse { ResponseCode = 404, Message = ex.Message };
            }
            
            

        }
       
        public async Task<ChangeBasketItemQuantityResponse> IncreaseBasketItemQuantityAsync(int basketItemId)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var userName = _contextAccessor?.HttpContext?.User.Identity.Name;
                AppUser appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u => u.UserName == userName);
                if (appUser == null)
                {
                    return new ChangeBasketItemQuantityResponse
                    {
                        ResponseCode = 404,
                        Message = "User Not Found"
                    };
                }

                BasketItem basketItem = appUser.BasketItems.FirstOrDefault(bi => bi.Id == basketItemId);
                BasketItemMapper mapper = new BasketItemMapper();
                if (basketItem == null)
                {
                    return new ChangeBasketItemQuantityResponse { ResponseCode = 2, Message = "Basket Item Could Not Found", BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b)) };

                }
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketItem.ProductID);
                ChangeBasketItemQuantityResponse response = new ChangeBasketItemQuantityResponse();

                if (product == null || product.IsDeleted)
                {
                    appUser.BasketItems.Remove(basketItem);
                    response.ResponseCode = 2;
                    response.Message = "Product Is Out Of Stock";
                    response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                }
                else 
                {
                    if (product.Stock > basketItem.Quantity)
                    {
                        basketItem.Quantity += 1;
                        basketItem.TotalPrice = basketItem.Quantity * basketItem.UnitPrice;
                        response.ResponseCode = 1;
                        response.Message = "Basket Quantity Increased";
                        response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                    }
                    else
                    {
                        response.ResponseCode = 2;
                        response.Message = $"{product.Name}'s stock value is {basketItem.Quantity}";
                        response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                    }
                }
                
                await _context.SaveChangesAsync();
                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;
                Console.WriteLine(elapsed.TotalSeconds);
                return response;
            }
            catch(Exception ex)
            {
                return new ChangeBasketItemQuantityResponse { ResponseCode=404, Message=ex.Message, BasketItems=new List<BasketItemDTO>()};
            }
            
        }
       
        public async Task<ChangeBasketItemQuantityResponse> DecreaseBasketItemQuantityAsync(int basketItemId)
        {
            try
            {               
                var userName = _contextAccessor?.HttpContext?.User.Identity.Name;
                AppUser appUser = await _context.Users.Include(u => u.BasketItems).FirstOrDefaultAsync(u => u.UserName == userName);
                if (appUser == null)
                {
                    return new ChangeBasketItemQuantityResponse
                    {
                        ResponseCode = 404,
                        Message = "User Not Found"
                    };
                }

                BasketItem basketItem = appUser.BasketItems.FirstOrDefault(bi => bi.Id == basketItemId);
                BasketItemMapper mapper = new BasketItemMapper();
                if (basketItem == null)
                {
                    return new ChangeBasketItemQuantityResponse { ResponseCode = 2, Message = "Basket Item Could Not Found", BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b)) };

                }
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketItem.ProductID);
                ChangeBasketItemQuantityResponse response = new ChangeBasketItemQuantityResponse();

                if (product == null || product.IsDeleted)
                {
                    appUser.BasketItems.Remove(basketItem);
                    response.ResponseCode = 2;
                    response.Message = "Product Is Out Of Stock";
                    response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                }
                else
                {
                    if (basketItem.Quantity>1)
                    {
                        basketItem.Quantity -= 1;
                        basketItem.TotalPrice = basketItem.Quantity * basketItem.UnitPrice;
                        response.ResponseCode = 1;
                        response.Message = "Basket Quantity Decreased";
                        response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                    }
                    else
                    {
                        response.ResponseCode = 2;
                        response.Message = "Quantity can not be less than 1";
                        response.BasketItems = appUser.BasketItems.Select(b => mapper.MapToDto(b));
                    }
                }

                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                return new ChangeBasketItemQuantityResponse { ResponseCode = 404, Message = ex.Message, BasketItems = new List<BasketItemDTO>() };
            }

        }
    }
}
