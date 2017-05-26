using Model.Registers;
using Service.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aula6.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private ProductService service = new ProductService();

        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            return service.Get();
        }

        // GET: api/Products/5
        public Product Get(long? id)
        {
            if (id == null) return null;

            return service.ById(id.Value);
        }

        // POST: api/Products
        public void Post([FromBody]Product value)
        {
            service.Save(value);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product value)
        {
            service.Save(value);
        }

        // DELETE: api/Products/5
        public Product Delete(int id)
        {
            return service.Delete(id);
        }
    }
}
