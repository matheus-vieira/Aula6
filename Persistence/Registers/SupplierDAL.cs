using Model.Registers;
using Persistence.Contexts;
using System;
using System.Data.Entity;
using System.Linq;

namespace Persistence.DAL.Registers
{
    public class SupplierDAL : IDisposable
    {
        private EFContext context = new EFContext();

        public IQueryable<Supplier> GetOrderedByName()
        {
            return context.Suppliers.OrderBy(b => b.Name);
        }

        public Supplier ById(long id)
        {
            return context
                .Suppliers
                .Where(s => s.SupplierId == id)
                .Include("Products.Category")
                .First();
        }

        public void Save(Supplier item)
        {
            if (item.SupplierId == 0)
                context.Suppliers.Add(item);
            else
                context.Entry(item).State = EntityState.Modified;

            context.SaveChanges();
        }

        public Supplier Delete(long id)
        {
            var item = ById(id);

            context.Suppliers.Remove(item);
            context.SaveChanges();

            return item;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
