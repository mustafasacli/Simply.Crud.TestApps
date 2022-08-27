using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Entity;
using BookmarksStocker.Source.ViewModel;
using Simply.Common;
using Simply.Crud;
using Simply.Crud.Constants;
using Simply.Data;
using SimpleInfra.Common.Response;
using SimpleInfra.Validation;
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

                using (var context = GetDbConnection())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;
                        var entity = Map<BookmarksViewModel, Bookmarks>(model);
                        context.OpenIfNot();
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
                    { context.CloseIfNot(); }
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
                using (var context = GetDbConnection())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;
                        context.OpenIfNot();
                        var entity = context.FirstOrDefault<Bookmarks>(q => q.Id == id);
                        // context.CloseIfNot();

                        if (entity == null || entity == default(Bookmarks))
                        {
                            response.ResponseCode = BusinessResponseValues.NullEntityValue;
                            response.ResponseMessage = "Kayýt bulunamadý.";
                            return response;
                        }

                        response.Data = Map<Bookmarks, BookmarksViewModel>(entity);
                        response.ResponseCode = 1;
                    }
                    finally
                    { context.CloseIfNot(); }
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

                using (var context = GetDbConnection())
                {
                    try
                    {
                        context.OpenIfNot();
                        var entity = context.Select<Bookmarks>(q => q.Id == model.Id).FirstOrDefault();
                        if (entity == null || entity == default(Bookmarks))
                        {
                            response.ResponseCode = BusinessResponseValues.NullEntityValue;
                            response.ResponseMessage = "Kayýt bulunamadý.";
                            return response;
                        }

                        MapTo(model, entity);
                        entity.UpdateTime = DateTime.Now;
                        // context.Bookmarks.Attach(entity);
                        // context.Entry(entity).State = EntityState.Modified;

                        response.ResponseCode = context.Update(entity);
                        // context.CloseIfNot();
                    }
                    finally
                    { context.CloseIfNot(); }
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
                using (var context = GetDbConnection())
                {
                    try
                    {
                        context.OpenIfNot();
                        response.ResponseCode = context.DeleteByIdList<Bookmarks>(idValues: new object[] { model.Id });
                        // context.CloseIfNot();
                    }
                    finally
                    { context.CloseIfNot(); }
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
                using (var context = GetDbConnection())
                {
                    try
                    {
                        //context.OpenIfNot();
                        response.ResponseCode = context.OpenAnd()
                            .DeleteByIdList<Bookmarks>(idValues: new object[] { id });
                        // context.CloseIfNot();
                    }
                    finally
                    { context.CloseIfNot(); }
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
                using (var context = GetDbConnection())
                {
                    try
                    {
                        DbCrudSettings.LogQuery = true;
                        context.OpenIfNot();
                        var entities = context.GetAll<Bookmarks>();
                        // context.CloseIfNot();
                        response.Data = MapList<Bookmarks, BookmarksViewModel>(entities);
                    }
                    finally
                    { context.CloseIfNot(); }
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

        public bool ExistByName(BookmarksViewModel viewModel)
        {
            bool result = false;

            if (viewModel == null)
                return result;

            using (var context = GetDbConnection())
            {
                context.OpenIfNot();
                result = context.Any<Bookmarks>(q => q.Name == viewModel.Name && q.Id != viewModel.Id);
                context.CloseIfNot();
            }

            return result;
        }

        public bool ExistByUrl(BookmarksViewModel viewModel)
        {
            bool result = false;

            if (viewModel == null)
                return result;

            using (var context = GetDbConnection())
            {
                // context.OpenIfNot();
                result = context.Any<Bookmarks>(q => q.Url == viewModel.Url && q.Id != viewModel.Id);
                context.CloseIfNot();
            }

            return result;
        }
    }
}