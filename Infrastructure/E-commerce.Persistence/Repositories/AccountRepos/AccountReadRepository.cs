using E_commerce.Application.AutoMappers;
using E_commerce.Application.DTOs;
using E_commerce.Application.Repositories.AccountRepos;
using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.AppUserCRUD;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Entities.Identity;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.AccountRepos
{
    public class AccountReadRepository : IAccountReadRepository
    {
        readonly AppDbContext _context;
        public AccountReadRepository(AppDbContext context)
        {
            _context= context;
        }


        public AppUserGetAllResponse GetAllAppUsers(Pagination pagination)
        {
            try
            {
                IQueryable<AppUser> appUsers = _context.Users.AsNoTracking();
                if (appUsers.Any())
                {
                    AppUserMapper appUserMapper = new AppUserMapper();
                    return new AppUserGetAllResponse
                    {
                        Appusers = appUsers.Skip((pagination.PageIndex - 1) * pagination.Size)
                                           .Take(pagination.Size)
                                           .Select(p => appUserMapper.MapToDto(p))
                                           .ToList(),
                        ResponseCode = 1,
                        Message = "All Users Obtained",
                        Pagination = new Pagination
                        {
                            Size = pagination.Size,
                            PageIndex = pagination.PageIndex,
                            PageCount = (short)Math.Ceiling((decimal)appUsers.Count() / pagination.Size)
                        }
                    };
                }
                else
                {
                    return new AppUserGetAllResponse
                    {
                        ResponseCode = 2,
                        Message = "No User Exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AppUserGetAllResponse { ResponseCode = 404, Message = ex.Message };
            }
        }

        public async Task<AppUserGetByIdResponse> GetAppUserById(string? id)
        {
            if (id != null)
            {
                try
                {
                   IQueryable<AppUser> Users = _context.Users;
                    AppUser DBappUser = Users.FirstOrDefault(u=>u.Id== id);
                    if (DBappUser != null)
                    {
                        AppUserMapper appUserMapper = new AppUserMapper();
                        return new AppUserGetByIdResponse
                        {
                            ResponseCode = 1,
                            Message = "User Obtained",
                            appUser = appUserMapper.MapToDto(DBappUser),

                        };
                    }
                    return new AppUserGetByIdResponse
                    {
                        ResponseCode = 2,
                        Message = "User Could Not Found"
                    };
                }
                catch (Exception ex)
                {
                    return new AppUserGetByIdResponse
                    {
                        ResponseCode = 404,
                        Message = ex.Message
                    };
                }
            }
            return new AppUserGetByIdResponse
            {
                ResponseCode = 2,
                Message = "User Could Not Found"
            }; 
        }

        
    }
}
