using Bookmarks.Project.Dtos;
using SimpleInfra.Common.Response;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bookmarks.Project.WcfService.Interfaces
{
    [ServiceContract(Namespace = "http://127.0.0.1:8081/BrowsersService")]
    public interface IBrowsersService
    {
        [OperationContract]
        SimpleResponse<BrowsersDto> Create(BrowsersDto dto);

        [OperationContract]
        SimpleResponse<BrowsersDto> Read(int id);

        [OperationContract]
        SimpleResponse Update(BrowsersDto dto);

        [OperationContract]
        SimpleResponse Delete(BrowsersDto dto);

        [OperationContract]
        SimpleResponse Delete(int id);

        [OperationContract]
        SimpleResponse<List<BrowsersDto>> ReadAll();
    }
}