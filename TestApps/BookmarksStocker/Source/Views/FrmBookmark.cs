using BookmarksStocker.Source.Business;
using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Util;
using BookmarksStocker.Source.ViewModel;
using System;
using System.Windows.Forms;

namespace BookmarksStocker.Source.Views
{
    public partial class FrmBookmark : Form
    {
        #region [ Public Fields ]

        public delegate void BookmarkChange(BookmarksViewModel bookmark, bool isInsert);

        public BookmarkChange BookmarkChanged;

        #endregion [ Public Fields ]

        #region [ Private Fields ]

        private int _bookmarkId;
        private bool _isFormLoaded = false;
        private BookmarksViewModel bookmark;
        private IBookmarksBusiness bookmarksBusiness;
#pragma warning disable CS0414 // The field 'FrmBookmark.isUpdated' is assigned but its value is never used
        private bool isUpdated = false;
#pragma warning restore CS0414 // The field 'FrmBookmark.isUpdated' is assigned but its value is never used

        #endregion [ Private Fields ]

        #region [ FrmBookmark Ctors ]

        public FrmBookmark()
            : this(-1)
        { }

        public FrmBookmark(int bookmarkId)
        {
            try
            {
                InitializeComponent();
                bookmarksBusiness = new BookmarksBusiness();
                bookmark = new BookmarksViewModel();
                _bookmarkId = bookmarkId;
            }
            catch (Exception ex)
            {
                // FreeLogger.LogMethod(ex, this.Name, "FrmBookmark Ctor");

                MessageUtil.Error("Bookmarks could not be opened." + ex.Message);
            }
        }

        #endregion [ FrmBookmark Ctors ]

        #region [ FormLoad method ]

        private void FormLoad(object sender, EventArgs e)
        {
            LoadForm();
        }

        #endregion [ FormLoad method ]

        #region [ LoadForm method ]

        private void LoadForm()
        {
            try
            {
                _isFormLoaded = false;
                if (_bookmarkId != -1)
                {
                    bookmark = bookmarksBusiness.Read(_bookmarkId).Data;
                    if (bookmark != null)
                    {
                        txtName.Text = bookmark.Name;
                        txtDescription.Text = bookmark.Description;
                        txtUrl.Text = bookmark.Url;
                    }
                }
                _isFormLoaded = true;
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "FormLoad");

                MessageUtil.Error("BookmarksViewModel could not be loaded.");
            }
            finally
            {
                //bookmark = new BookmarksViewModel();

                if (bookmark != null && _bookmarkId != -1)
                    bookmark.Id = _bookmarkId;
            }
        }

        #endregion [ LoadForm method ]

        #region [ btnSave_Click method ]

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion [ btnSave_Click method ]

        #region [ Save method ]

        private void Save()
        {
            try
            {
                if (txtUrl.Text.IsNullOrSpace())
                {
                    MessageUtil.Warn("Url can not be empty.");
                    return;
                }

                if (txtName.Text.IsNullOrSpace())
                {
                    MessageUtil.Warn("Name can not be empty.");
                    return;
                }

                bool result = bookmarksBusiness.ExistByName(bookmark);
                if (result)
                {
                    MessageUtil.Warn("BookmarksViewModel is exist with given name.");
                    return;
                }

                result = bookmarksBusiness.ExistByUrl(bookmark);
                if (result)
                {
                    MessageUtil.Warn("BookmarksViewModel is exist with given Url.");
                    return;
                }

                if (_bookmarkId == -1)
                {
                    bookmark.CreationTime = DateTime.Now;
                    var response = bookmarksBusiness.Create(bookmark);
                    if (response.ResponseCode < 0)
                    {
                        MessageUtil.Message("Hata: " + response.ResponseMessage);
                    }
                    else
                    {
                        txtName.ResetText();
                        txtDescription.ResetText();
                        txtUrl.ResetText();

                        bookmark = new BookmarksViewModel();
                    }
                }
                else
                {
                    bookmark.UpdateTime = DateTime.Now;
                    var response = bookmarksBusiness.Update(bookmark);
                    if (response.ResponseCode > 0)
                    {
                        isUpdated = true;

                        btnSave.Enabled = false;
                        btnCancel.Text = "Close";
                        if (BookmarkChanged != null)
                        {
                            BookmarkChanged(bookmark, false);
                        }
                    }
                    else
                    {
                        MessageUtil.Message("Hata: " + response.ResponseMessage);
                    }
                }
                MessageUtil.Message("Boomark Saved.");
            }
            catch (Exception ex)
            {
                MessageUtil.Error("Kaydetmede hata oluştu." + ex.Message);
                // FreeLogger.LogMethod(ex, this.Name, "Save");
            }
        }

        #endregion [ Save method ]

        #region [ btnCancel_Click method ]

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion [ btnCancel_Click method ]

        #region [ FrmBookmark_FormClosing method ]

        private void FrmBookmark_FormClosing(object sender, FormClosingEventArgs e)
        {
            //int changeCount = _bookmarkId == -1 ? 0 : 1;
            //if (bookmark.ChangeSetCount > changeCount && isUpdated == false)
            //{
            DialogResult dr = MessageUtil.Confirm("Form will be Closed, Are you sure?");
            if (dr != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
            //}
        }

        #endregion [ FrmBookmark_FormClosing method ]

        #region [ TextChanged methods ]

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
                bookmark.Name = txtName.Text;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
                bookmark.Description = txtDescription.Text;
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
                bookmark.Url = txtUrl.Text;
        }

        #endregion [ TextChanged methods ]
    }
}