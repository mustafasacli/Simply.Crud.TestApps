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
    public class BookmarksController : OzelYurtBaseController
    {
        private IBookmarksBusiness iBookmarksBusiness;

        public BookmarksController(IBookmarksBusiness iBookmarksBusiness = null)
        {
            this.iBookmarksBusiness = iBookmarksBusiness ??
                GsbIoC.Instance.GetInstance<IBookmarksBusiness>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var response = iBookmarksBusiness.ReadAll();
            return View(response.Data);
        }

        public ActionResult Create()
        {
            var model = new BookmarksViewModel();
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult CreatePost(BookmarksViewModel model)
        {
            var response = iBookmarksBusiness.Create(model);

            if (response.ResponseCode > 0)
            { return RedirectToAction("Index"); }
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return View("Create", model);
            }
        }

        [HttpGet]
        public ActionResult Detail(long id)
        {
            var response = iBookmarksBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(response.Data);
        }

        public ActionResult Edit(long id)
        {
            var response = iBookmarksBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Edit",  response.Data);
        }

        [HttpPost]
        public ActionResult UpdatePost(BookmarksViewModel model)
        {
            var response = iBookmarksBusiness.Update(model);

            if (response.ResponseCode > 0)
            { return RedirectToAction("Index"); }
            else
            {
                ModelState.AddModelError(string.Empty, response.ResponseMessage);
                return View("Edit", model);
            }
        }

        public ActionResult Delete(long id)
        {
            var response = iBookmarksBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Delete", response.Data);
        }

        [HttpPost]
        public ActionResult DeletePost(long id)
        {
            var response = iBookmarksBusiness.Delete(id);

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
            var response = iBookmarksBusiness.ReadAll();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}