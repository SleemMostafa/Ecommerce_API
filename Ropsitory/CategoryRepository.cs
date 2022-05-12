using Ecommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce_API.Ropsitory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext context;

        public CategoryRepository(ApplicationContext _context)

        {
            context = _context;
        }
        public int Delete(int elment)
        {
            var query = GetById(elment);

            if (query != null)
            {
                context.categories.Remove(query);

                return context.SaveChanges();
            }
            return 0;
        }

        public List<Category> GetAll()
        {
            return context.categories.ToList();
        }

        public Category GetById(int elment)
        {
            return context.categories.FirstOrDefault(c => c.ID == elment);
        }

        public int Insert(Category entity)
        {
            if (entity != null)
            {
                context.categories.Add(entity);
                return context.SaveChanges();
            }
            return 0;
               
        }

        public int Update(int elment, Category entity)
        {
            var query = GetById(elment);
            if(query != null)
            {
                query.Name = entity.Name;
                return context.SaveChanges();
            }
            return 0;

        }
    }
}
