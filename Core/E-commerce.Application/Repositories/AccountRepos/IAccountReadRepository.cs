using E_commerce.Application.DTOs;
using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.AppUserCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.AccountRepos
{
    public interface IAccountReadRepository
    {
        AppUserGetAllResponse GetAllAppUsers(Pagination pagination);
        Task<AppUserGetByIdResponse> GetAppUserById(string id);

    }
}
