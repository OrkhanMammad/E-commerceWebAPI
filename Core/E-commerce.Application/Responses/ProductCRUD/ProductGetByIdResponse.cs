using E_commerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.ProductCRUD
{
    public sealed class ProductGetByIdResponse : BaseResponse
    {
       public ProductDTO product { get; set; }
       

    }
}
