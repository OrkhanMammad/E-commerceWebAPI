using E_commerce.Application.AutoMappers;
using E_commerce.Application.DTOs;
using E_commerce.Application.Repositories.ProductRepos;
using E_commerce.Application.RequestParameters;
using E_commerce.Application.Responses.ProductCRUD;
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
    public class ProductReadRepository : IProductReadRepository
    {
        readonly AppDbContext _context;
        public ProductReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public ProductGetAllResponse GetAllProducts(Pagination pagination)
        {
            try
            {
                IQueryable<Product> products = _context.Products
               .Where(p => p.IsDeleted == false)
               .Include(p => p.ProductImages)
               .AsNoTracking();

                if (products.Any())
                {
                    ProductMapper productMapper = new ProductMapper();
                    return new ProductGetAllResponse
                    {

                        ResponseCode = 1,
                        Message = "All Products Obtained",
                        Products = products.Skip((pagination.PageIndex - 1) * pagination.Size)
                                            .Take(pagination.Size)
                                            .Select(p => productMapper.MapToDto(p))                                            
                                            .ToList(),

                        Pagination = new Pagination { 
                                                      Size = pagination.Size,
                                                      PageIndex = pagination.PageIndex,     
                                                      PageCount = (short)Math.Ceiling((decimal)products.Count() / pagination.Size) 
                                                    }

                    };
                }
                else              
                    return new ProductGetAllResponse
                    {
                        ResponseCode = 2,
                        Message = "Products Could Not Found",
                    };
                
            }
            catch(Exception ex)
            {
                return new ProductGetAllResponse
                {
                    ResponseCode = 404,
                    Message = ex.Message,
                };
            }          
        }

        public ProductGetByIdResponse GetProductById(int id)
        {
            try
            {
                Product? dbProduct = _context.Products
                                            .AsNoTracking()
                                            .Include(p => p.ProductImages)
                                            .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);

                if (dbProduct != null)
                {
                    ProductMapper mapper = new ProductMapper();
                    ProductDTO productDto = mapper.MapToDto(dbProduct);
                    return new ProductGetByIdResponse { product = productDto, Message = "Product Obtained", ResponseCode = 1 };
                }
                return new ProductGetByIdResponse { product = null, Message = "Product Could Not Found", ResponseCode = 2 };
            }

            catch (Exception ex)
            {
                return new ProductGetByIdResponse { product = new ProductDTO(), Message = ex.Message, ResponseCode = 404 };
            }
        }

        //public ProductGetByIdResponse GetProductById(int id)
        //{
        //    try
        //    {
        //        ProductDTO? productDTO = _context.Products
        //                                    .Where(p => p.Id == id && p.IsDeleted == false)
        //                                    .AsNoTracking()
        //                                    .Include(p => p.ProductImages)
        //                                    .Select(p=>new ProductDTO 
        //                                                            { Id=p.Id,
        //                                                              Name=p.Name,
        //                                                              Price=p.Price,
        //                                                              ProductImages = p.ProductImages.Select(pi => new ProductImageDTO
        //                                                              {
        //                                                                  Id = pi.Id,
        //                                                                  Image = pi.Image,
        //                                                                  ProductID = pi.ProductID
        //                                                              }).ToList(),
        //                                                              Description=p.Description,
        //                                                              Stock=p.Stock,
        //                                                              }).FirstOrDefault();





        //        if (productDTO != null)
        //        {
        //            return new ProductGetByIdResponse { product = productDTO, Message = "Product Obtained", ResponseCode = 1 };
        //        }
        //        return new ProductGetByIdResponse { product = null, Message = "Product Could Not Found", ResponseCode = 2 };
        //    }

        //    catch (Exception ex)
        //    {
        //        return new ProductGetByIdResponse { product = new ProductDTO(), Message = ex.Message, ResponseCode = 404 };
        //    }
        //}
    }
}
