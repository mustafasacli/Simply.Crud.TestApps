using Bookmarks.Project.Business.Interfaces;
using Bookmarks.Project.Context;
using Bookmarks.Project.Entity;
using Bookmarks.Project.ViewModel;
using SimpleFileLogging;
using SimpleInfra.Common.Core;
using SimpleInfra.Common.Response;
using SimpleInfra.Business.Core;
using SimpleInfra.Mapping;
using SimpleInfra.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bookmarks.Project.Business
{
    /// <summary>
    /// Defines the <see cref="BookmarksBusiness"/>.
    /// </summary>
    public class BookmarksBusiness : SimpleBaseBusiness, IBookmarksBusiness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarksBusiness"/> class.
        /// </summary>
        public BookmarksBusiness()
        {
        }

        /// <summary>
        /// The Creates entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse{BookmarksViewModel}"/>.</returns>
        public SimpleResponse<BookmarksViewModel> Create(BookmarksViewModel model)
        {
            var response = new SimpleResponse<BookmarksViewModel>();

            try
            {
                var validation = model.Validate();
                if (validation.HasError)
                {
                    return new SimpleResponse<BookmarksViewModel>
                    {
                        Data = model,
                        ResponseCode = BusinessResponseValues.ValidationErrorResult,
                        ResponseMessage = validation.AllValidationMessages
                    };
                }

                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = Map<BookmarksViewModel, Bookmarks>(model);
                    context.Bookmarks.Add(entity);
                    response.ResponseCode = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Ekleme iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            return response;
        }

        /// <summary>
        /// The Reads for BookmarksViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{BookmarksViewModel}"/>.</returns>
        public SimpleResponse<BookmarksViewModel> Read(long id)
        {
            var response = new SimpleResponse<BookmarksViewModel>();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Bookmarks
                        .AsNoTracking()
                        .SingleOrDefault(q => q.Id == id);

                    if (entity == null || entity == default(Bookmarks))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    response.Data = Map<Bookmarks, BookmarksViewModel>(entity);
                    response.ResponseCode = 1;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            return response;
        }

        /// <summary>
        /// Updates entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Update(BookmarksViewModel model)
        {
            var response = new SimpleResponse();

            try
            {
                var validation = model.Validate();
                if (validation.HasError)
                {
                    return new SimpleResponse
                    {
                        ResponseCode = BusinessResponseValues.ValidationErrorResult,
                        ResponseMessage = validation.AllValidationMessages
                    };
                }

                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Bookmarks.SingleOrDefault(q => q.Id == model.Id);
                    if (entity == null || entity == default(Bookmarks))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    MapTo(model, entity);
                    // context.Bookmarks.Attach(entity);
                    // context.Entry(entity).State = EntityState.Modified;
                    response.ResponseCode = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Güncelleme iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            return response;
        }

        /// <summary>
        /// Deletes entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Delete(BookmarksViewModel model)
        {
            var response = new SimpleResponse();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Bookmarks.FirstOrDefault(q => q.Id == model.Id);
                    if (entity == null || entity == default(Bookmarks))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    context.Bookmarks.Remove(entity);
                    response.ResponseCode = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            return response;
        }

        /// <summary>
        /// Deletes record by given id value(s).
        /// </summary>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Delete(long id)
        {
            var response = new SimpleResponse();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Bookmarks.SingleOrDefault(q => q.Id == id);
                    if (entity == null || entity == default(Bookmarks))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    context.Bookmarks.Remove(entity);
                    response.ResponseCode = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            return response;
        }

        /// <summary>
        /// Reads All records for BookmarksViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{List{BookmarksViewModel}}"/>.</returns>
        public SimpleResponse<List<BookmarksViewModel>> ReadAll()
        {
            var response = new SimpleResponse<List<BookmarksViewModel>>();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entities = context.Bookmarks
                        .AsNoTracking()
                        .ToList() ?? new List<Bookmarks>();

                    response.Data = MapList<Bookmarks, BookmarksViewModel>(entities);
                    response.ResponseCode = response.Data.Count;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            response.Data = response.Data ?? new List<BookmarksViewModel>();
            return response;
        }
    }
}