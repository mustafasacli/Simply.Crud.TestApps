using Bookmarks.Project.Dtos;
using Bookmarks.Project.ViewModel;
using SimpleInfra.Common.Response;
using System.Collections.Generic;

namespace Bookmarks.Project.BusinessCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IBrowsersBusiness"/>.
    /// </summary>
    public interface IBrowsersBusiness
    {
        /// <summary>
        /// The Creates entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse{BrowsersViewModel}"/>.</returns>
        SimpleResponse<BrowsersViewModel> Create(BrowsersViewModel viewModel);

        /// <summary>
        /// The Reads for BrowsersViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{BrowsersViewModel}"/>.</returns>
        SimpleResponse<BrowsersViewModel> Read(int id);

        /// <summary>
        /// Updates entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Update(BrowsersViewModel viewModel);

        /// <summary>
        /// Deletes entity for BrowsersViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BrowsersViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Delete(BrowsersViewModel viewModel);

        /// <summary>
        /// Deletes record by given id value(s).
        /// </summary>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Delete(int id);

        /// <summary>
        /// Reads All records for BrowsersViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{List{BrowsersViewModel}}"/>.</returns>
        SimpleResponse<List<BrowsersViewModel>> ReadAll();
    }
}