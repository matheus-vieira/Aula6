using Model.Tables;
using Persistence.DAL.Tables;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Service.Tables
{
    public class CategoryService
    {
        private CategoryDAL dal = new CategoryDAL();

        #region [ Get's ]

        public IQueryable<Category> Get()
        { return dal.Get(); }

        public IQueryable<Category> GetOrderedByName()
        { return dal.GetOrderedByName(); }

        public Category ById(long id)
        { return dal.ById(id); }

        #endregion [ Get's ]

        public void Save(Category item)
        { dal.Save(item); }

        public Category Delete(long id)
        { return dal.Delete(id); }
    }
}
