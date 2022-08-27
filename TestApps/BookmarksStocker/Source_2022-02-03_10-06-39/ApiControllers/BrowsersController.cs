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
    public class BrowsersController : ApiController
    {
        private IBrowsersBusiness iBrowsersBusiness;

        public BrowsersController(IBrowsersBusiness iBrowsersBusiness = null)
        {
            this.iBrowsersBusiness = iBrowsersBusiness ?? 
                GsbIoC.Instance.GetInstance<IBrowsersBusiness>();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreatePost(BrowsersViewModel model)
        {
            var response = new SimpleResponse<BrowsersViewModel>();
            response = iBrowsersBusiness.Create(model);
            return Json(response);
        }

        [HttpGet]
        [Route("detail")]
        public IHttpActionResult Detail(int id)
        {
            var modelResult = iBrowsersBusiness.Read(id);
            return Json(modelResult);
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdatePost(BrowsersViewModel model)
        {
            var response = new SimpleResponse();
            response = iBrowsersBusiness.Update(model);
            return Json(response);
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            var result = iBrowsersBusiness.Delete(id);
            return Json(result);
        }

        [HttpGet]
        [Route("readall")]
        public IHttpActionResult ReadAll()
        {
            var modelResult = iBrowsersBusiness.ReadAll();
            return Json(modelResult);
        }
    }
}