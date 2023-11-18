using E_commerce.Application.Responses.ProductCRUD;
using E_commerce.Application.ViewModels.ProductVMs;
using E_commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Repositories.ProductRepos
{
    public interface IProductWriteRepository
    {
        Task<ProductAddResponse> AddNewProduct(ProductAddVM productAddVM);
        Task<ProductUpdateResponse> UpdateProduct(ProductUpdateVM productUpdateVM);
        Task<ProductDeleteResponse> DeleteProduct(int id);
    }
}
