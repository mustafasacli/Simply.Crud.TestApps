using Bookmarks.Project.Dtos;
using Bookmarks.Project.ViewModel;
using SimpleInfra.Common.Response;
using System.Collections.Generic;

namespace Bookmarks.Project.BusinessCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IBookmarksBusiness"/>.
    /// </summary>
    public interface IBookmarksBusiness
    {
        /// <summary>
        /// The Creates entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse{BookmarksViewModel}"/>.</returns>
        SimpleResponse<BookmarksViewModel> Create(BookmarksViewModel viewModel);

        /// <summary>
        /// The Reads for BookmarksViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{BookmarksViewModel}"/>.</returns>
        SimpleResponse<BookmarksViewModel> Read(long id);

        /// <summary>
        /// Updates entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Update(BookmarksViewModel viewModel);

        /// <summary>
        /// Deletes entity for BookmarksViewModel.
        /// </summary>
        /// <param name="viewModel">The viewModel <see cref="BookmarksViewModel"/>.</param>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Delete(BookmarksViewModel viewModel);

        /// <summary>
        /// Deletes record by given id value(s).
        /// </summary>
        /// <returns>The <see cref="SimpleResponse"/>.</returns>
        SimpleResponse Delete(long id);

        /// <summary>
        /// Reads All records for BookmarksViewModel.
        /// </summary>
        /// <returns>The <see cref="SimpleResponse{List{BookmarksViewModel}}"/>.</returns>
        SimpleResponse<List<BookmarksViewModel>> ReadAll();
    }
}