using BookmarksStocker.DAO;
using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Entity;
using BookmarksStocker.Source.ViewModel;
using SimpleInfra.Common.Response;
using SimpleInfra.Validation;
using Simply.Common;
using Simply.Crud;
using Simply.Crud.Constants;
using Simply.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookmarksStocker.Source.Business
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
        /// <param name="model">The viewModel <see cref="BookmarksViewModel"/>.</param>
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

                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;
                        var entity = Map<BookmarksViewModel, Bookmarks>(model);

                        var returnValue = context.InsertAndGetId(entity);
                        model.Id = returnValue.ToLong();
                        return new SimpleResponse<BookmarksViewModel>
                        {
                            Data = model,
                            ResponseCode = 1,
                            ResponseMessage = "Kayýt baþarýyla eklendi."
                        };
                    }
                    finally
                    { context.Close(); }
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
                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;
                        var entity = context.FirstOrDefault<Bookmarks>(q => q.Id == id);
                        if (entity == null)
                        {
                            response.ResponseCode = BusinessResponseValues.NullEntityValue;
                            response.ResponseMessage = "Kayýt bulunamadý.";
                            return response;
                        }

                        response.Data = Map<Bookmarks, BookmarksViewModel>(entity);
                        response.ResponseCode = 1;
                    }
                    finally
                    { context.Close(); }
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
        /// <param name="model">The viewModel <see cref="BookmarksViewModel"/>.</param>
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

                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        var entity = context.Select<Bookmarks>(q => q.Id == model.Id).FirstOrDefault();
                        if (entity == null)
                        {
                            response.ResponseCode = BusinessResponseValues.NullEntityValue;
                            response.ResponseMessage = "Kayýt bulunamadý.";
                            return response;
                        }

                        MapTo(model, entity);
                        entity.UpdateTime = DateTime.Now;
                        response.ResponseCode = context.Update(entity);
                    }
                    finally
                    { context.Close(); }
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
        /// <param name="model">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Delete(BookmarksViewModel model)
        {
            var response = new SimpleResponse();

            try
            {
                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        response.ResponseCode = context.DeleteAll<Bookmarks>(q => q.Id == model.Id);
                    }
                    finally
                    { context.Close(); }
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
                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        response.ResponseCode = context.DeleteAll<Bookmarks>(q => q.Id == id);
                    }
                    finally
                    { context.Close(); }
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
                using (ISimpleDatabase context = new SimpleBookmarkDatabase())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;

                        var entities = context.GetAll<Bookmarks>();
                        response.Data = MapList<Bookmarks, BookmarksViewModel>(entities);
                    }
                    finally
                    { context.Close(); }
                }

                response.ResponseCode = response.Data?.Count ?? 0;
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

        /// <summary>
        /// Exists the by name.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>A bool.</returns>
        public bool ExistByName(BookmarksViewModel viewModel)
        {
            bool result = false;

            if (viewModel == null)
                return result;

            using (ISimpleDatabase context = new SimpleBookmarkDatabase())
            {
                result = context.Any<Bookmarks>(q => q.Name == viewModel.Name && q.Id != viewModel.Id);
            }

            return result;
        }

        /// <summary>
        /// Exists the by url.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>A bool.</returns>
        public bool ExistByUrl(BookmarksViewModel viewModel)
        {
            bool result = false;

            if (viewModel == null)
                return result;

            using (ISimpleDatabase context = new SimpleBookmarkDatabase())
            {
                result = context.Any<Bookmarks>(q => q.Url == viewModel.Url && q.Id != viewModel.Id);
            }

            return result;
        }
    }
}