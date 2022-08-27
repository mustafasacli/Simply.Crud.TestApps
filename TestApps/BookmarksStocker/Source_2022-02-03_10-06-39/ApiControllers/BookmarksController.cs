using Bookmarks.Project.ViewModel;
using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.Dtos;
using SimpleInfra.Common.Response;
using SimpleInfra.Common.Core;
using Gsb.IoC;
using SimpleInfra.Validation;
using SimpleInfra.Mapping;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Bookmarks.Project.Web.Controllers
{
    public class BookmarksController : ApiController
    {
        private IBookmarksBusiness iBookmarksBusiness;

        public BookmarksController(IBookmarksBusiness iBookmarksBusiness = null)
        {
            this.iBookmarksBusiness = iBookmarksBusiness ?? 
                GsbIoC.Instance.GetInstance<IBookmarksBusiness>();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreatePost(BookmarksViewModel model)
        {
            var response = new SimpleResponse<BookmarksViewModel>();
            response = iBookmarksBusiness.Create(model);
            return Json(response);
        }

        [HttpGet]
        [Route("detail")]
        public IHttpActionResult Detail(long id)
        {
            var modelResult = iBookmarksBusiness.Read(id);
            return Json(modelResult);
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdatePost(BookmarksViewModel model)
        {
            var response = new SimpleResponse();
            response = iBookmarksBusiness.Update(model);
            return Json(response);
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(long id)
        {
            var result = iBookmarksBusiness.Delete(id);
            return Json(result);
        }

        [HttpGet]
        [Route("readall")]
        public IHttpActionResult ReadAll()
        {
            var modelResult = iBookmarksBusiness.ReadAll();
            return Json(modelResult);
        }
    }
}