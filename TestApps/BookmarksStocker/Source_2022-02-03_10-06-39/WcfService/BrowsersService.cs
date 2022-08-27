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
    public class BrowsersService : IBrowsersService
    {
        private IBrowsersBusiness iBrowsersBusiness;

        public BrowsersService()
        {
            this.iBrowsersBusiness =
                SimpleIoC.Instance.GetInstance<IBrowsersBusiness>();
        }

        public SimpleResponse<BrowsersDto> Create(BrowsersDto dto)
        {
            var response = new SimpleResponse<BrowsersDto>();

            try
            {
                var model = SimpleMapper.Map<BrowsersDto, BrowsersViewModel>(dto);
                var resp = iBrowsersBusiness.Create(model);
                response = new SimpleResponse<BrowsersDto>()
                {
                    ResponseCode = resp.ResponseCode,
                    ResponseMessage = resp.ResponseMessage,
                    RCode = resp.RCode
                };

                response.Data = SimpleMapper.Map<BrowsersViewModel, BrowsersDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse<BrowsersDto> Read(int id)
        {
            var response = new SimpleResponse<BrowsersDto>();

            try
            {
                var resp  = iBrowsersBusiness.Read(id);
                var isNullOrDef = resp.Data == null || resp.Data == default(BrowsersViewModel);
                response.ResponseCode = isNullOrDef ? BusinessResponseValues.NullEntityValue : 1;
                response.RCode = resp.RCode;
                response.ResponseMessage = resp.ResponseMessage;
                if(!isNullOrDef)
                    response.Data = SimpleMapper.Map<BrowsersViewModel, BrowsersDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Update(BrowsersDto dto)
        {
            var response = new SimpleResponse();

            try
            {
                var model = SimpleMapper.Map<BrowsersDto, BrowsersViewModel>(dto);
                response = iBrowsersBusiness.Update(model);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Güncelleme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Delete(BrowsersDto dto)
        {
            var response = new SimpleResponse();

            try
            {
                var model = SimpleMapper.Map<BrowsersDto, BrowsersViewModel>(dto);
                response = iBrowsersBusiness.Delete(model);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse Delete(int id)
        {
            var response = new SimpleResponse();

            try
            {
                response =  iBrowsersBusiness.Delete(id);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            return response;
        }

        public SimpleResponse<List<BrowsersDto>> ReadAll()
        {
            var response = new SimpleResponse<List<BrowsersDto>>();

            try
            {
                var resp = iBrowsersBusiness.ReadAll();

                response.ResponseCode = resp.ResponseCode;
                response.ResponseMessage = resp.ResponseMessage;
                response.RCode = resp.RCode;
                response.Data = SimpleMapper.MapList<BrowsersViewModel, BrowsersDto>(resp.Data);
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                SimpleFileLogger.Instance.Error(ex);
            }

            response.Data = response.Data ?? new List<BrowsersDto>();
            return response;
        }
    }
}