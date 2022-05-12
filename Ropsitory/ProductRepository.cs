using Ecommerce_API.DTO;
using Ecommerce_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_API.Ropsitory
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext context;

        public ProductRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public int Delete(int pdoductId)
        {
            var query = GetById(pdoductId);
            context.Products.Remove(query);
            return context.SaveChanges();

        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int pdoductId)
        {  
            return context.Products.FirstOrDefault(p => p.ID == pdoductId);   
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return context.Products.Where(p => p.CateogryID == categoryId).ToList();
        }

        public int Insert(Product newProduct)
        {
            if (newProduct != null)
            {
                context.Products.Add(newProduct);
                return context.SaveChanges();
            }
            return 0;

        }
        
        public int Update(int id,Product newProduct)
        {
            var oldProduct = GetById(id);
            if (oldProduct != null)
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.Price = newProduct.Price;
                oldProduct.Photo = newProduct.Photo;
                oldProduct.PurchaseDate = newProduct.PurchaseDate;
                oldProduct.Quantity = newProduct.Quantity;
                oldProduct.CateogryID = newProduct.CateogryID;
                oldProduct.Description = newProduct.Description;
                return context.SaveChanges();
            }
            return 0;
        }
    }
}
