using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.Dtos;
using Bookmarks.Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using SimpleInfra.Common.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bookmarks.Project.Web.Controllers
{
    public class BrowsersController : ControllerBase
    {
        private IBrowsersBusiness iBrowsersBusiness;

        public BrowsersController(IBrowsersBusiness iBrowsersBusiness)
        {
            this.iBrowsersBusiness = iBrowsersBusiness;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = iBrowsersBusiness.ReadAll();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            var model = new BrowsersViewModel();
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(BrowsersViewModel model)
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
        public IActionResult Detail(int id)
        {
            var response = iBrowsersBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(response.Data, JsonRequestBehavior.AllowGet);
        }
        public IActionResult  Edit(int id)
        {
            var response = iBrowsersBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Edit", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(BrowsersViewModel model)
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

        public IActionResult Delete(int id)
        {
            var response = iBrowsersBusiness.Read(id);
            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Delete", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var response = iBrowsersBusiness.Delete(id);

            if (response.ResponseCode > 0)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return RedirectToAction("Delete", new { id });
            }
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            var response = iBrowsersBusiness.ReadAll();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}