using E_commerce.Application.Repositories.OrderRepos;
using E_commerce.Application.Responses.OrderCRUD;
using E_commerce.Persistence.Repositories.OrderRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        public OrdersController(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        [HttpGet]
        [Route("MakenOrder")]
        public async Task<MakeNewOrderResponse> MakeAnOrder(string addressLine)
        {
            return await _orderWriteRepository.MakeNewOrderAsync(addressLine);
        }

    }
}
