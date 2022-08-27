using Gsb.Common.Core;
using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.Dtos;
using Bookmarks.Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleInfra.Common.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmarks.Project.CoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrowsersController : ControllerBase
    {
        private IBrowsersBusiness iBrowsersBusiness;
        private readonly ILogger<BrowsersController> _logger;

        public BrowsersController(ILogger<BrowsersController> logger,
                    IBrowsersBusiness iBrowsersBusiness)
        {
            this._logger = logger;
            this.iBrowsersBusiness = iBrowsersBusiness;
        }


        [HttpPost]
        [Route("create")]
        public SimpleResponse<BrowsersViewModel> Create(BrowsersViewModel model)
        {
            var response = iBrowsersBusiness.Create(model);
            return response;
        }

        [HttpGet]
        [Route("detail")]
        public SimpleResponse<BrowsersViewModel> Detail(int id)
        {
            var response = iBrowsersBusiness.Read(id);
            return response;
        }

        [HttpPost]
        [Route("update")]
        public SimpleResponse Update(BrowsersViewModel model)
        {
            var response = iBrowsersBusiness.Update(model);
            return response;
        }

        [HttpPost]
        [Route("delete")]
        public SimpleResponse Delete(int id)
        {
            var result = iBrowsersBusiness.Delete(id);
            return result;
        }

        [HttpGet]
        public SimpleResponse<List<BrowsersViewModel>> ReadAll()
        {
            var response = iBrowsersBusiness.ReadAll();
            return response;
        }
    }
}