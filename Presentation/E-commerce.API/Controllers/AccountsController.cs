using E_commerce.Application.Repositories.AccountRepos;
using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.AppUserCRUD;
using E_commerce.Application.Services;
using E_commerce.Application.ViewModels.AppUserVMs;
using E_commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IAccountReadRepository _accountReadRepository;
        readonly IAccountWriteRepository _accountWriteRepository;
        public AccountsController(IAccountReadRepository accountReadRepository,
                                  IAccountWriteRepository accountWriteRepository,   
                                  RoleManager<IdentityRole> roleManager
                                 )
        {
            _accountReadRepository = accountReadRepository;
            _accountWriteRepository = accountWriteRepository;
            _roleManager = roleManager;
            

        }

        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers([FromQuery]Pagination pagination)
        {
            return Ok(_accountReadRepository.GetAllAppUsers(pagination));
        }

        [HttpGet]
        [Route("getUserById")]
        public Task<AppUserGetByIdResponse> GetUserById(string id)
        { 
        return _accountReadRepository.GetAppUserById(id);      
        }

        [HttpPost]
        [Route("Register")]

        public async Task<AppUserAddResponse> Register([FromQuery]AppUserAddVM appUserAddVM)
        {
            return await _accountWriteRepository.Register(appUserAddVM);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<AppUserLoginResponse> Login([FromBody]AppUserLoginVM appUserLoginVM)
        {
            return await _accountWriteRepository.Login(appUserLoginVM);
        }

        

        //[HttpGet]
        //[Route("addroles")]
        //public async Task<IActionResult> Addroles()
        //{
        //    var role = new IdentityRole("USER");

            
        //    return Ok(await _roleManager.CreateAsync(role));
        //}


    }
}
