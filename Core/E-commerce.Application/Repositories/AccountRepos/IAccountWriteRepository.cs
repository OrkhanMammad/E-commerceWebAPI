using E_commerce.Application.Responses.AppUserCRUD;
using E_commerce.Application.ViewModels.AppUserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.AccountRepos
{
    public interface IAccountWriteRepository
    {
        Task<AppUserAddResponse> Register(AppUserAddVM appUserAddVM);
        Task<AppUserLoginResponse> Login(AppUserLoginVM appUserLoginVM);

    }
}
