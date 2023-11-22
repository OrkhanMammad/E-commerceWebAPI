using E_commerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.OrderCRUD
{
    public sealed class MakeNewOrderResponse : BaseResponse
    {
        public OrderDTO? Order { get; set; } = new OrderDTO();
    }
}
