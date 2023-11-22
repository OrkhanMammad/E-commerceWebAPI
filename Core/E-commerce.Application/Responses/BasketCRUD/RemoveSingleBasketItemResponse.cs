using E_commerce.Application.DTOs;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.BasketCRUD
{
    public sealed class RemoveSingleBasketItemResponse : BaseResponse
    {
        public IEnumerable<BasketItemDTO> BasketItems { get; set; }
    }
}
