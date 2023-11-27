using E_commerce.Application.Abstractions.Hubs;
using E_commerce.Application.AutoMappers;
using E_commerce.Application.Repositories.OrderRepos;
using E_commerce.Application.Responses.OrderCRUD;
using E_commerce.Application.Services;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Entities.Identity;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.OrderRepos
{
    public sealed class OrderWriteRepository : IOrderWriteRepository
    {
        readonly IHttpContextAccessor _contextAccessor;
        readonly AppDbContext _context;
        readonly IOrderHubService _orderHubService;
        
        public OrderWriteRepository(IHttpContextAccessor contextAccessor, AppDbContext context, IOrderHubService orderHubService)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _orderHubService = orderHubService;          
        }

        async Task<AppUser> GetAppUserAsync() 
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;

            AppUser? appUser = await _context.Users
                                             .Include(u => u.BasketItems)
                                             .Include(u => u.Orders)
                                             .FirstOrDefaultAsync(u => u.UserName == userName);
            return appUser;
        }

        Order CreateNewOrder(IQueryable<Product> products, List<BasketItem> basketItems, string addressLine)
        {
            Order newOrder = new Order { AddressLine = addressLine,
                                         Status = "Pending", 
                                         OrderItems = new List<OrderItem>(),    
                                         CreatedDate=DateTime.UtcNow.AddHours(4)
                                        };

            foreach (var basketItem in basketItems)
            {
                products.FirstOrDefault(p => p.Id == basketItem.ProductID).Stock -= basketItem.Quantity;
                newOrder.OrderItems.Add(new OrderItem
                {
                    Price = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    TotalAmount = basketItem.TotalPrice,
                    ProductName = basketItem.ProductName,
                    ProductId = basketItem.ProductID

                });
                newOrder.TotalAmount += basketItem.TotalPrice;
            }
            basketItems.Clear();
            return newOrder;
        }

        public async Task<MakeNewOrderResponse> MakeNewOrderAsync(string addressLine)
        {
            try
            {
                if (addressLine == null)
                    return new MakeNewOrderResponse { ResponseCode = 2, Message = "Address Must Be Added", };
                

                AppUser? appUser = await GetAppUserAsync();
                
                if (appUser == null)
                    return new MakeNewOrderResponse { ResponseCode = 404, Message = "User Not Found", };
                
                if (!appUser.BasketItems.Any())
                    return new MakeNewOrderResponse { ResponseCode = 404, Message = "Basket is Empty" };
                
                int[] ProductIDs = appUser.BasketItems.Select(bi => bi.ProductID).ToArray();

                IQueryable<Product> products = _context.Products
                                                       .Where(product => ProductIDs.Contains(product.Id));

                Order newOrder = CreateNewOrder(products, appUser.BasketItems, addressLine);
                appUser.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
                await _orderHubService.OrderAddedMessageAsync("New Order Added");
                OrderMapper mapper = new OrderMapper();
                return new MakeNewOrderResponse { ResponseCode = 1, Message = $"Order Is Successful", Order = mapper.MapToDto(newOrder) };

            }
            catch (Exception ex)
            {
                return new MakeNewOrderResponse { ResponseCode = 404, Message = ex.Message };
            }
        }

        //public async Task<MakeNewOrderResponse> MakeNewOrderAsync(string addressLine)
        //{
        //    try
        //    {
        //        Stopwatch stopwatch = new Stopwatch();
        //        stopwatch.Start();
        //        if (addressLine == null)
        //        {
        //            return new MakeNewOrderResponse { ResponseCode = 2, Message = "Address Must Be Added", };

        //        }
        //        var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;

        //        AppUser? appUser = await _context.Users
        //                                         .Include(u => u.BasketItems)
        //                                         .Include(u => u.Orders)
        //                                         .FirstOrDefaultAsync(u => u.UserName == userName);

        //        if (appUser == null)
        //        {
        //            return new MakeNewOrderResponse { ResponseCode = 404, Message = "User Not Found", };
        //        }
        //        if (!appUser.BasketItems.Any())
        //        {
        //            return new MakeNewOrderResponse { ResponseCode = 404, Message = "Basket is Empty" };
        //        }
        //        int[] ProductIDs = appUser.BasketItems.Select(bi => bi.ProductID).ToArray();

        //        IQueryable<Product> products = _context.Products
        //                                               .Where(product => ProductIDs.Contains(product.Id));

        //        Order newOrder = new Order { AddressLine = addressLine, Status = "Pending", OrderItems = new List<OrderItem>() };
        //        foreach (var basketItem in appUser.BasketItems)
        //        {
        //            products.FirstOrDefault(p => p.Id == basketItem.ProductID).Stock -= basketItem.Quantity;
        //            newOrder.OrderItems.Add(new OrderItem
        //            {
        //                Price = basketItem.UnitPrice,
        //                Quantity = basketItem.Quantity,
        //                TotalAmount = basketItem.TotalPrice,
        //                ProductName = basketItem.ProductName,
        //                ProductId = basketItem.ProductID

        //            });
        //            newOrder.TotalAmount += basketItem.TotalPrice;
        //        }
        //        appUser.BasketItems.Clear();
        //        appUser.Orders.Add(newOrder);
        //        await _context.SaveChangesAsync();
        //        await _orderHubService.OrderAddedMessageAsync("New Order Added");
        //        OrderMapper mapper = new OrderMapper();
        //        stopwatch.Stop();
        //        TimeSpan elapsed = stopwatch.Elapsed;
        //        return new MakeNewOrderResponse { ResponseCode = 1, Message = $"Order Is Successful {elapsed.TotalSeconds}", Order = mapper.MapToDto(newOrder) };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new MakeNewOrderResponse { ResponseCode = 404, Message = ex.Message };
        //    }
        //}


    }
}
