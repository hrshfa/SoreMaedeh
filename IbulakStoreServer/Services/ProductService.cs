﻿using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Products;
using System.Linq;

namespace IbulakStoreServer.Services
{
    public class ProductService
    {
        private readonly StoreDbContext _context;
        public ProductService(StoreDbContext context)
        {
            _context=context;
        }
        public Product? Get(int id)
        {
            Product? product =  _context.Products.Find(id);
            return product;
        }
        public async Task<Product?> GetAsync(int id)
        {
            Product? product =await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<Product?> FindByIdAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<List<Product>> GetsAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<List<Product>> GetsByCategoryAsync(int categoryId)
        {
            List<Product> products = await _context.Products.Where(product=> product.CategoryId== categoryId).ToListAsync();
            return products;
        }
        public async Task AddAsync(Product product)
        {
            Category? category = await _context.Categories.FindAsync(product.CategoryId);
            if (category is null)
            {
                throw new Exception("دسته بندی محصولی با این شناسه پیدا نشد.");
            }
            product.Category = category;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(Product product)
        {
            Product? oldProduct = await _context.Products.FindAsync(product.Id);
            if (oldProduct is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            oldProduct.Price=product.Price;
            oldProduct.Name=product.Name;
            oldProduct.Description = product.Description;
            oldProduct.ImageFileName = product.ImageFileName;
            oldProduct.Count = product.Count;
            oldProduct.CategoryId = product.CategoryId;
            _context.Products.Update(oldProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                throw new Exception("محصولی با این شناسه پیدا نشد.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SearchResponseDto>> SearchAsync(SearchRequestDto model)
        {
            var products =await _context.Products
                                .Where(a=> 
                                (model.Count==null || a.Count<=model.Count)
                               && (model.FromDate == null || a.CreatedAt>=model.FromDate)
                               && (model.ToDate == null || a.CreatedAt<=model.ToDate)
                               && (model.CategoryName == null || a.Category.Name.Contains(model.CategoryName))
                               && (model.ProductName == null || a.Name.Contains(model.ProductName))
                                )
                                .Skip(model.PageNo*model.PageSize)
                                .Take(model.PageSize)
                                .Select(a=> new SearchResponseDto
                                {
                                    ProductId=a.Id,
                                    ProductName=a.Name,
                                    CategoryId=a.CategoryId,
                                    Count=a.Count,
                                    Price= a.Price,
                                    CreatedAt= a.CreatedAt,
                                    Description= a.Description,
                                    CategoryName= a.Category.Name,
                                    CategoryImageFileName= a.Category.ImageFileName,    
                                    ProductImageFileName = a.ImageFileName
                                }
                )
                                .ToListAsync();
            return products;
        }
    }
}
