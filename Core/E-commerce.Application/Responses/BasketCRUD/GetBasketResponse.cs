using E_commerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.BasketCRUD
{
    public sealed class GetBasketResponse : BaseResponse
    {
        public IEnumerable<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
    }
}
