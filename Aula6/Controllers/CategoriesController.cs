using Aula6.Models;
using Model.Tables;
using Newtonsoft.Json;
using Service.Tables;
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
    public class CategoriesController : Controller
    {

        private CategoryService service = new CategoryService();

        #region [ Actions ]

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var apiModel = new CategoryListAPIModel();

            var resp = await GetFromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    // { Message: "OK", Result: [{},{}]}
                    string result = response.Content
                    .ReadAsStringAsync().Result;
                    apiModel = JsonConvert
                    .DeserializeObject<CategoryListAPIModel>(result);
                }
            });

            return View(apiModel.Result);
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            return await GetViewById(id);
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
        public ActionResult Create(Category item)
        {
            return Save(item);
        }

        #endregion [ Create ]

        #region [ Edit ]

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            return await GetViewById(id);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category item)
        {
            return Save(item);
        }

        #endregion [ Edit ]

        #region [ Delete ]

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            return await GetViewById(id);
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
                    "Category {0} was removed.",
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

        private async Task<ActionResult> GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CategoryAPIModel item = null;
            var resp = await GetFromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content
                    .ReadAsStringAsync().Result;
                    item = JsonConvert
                    .DeserializeObject<CategoryAPIModel>(result);
                }
            }, id.Value);

            if (!resp.IsSuccessStatusCode)
                return new HttpStatusCodeResult(resp.StatusCode);

            if (item.Message == "!OK" ||
                item.Result == null) return HttpNotFound();

            return View(item.Result);
        }

        private async Task<HttpResponseMessage> GetFromAPI(
            Action<HttpResponseMessage> action,
            long? id = null)
        {
            using (var client = new HttpClient())
            {
                var reqUrl = HttpContext.Request.Url;
                var baseUrl = string.Format("{0}://{1}",
                    reqUrl.Scheme, reqUrl.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories";
                if (id != null) url += "/" + id;

                var request = await client.GetAsync(url);
                //HttpContent content = new HttpContent();
                ////content.
                //var r = client.PostAsync(url, content);

                if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        private ActionResult Save(Category item)
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