using Model.Registers;
using Persistence.DAL.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Registers
{
    public class SupplierService : IDisposable
    {
        private SupplierDAL dal = new SupplierDAL();

        public IQueryable<Supplier> GetOrderedByName()
        { return dal.GetOrderedByName(); }

        public Supplier ById(long id)
        { return dal.ById(id); }

        public void Save(Supplier item)
        { dal.Save(item); }

        public Supplier Delete(long id)
        { return dal.Delete(id); }

        public void Dispose()
        {
            dal.Dispose();
        }
    }
}
