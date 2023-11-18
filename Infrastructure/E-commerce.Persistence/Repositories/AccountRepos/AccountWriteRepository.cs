using E_commerce.Application.Abstractions.Token;
using E_commerce.Application.DTOs;
using E_commerce.Application.Repositories.AccountRepos;
using E_commerce.Application.Responses.AppUserCRUD;
using E_commerce.Application.ViewModels.AppUserVMs;
using E_commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.AccountRepos
{
    public sealed class AccountWriteRepository : IAccountWriteRepository
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        public AccountWriteRepository(UserManager<AppUser> userManager,
                                      RoleManager<IdentityRole> roleManager, 
                                      SignInManager<AppUser> signInManager, 
                                      ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;

        }

        public async Task<AppUserLoginResponse> Login(AppUserLoginVM appUserLoginVM)
        {
            try
            {
                AppUser appUser = await _userManager.FindByNameAsync(appUserLoginVM.UserName);
                if (appUser == null)
                {
                    return new AppUserLoginResponse { ResponseCode = 2, Message = "Username or Password is not correct" };
                }
                SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, appUserLoginVM.Password, false);
                if (!signInResult.Succeeded)
                {
                    return new AppUserLoginResponse { ResponseCode = 2, Message = "Username or Password is not correct" };
                }
                TokenDTO token = _tokenHandler.CreateAccessToken();

                return new AppUserLoginResponse { ResponseCode = 1, Message = "Logged In Successfully", Token = token };
            }
            catch (Exception ex)
            {
                return new AppUserLoginResponse { ResponseCode = 2, Message = ex.Message };
            }
            
        }

        public async Task<AppUserAddResponse> Register(AppUserAddVM appUserAddVM)
        {
            try
            {
                AppUser newAppuser = new AppUser { Name = appUserAddVM.Name,
                                                   Surname = appUserAddVM.Surname, 
                                                   UserName = appUserAddVM.UserName,    
                                                   Email = appUserAddVM.Email 
                                                 };

                IdentityResult identityResult = await _userManager.CreateAsync(newAppuser, appUserAddVM.Password);
                
                if (!identityResult.Succeeded)
                {
                    return new AppUserAddResponse { ResponseCode = 2, Message = identityResult.Errors.FirstOrDefault().Code, Description = identityResult.Errors.FirstOrDefault().Description  };
                }
                else
                {
                    await _userManager.AddToRoleAsync(newAppuser, "USER");
                    return new AppUserAddResponse { ResponseCode = 1, Message = "You Registered Successfully" };
                }
            }
            catch (Exception ex)
            {
                return new AppUserAddResponse { ResponseCode = 404, Message = ex.Message };
            }       
        }
    }
}
