using Model.Registers;
using Newtonsoft.Json;
using Service.Registers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aula6.Controllers
{
    public class SuppliersController : Controller
    {
        private SupplierService service = new SupplierService();
 
        #region [ Actions ]

        // GET: Categories
        public async Task<ActionResult> Index()
        {

            var list = new List<Supplier>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);

                client.DefaultRequestHeaders.Clear();

                var response = await client.GetAsync("Api/Categoreis");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content
                        .ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Supplier>>(result);

                }
            }

            return View(list);
        }

        // GET: Categories/Details/5
        public ActionResult Details(long? id)
        {
            return GetViewById(id);
        }

        #region [ Create ]

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier item)
        {
            return Save(item);
        }

        #endregion [ Create ]

        #region [ Edit ]

        // GET: Categories/Edit/5
        public ActionResult Edit(long? id)
        {
            return GetViewById(id);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier item)
        {
            return Save(item);
        }

        #endregion [ Edit ]

        #region [ Delete ]

        // GET: Categories/Delete/5
        public ActionResult Delete(long? id)
        {
            return GetViewById(id);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                var item = service.Delete(id);

                TempData["Message"] = string.Format(
                    "Supplier {0} was removed.",
                    item.Name.ToUpper());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion [ Edit ]

        #endregion [ Actions ]

        #region [ Methods ]

        private ActionResult GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = service.ById(id.Value);

            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        private ActionResult Save(Supplier item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Save(item);
                    return RedirectToAction("Index");
                }
                return View(item);
            }
            catch
            {
                return View(item);
            }
        }

        #endregion [ Methods ]

    }
}