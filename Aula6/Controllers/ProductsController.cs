using Model.Registers;
using Service.Registers;
using Service.Tables;
using System.Net;
using System.Web.Mvc;

namespace Aula6.Controllers
{
    public class ProductsController : Controller
    {

        #region [ Properties ]

        private CategoryService categoryService = new CategoryService();
        private ProductService productService = new ProductService();
        private SupplierService supplierService = new SupplierService();

        #endregion [ Properties ]

        #region [ Actions ]

        // GET: Products
        public ActionResult Index()
        {
            return View(productService.GetOrderedByName());
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            return GetViewById(id);
        }

        #region [ Create ]

        // GET: Products/Create
        public ActionResult Create()
        {
            PopulateViewBag();

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            return Save(product);
        }

        #endregion [ Create ]

        #region [ Edit ]

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            PopulateViewBag(productService.ById(id.Value));

            return GetViewById(id);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            return Save(product);
        }

        #endregion [ Edit ]

        #region [ Delete ]

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            return GetViewById(id);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                var product = productService.Delete(id);

                TempData["Message"] = "Product	" +
                    product.Name.ToUpper() +
                    " was removed";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion [ Delete ]

        #endregion [ Actions ]

        #region [ Methods ]

        private ActionResult GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product produto = productService.ById(id.Value);

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        private void PopulateViewBag(Product product = null)
        {
            var categories = categoryService.GetOrderedByName();
            var suppliers = supplierService.GetOrderedByName();

            long categoryId = -1;
            long supplierId = -1;

            if (product != null)
            {
                categoryId = product.CategoryId.Value;
                supplierId = product.SupplierId.Value;
            }

            ViewBag.CategoryId = new SelectList(
                categories,
                "CategoryId",
                "Name",
                categoryId);
            ViewBag.SupplierId = new SelectList(suppliers, "SupplierId", "Name", supplierId);
        }

        private ActionResult Save(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Save(product);
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        #endregion [ Methods ]

    }
}