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
    /// Defines the <see cref="BrowsersBusiness"/>.
    /// </summary>
    public class BrowsersBusiness : SimpleBaseBusiness, IBrowsersBusiness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsersBusiness"/> class.
        /// </summary>
        public BrowsersBusiness()
        {
        }

        /// <summary>
        /// The Creates entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse{BrowsersViewModel}"/>.</returns>
        public SimpleResponse<BrowsersViewModel> Create(BrowsersViewModel model)
        {
            var response = new SimpleResponse<BrowsersViewModel>();

            try
            {
                var validation = model.Validate();
                if (validation.HasError)
                {
                    return new SimpleResponse<BrowsersViewModel>
                    {
                        Data = model,
                        ResponseCode = BusinessResponseValues.ValidationErrorResult,
                        ResponseMessage = validation.AllValidationMessages
                    };
                }

                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = Map<BrowsersViewModel, Browsers>(model);
                    context.Browsers.Add(entity);
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
        /// The Reads for BrowsersViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{BrowsersViewModel}"/>.</returns>
        public SimpleResponse<BrowsersViewModel> Read(int id)
        {
            var response = new SimpleResponse<BrowsersViewModel>();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Browsers
                        .AsNoTracking()
                        .SingleOrDefault(q => q.Id == id);

                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    response.Data = Map<Browsers, BrowsersViewModel>(entity);
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
        /// Updates entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Update(BrowsersViewModel model)
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
                    var entity = context.Browsers.SingleOrDefault(q => q.Id == model.Id);
                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    MapTo(model, entity);
                    // context.Browsers.Attach(entity);
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
        /// Deletes entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        public SimpleResponse Delete(BrowsersViewModel model)
        {
            var response = new SimpleResponse();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Browsers.FirstOrDefault(q => q.Id == model.Id);
                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    context.Browsers.Remove(entity);
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
        public SimpleResponse Delete(int id)
        {
            var response = new SimpleResponse();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entity = context.Browsers.SingleOrDefault(q => q.Id == id);
                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kayýt bulunamadý.";
                        return response;
                    }

                    context.Browsers.Remove(entity);
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
        /// Reads All records for BrowsersViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{List{BrowsersViewModel}}"/>.</returns>
        public SimpleResponse<List<BrowsersViewModel>> ReadAll()
        {
            var response = new SimpleResponse<List<BrowsersViewModel>>();

            try
            {
                using (var context = new BookmarksProjectDbContext())
                {
                    var entities = context.Browsers
                        .AsNoTracking()
                        .ToList() ?? new List<Browsers>();

                    response.Data = MapList<Browsers, BrowsersViewModel>(entities);
                    response.ResponseCode = response.Data.Count;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma iþleminde hata oluþtu.";
                DayLogger.Error(ex);
            }

            response.Data = response.Data ?? new List<BrowsersViewModel>();
            return response;
        }
    }
}