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
    public class BookmarksController : ControllerBase
    {
        private IBookmarksBusiness iBookmarksBusiness;
        private readonly ILogger<BookmarksController> _logger;

        public BookmarksController(ILogger<BookmarksController> logger,
                    IBookmarksBusiness iBookmarksBusiness)
        {
            this._logger = logger;
            this.iBookmarksBusiness = iBookmarksBusiness;
        }


        [HttpPost]
        [Route("create")]
        public SimpleResponse<BookmarksViewModel> Create(BookmarksViewModel model)
        {
            var response = iBookmarksBusiness.Create(model);
            return response;
        }

        [HttpGet]
        [Route("detail")]
        public SimpleResponse<BookmarksViewModel> Detail(long id)
        {
            var response = iBookmarksBusiness.Read(id);
            return response;
        }

        [HttpPost]
        [Route("update")]
        public SimpleResponse Update(BookmarksViewModel model)
        {
            var response = iBookmarksBusiness.Update(model);
            return response;
        }

        [HttpPost]
        [Route("delete")]
        public SimpleResponse Delete(long id)
        {
            var result = iBookmarksBusiness.Delete(id);
            return result;
        }

        [HttpGet]
        public SimpleResponse<List<BookmarksViewModel>> ReadAll()
        {
            var response = iBookmarksBusiness.ReadAll();
            return response;
        }
    }
}