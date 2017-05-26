using Model.Tables;
using Persistence.Contexts;
using System.Data.Entity;
using System.Linq;
using System;

namespace Persistence.DAL.Tables
{
    public class CategoryDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Category> Get()
        {
            return context
                .Categories;
        }

        public IQueryable<Category> GetOrderedByName()
        {
            return context.Categories
                .OrderBy(b => b.Name);
        }

        private string GetName(Category b)
        {
            if (b.CategoryId > 0)
                return b.Name;
            return string.Empty;
        }

        public Category ById(long id)
        {
            return context
                .Categories
                .Where(s => s.CategoryId == id)
                .First();
        }

        public void Save(Category item)
        {
            if (item.CategoryId == 0)
                context.Categories.Add(item);
            else
                context.Entry(item).State = EntityState.Modified;

            context.SaveChanges();
        }

        public Category Delete(long id)
        {
            var item = ById(id);

            context.Categories.Remove(item);
            context.SaveChanges();

            return item;
        }
    }
}