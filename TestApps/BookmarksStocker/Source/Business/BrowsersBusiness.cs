using BookmarksStocker.Source;
using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Entity;
using BookmarksStocker.Source.ViewModel;
using Simply.Common;
using Simply.Crud;
using Simply.Data;
using SimpleInfra.Common.Response;
using SimpleInfra.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrowsersStocker.Source.Business
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

                using (var context = GetDbConnection())
                {
                    var entity = Map<BrowsersViewModel, Browsers>(model);
                    var returnValue = context.InsertAndGetId(entity);
                    model.Id = returnValue.ToInt();
                    return new SimpleResponse<BrowsersViewModel>
                    {
                        Data = model,
                        ResponseCode = 1,
                        ResponseMessage = "Kay�t ba�ar�yla eklendi."
                    };
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Ekleme i�leminde hata olu�tu.";
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
                using (var context = GetDbConnection())
                {
                    var entity = context.FirstOrDefault<Browsers>(q => q.Id == id);

                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kay�t bulunamad�.";
                        return response;
                    }

                    response.Data = Map<Browsers, BrowsersViewModel>(entity);
                    response.ResponseCode = 1;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma i�leminde hata olu�tu.";
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

                using (var context = GetDbConnection())
                {
                    var entity = context.Select<Browsers>(q => q.Id == model.Id).FirstOrDefault();
                    if (entity == null || entity == default(Browsers))
                    {
                        response.ResponseCode = BusinessResponseValues.NullEntityValue;
                        response.ResponseMessage = "Kay�t bulunamad�.";
                        return response;
                    }

                    MapTo(model, entity);
                    // context.Browsers.Attach(entity); context.Entry(entity).State = EntityState.Modified;
                    response.ResponseCode = context.Update(entity);
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "G�ncelleme i�leminde hata olu�tu.";
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
                using (var context = GetDbConnection())
                {
                    try
                    {
                        context.OpenIfNot();
                        response.ResponseCode = context.DeleteByIdList<Browsers>(idValues: new object[] { model.Id });
                    }
                    finally
                    { context.CloseIfNot(); }
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme i�leminde hata olu�tu.";
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
                using (var context = GetDbConnection())
                {
                    response.ResponseCode = context.DeleteByIdList<Browsers>(idValues: new object[] { id });
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Silme i�leminde hata olu�tu.";
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
                using (var context = GetDbConnection())
                {
                    var entities = context.GetAll<Browsers>();
                    response.Data = MapList<Browsers, BrowsersViewModel>(entities);
                }

                response.ResponseCode = response.Data?.Count ?? 0;
            }
            catch (Exception ex)
            {
                response.ResponseCode = BusinessResponseValues.InternalError;
                response.ResponseMessage = "Okuma i�leminde hata olu�tu.";
                DayLogger.Error(ex);
            }

            response.Data = response.Data ?? new List<BrowsersViewModel>();
            return response;
        }
    }
}