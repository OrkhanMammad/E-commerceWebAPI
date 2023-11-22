using E_commerce.Application.Responses.OrderCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.OrderRepos
{
    public interface IOrderWriteRepository
    {
        Task<MakeNewOrderResponse> MakeNewOrderAsync(string addressLine);
    }
}
