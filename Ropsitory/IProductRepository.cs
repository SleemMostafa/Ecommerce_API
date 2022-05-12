using Ecommerce_API.Models;
using System.Collections.Generic;

namespace Ecommerce_API.Ropsitory
{
    public interface IProductRepository:IRopsitory<Product,int>
    {
        public List<Product> GetProductsByCategoryId(int id);
    }
}
