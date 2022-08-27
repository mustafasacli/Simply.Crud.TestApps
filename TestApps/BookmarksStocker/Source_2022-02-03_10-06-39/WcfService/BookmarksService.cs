using Bookmarks.Project.Dtos;
using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.WcfService.Interfaces;
using Bookmarks.Project.ViewModel;
using SimpleFileLogging;
using SimpleInfra.Business.Core;
using SimpleInfra.Common.Core;
using SimpleInfra.IoC;
using SimpleInfra.Common.Response;
using SimpleInfra.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bookmarks.Project.WcfService
{
    public class BookmarksService : IBookmarksService
    {
        private IBookmarksBusiness iBookmarksBusiness;

        public BookmarksService()
        {
            this.iBookmarksBusiness =
                SimpleIoC.Instance.GetInstance<IBookmarksBusiness>();
        }

        public SimpleResponse<BookmarksDto> Create(BookmarksDto dto)
        {
            var response = new SimpleResponse<BookmarksDto>();

            try
            {
                var model = SimpleMapper.Map<BookmarksDto, BookmarksViewModel>(dto);
                var resp = iBookmarksBusiness.Create(model);
                response = new SimpleResponse<BookmarksDto>()
                {
                    ResponseCode = resp.ResponseCode,
                    ResponseMessage = resp.ResponseMessage,
                    RCode = resp.RCode
                };

                response.Data = SimpleMapper.Map<BookmarksViewModel, BookmarksDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse<BookmarksDto> Read(long id)
        {
            var response = new SimpleResponse<BookmarksDto>();

            try
            {
                var resp  = iBookmarksBusiness.Read(id);
                var isNullOrDef = resp.Data == null || resp.Data == default(BookmarksViewModel);
                response.ResponseCode = isNullOrDef ? BusinessResponseValues.NullEntityValue : 1;
                response.RCode = resp.RCode;
                response.ResponseMessage = resp.ResponseMessage;
                if(!isNullOrDef)
                    response.Data = SimpleMapper.Map<BookmarksViewModel, BookmarksDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Update(BookmarksDto dto)
        {
            var response = new SimpleResponse();

            try
            {
                var model = SimpleMapper.Map<BookmarksDto, BookmarksViewModel>(dto);
                response = iBookmarksBusiness.Update(model);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Güncelleme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Delete(BookmarksDto dto)
        {
            var response = new SimpleResponse();

            try
            {
                var model = SimpleMapper.Map<BookmarksDto, BookmarksViewModel>(dto);
                response = iBookmarksBusiness.Delete(model);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Delete(long id)
        {
            var response = new SimpleResponse();

            try
            {
                response =  iBookmarksBusiness.Delete(id);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse<List<BookmarksDto>> ReadAll()
        {
            var response = new SimpleResponse<List<BookmarksDto>>();

            try
            {
                var resp = iBookmarksBusiness.ReadAll();

                response.ResponseCode = resp.ResponseCode;
                response.ResponseMessage = resp.ResponseMessage;
                response.RCode = resp.RCode;
                response.Data = SimpleMapper.MapList<BookmarksViewModel, BookmarksDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            response.Data = response.Data ?? new List<BookmarksDto>();
            return response;
        }
    }
}