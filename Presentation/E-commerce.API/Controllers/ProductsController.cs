using E_commerce.Application.Repositories.ProductRepos;
using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.ProductCRUD;
using E_commerce.Application.ViewModels.ProductVMs;
using E_commerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    
    public class ProductsController : ControllerBase
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpPost]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts([FromBody]Pagination pagination)
        {
                return Ok(_productReadRepository.GetAllProducts(pagination));
        }

        [HttpGet]
        [Route("getProductById")]
        public IActionResult GetProductById(int id)
        {            
                return Ok(_productReadRepository.GetProductById(id));
        }

        [HttpPost]
        [Route("AddNewProduct")]
        public async Task<ProductAddResponse> AddNewProduct(ProductAddVM productAddVM)
        {         
            return await _productWriteRepository.AddNewProduct(productAddVM);
        }

        [HttpPost]
        [Route("updateProduct")]
        public async Task<ProductUpdateResponse> UpdateProduct(ProductUpdateVM productUpdateVM)
        {
            return await _productWriteRepository.UpdateProduct(productUpdateVM);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ProductDeleteResponse> DeleteProductById(int id)
        {
            return await _productWriteRepository.DeleteProduct(id);
        }

    }
}
