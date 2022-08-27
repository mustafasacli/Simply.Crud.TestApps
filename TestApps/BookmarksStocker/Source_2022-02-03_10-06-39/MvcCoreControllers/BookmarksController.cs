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
    public class BookmarksController : ControllerBase
    {
        private IBookmarksBusiness iBookmarksBusiness;

        public BookmarksController(IBookmarksBusiness iBookmarksBusiness)
        {
            this.iBookmarksBusiness = iBookmarksBusiness;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var response = iBookmarksBusiness.ReadAll();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            var model = new BookmarksViewModel();
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(BookmarksViewModel model)
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
        public IActionResult Detail(long id)
        {
            var response = iBookmarksBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(response.Data, JsonRequestBehavior.AllowGet);
        }
        public IActionResult  Edit(long id)
        {
            var response = iBookmarksBusiness.Read(id);

            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Edit", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(BookmarksViewModel model)
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

        public IActionResult Delete(long id)
        {
            var response = iBookmarksBusiness.Read(id);
            if (response.Data == null || response.ResponseCode < 1)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Delete", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(long id)
        {
            var response = iBookmarksBusiness.Delete(id);

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
            var response = iBookmarksBusiness.ReadAll();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}