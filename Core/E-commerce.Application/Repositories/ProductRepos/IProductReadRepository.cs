using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.ProductCRUD;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.ProductRepos
{
    public interface IProductReadRepository
    {
        ProductGetAllResponse GetAllProducts(Pagination pagination);
        ProductGetByIdResponse GetProductById(int id);


    }
}
