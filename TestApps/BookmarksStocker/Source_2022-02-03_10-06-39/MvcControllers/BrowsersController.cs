using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.ViewModel;
using SimpleInfra.Common.Core;
using Gsb.IoC;
using SimpleInfra.Common.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Bookmarks.Project.Web.Controllers
{
    public class BrowsersController : OzelYurtBaseController
    {
        private IBrowsersBusiness iBrowsersBusiness;

        public BrowsersController(IBrowsersBusiness iBrowsersBusiness = null)
        {
            this.iBrowsersBusiness = iBrowsersBusiness ??
                GsbIoC.Instance.GetInstance<IBrowsersBusiness>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var response = iBrowsersBusiness.ReadAll();
            return View(response.Data);
        }

        public ActionResult Create()
        {
            var model = new BrowsersViewModel();
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult CreatePost(BrowsersViewModel model)
        {
            var response = iBrowsersBusiness.Create(model);

            if (response.ResponseCode > 0)
            { return RedirectToAction("Index"); }
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return View("Create", model);
            }
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var response = iBrowsersBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(response.Data);
        }

        public ActionResult Edit(int id)
        {
            var response = iBrowsersBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Edit",  response.Data);
        }

        [HttpPost]
        public ActionResult UpdatePost(BrowsersViewModel model)
        {
            var response = iBrowsersBusiness.Update(model);

            if (response.ResponseCode > 0)
            { return RedirectToAction("Index"); }
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return View("Edit", model);
            }
        }

        public ActionResult Delete(int id)
        {
            var response = iBrowsersBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Delete", response.Data);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var response = iBrowsersBusiness.Delete(id);

            if (response.ResponseCode > 0)
            { return RedirectToAction("Index"); }
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return RedirectToAction("Delete", new { id });
            }
        }

        [HttpGet]
        public ActionResult ReadAll()
        {
            var response = iBrowsersBusiness.ReadAll();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}