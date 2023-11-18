using E_commerce.Application.DTOs;
using E_commerce.Application.RequestParameters;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Responses.ProductCRUD
{
    public sealed class ProductGetAllResponse : BaseResponse
    {
        public List<ProductDTO> Products { get; set; }= new List<ProductDTO>();
        public Pagination Pagination { get; set; }=new Pagination();



    }
}
