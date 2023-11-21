using E_commerce.Application.Abstractions.Hubs;
using E_commerce.Application.Repositories.ProductRepos;
using E_commerce.Application.Responses.ProductCRUD;
using E_commerce.Application.ViewModels.ProductVMs;
using E_commerce.Domain.Entities;
using E_commerce.Persistence.DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Persistence.Repositories.ProductRepos
{
    public class ProductWriteRepository : IProductWriteRepository
    {
        readonly AppDbContext _context;
        readonly IProductHubService _productHubService;
        public ProductWriteRepository(AppDbContext context, IProductHubService productHubService)
        {
            _context = context;
            _productHubService = productHubService;
        }
        public async Task<ProductAddResponse> AddNewProduct(ProductAddVM productAddVM)
        {
            try
            {
                IQueryable<Product> DBproducts = _context.Products.Where(p => p.IsDeleted == false).AsQueryable().AsNoTracking();
                if (DBproducts.Any(p => p.Name.ToUpper() == productAddVM.Name.ToUpper()))
                {
                    return new ProductAddResponse { ResponseCode = 2, Message = "This Product Already Exists" };
                }
                DateTime dateTime = DateTime.Now;
                Product newProduct = new Product
                {
                    Name = productAddVM.Name,
                    Price = productAddVM.Price,
                    Stock = productAddVM.Stock,
                    Description = productAddVM.Description,
                    ProductImages = new List<ProductImage>(),
                    CreatedDate = dateTime
                };

                foreach (byte[] image in productAddVM.Images)
                {
                    newProduct.ProductImages.Add(new ProductImage { Image = image, CreatedDate = dateTime });
                }

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                await _productHubService.ProductAddedMessageAsync($"{productAddVM.Name} Has Been Added");

                return new ProductAddResponse { ResponseCode = 1, Message = productAddVM.Name + " " + "Added Successfully" };
            }
            catch (Exception ex)
            {
                return new ProductAddResponse { ResponseCode = 404, Message = ex.Message };
            }
           
        }

       
        public async Task<ProductUpdateResponse> UpdateProduct(ProductUpdateVM productUpdateVM)
        {
            try
            {
                IQueryable<Product> DBproducts = _context.Products.Where(p=>p.IsDeleted==false).Include(p=>p.ProductImages);
                if (DBproducts.Any(p => p.Name == productUpdateVM.Name))
                {
                    return new ProductUpdateResponse { ResponseCode = 3, Message = productUpdateVM.Name + " Is Already Used For Another Product" };
                }
                DateTime dateTime = DateTime.Now;
                Product updateProduct = DBproducts.FirstOrDefault(p => p.Id == productUpdateVM.ProductID);
                if (updateProduct == null)
                {
                    return new ProductUpdateResponse { ResponseCode = 2, Message = "Product Could Not Found" };
                }
                string productPreviousName = updateProduct.Name;

                updateProduct.Price = productUpdateVM.Price;
                updateProduct.Stock = productUpdateVM.Stock;
                updateProduct.Description = productUpdateVM.Description;
                updateProduct.Name = productUpdateVM.Name;
                updateProduct.ProductImages = new List<ProductImage>();
                updateProduct.UpdatedDate = dateTime;
                foreach (byte[] image in productUpdateVM.Images)
                {
                    updateProduct.ProductImages.Add(new ProductImage { Image = image, CreatedDate = dateTime });
                }
                await _context.SaveChangesAsync();
                return new ProductUpdateResponse { ResponseCode = 1, Message = productPreviousName + " " + "Updated Successfully" };
            }
            catch (Exception ex)
            {
                return new ProductUpdateResponse { ResponseCode = 404, Message = ex.Message };
            }
            
        }


        public async Task<ProductDeleteResponse> DeleteProduct(int id)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                Product deleteProduct = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
                if (deleteProduct == null)
                {
                    return new ProductDeleteResponse { ResponseCode = 2, Message = "Product Could Not Found" };
                }
                deleteProduct.IsDeleted = true;
                deleteProduct.DeletedDate = dateTime;
                await _context.SaveChangesAsync();
                return new ProductDeleteResponse { ResponseCode = 1, Message = deleteProduct.Name + " " + "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ProductDeleteResponse { ResponseCode = 404, Message = ex.Message };
            }         
        }
    }
}
