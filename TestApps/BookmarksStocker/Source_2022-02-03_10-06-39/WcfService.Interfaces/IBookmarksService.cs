using Bookmarks.Project.Dtos;
using SimpleInfra.Common.Response;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bookmarks.Project.WcfService.Interfaces
{
    [ServiceContract(Namespace = "http://127.0.0.1:8081/BookmarksService")]
    public interface IBookmarksService
    {
        [OperationContract]
        SimpleResponse<BookmarksDto> Create(BookmarksDto dto);

        [OperationContract]
        SimpleResponse<BookmarksDto> Read(long id);

        [OperationContract]
        SimpleResponse Update(BookmarksDto dto);

        [OperationContract]
        SimpleResponse Delete(BookmarksDto dto);

        [OperationContract]
        SimpleResponse Delete(long id);

        [OperationContract]
        SimpleResponse<List<BookmarksDto>> ReadAll();
    }
}