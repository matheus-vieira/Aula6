using Aula6.Models;
using Model.Tables;
using Service.Registers;
using Service.Tables;
using System.Linq;
using System.Web.Http;

namespace Aula6.Controllers.Api
{
    public class CategoriesController : ApiController
    {
        private CategoryService service = new CategoryService();
        private ProductService productService = new ProductService();

        // GET: api/Categories
        public CategoryListAPIModel Get()
        {
            var apiModel = new CategoryListAPIModel();

            try
            {
                apiModel.Result = service.Get().ToList();
            }
            catch (System.Exception)
            {
                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // GET: api/Categories/5
        public CategoryAPIModel Get(long? id)
        {
            var apiModel = new CategoryAPIModel();

            try
            {
                if (id == null)
                    apiModel.Message = "!OK";
                else
                {
                    apiModel.Result = service.ById(id.Value);
                    if (apiModel.Result != null)
                        apiModel.Result.Products = productService
                            .GetByCategory(id.Value).ToList();
                }
            }
            catch (System.Exception)
            {
                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // POST: api/Categories
        public void Post([FromBody]Category value)
        { service.Save(value); }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]Category value)
        { service.Save(value); }

        // DELETE: api/Categories/5
        public void Delete(int id)
        { service.Delete(id); }
    }
}
