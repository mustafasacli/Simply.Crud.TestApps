using BookmarksStocker.Source.Business;
using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Management;
using BookmarksStocker.Source.Util;
using BookmarksStocker.Source.ViewModel;
using IWshRuntimeLibrary;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookmarksStocker.Source.Views
{
    public partial class FrmBookmarkList : Form
    {
        #region [ Private Fields ]

        private bool _isFromClosing = false;

        private IBookmarksBusiness bookmarksBusiness;

        private List<BookmarksViewModel> bookmarkList = new List<BookmarksViewModel>();
        private ExportManager exportMan;
        private int isCreationToggle = 1;
        private int isStartToggle = 1;

        #endregion [ Private Fields ]

        #region [ FrmBookmarkList Ctor ]

        public FrmBookmarkList()
        {
            try
            {
                InitializeComponent();

                bookmarksBusiness = new BookmarksBusiness();
                exportMan = new ExportManager();

                rdbtnCreationtime.Checked = true;
                rdbtnStartsWith.Checked = true;
                IsDateFilteringChecked(false);
                grdVwBookmarks.ColumnHeaderTextList = "ID[i]-Name-Description-Url-Creation Time-Update Time-Table[i]-ChangeSetCount[i]";
                cmbxSearchType.SetItemSource("Name*Name-Url*Url-Description*Description", '-', '*');

                if (cmbxSearchType.Items.Count > 0)
                    cmbxSearchType.SelectedIndex = 0;
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "Ctor");

                MessageUtil.Error("Bookmark List could not be opened.");
            }
        }

        #endregion [ FrmBookmarkList Ctor ]

        #region [ Add method ]

        private void Add()
        {
            try
            {
                FrmBookmark bkrmrk = new FrmBookmark();
                bkrmrk.BookmarkChanged += new FrmBookmark.BookmarkChange(this.BkmrkChanged);
                bkrmrk.ShowDialog();
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "Add");
            }
        }

        #endregion [ Add method ]

        #region [ addToolStripMenuItem_Click method ]

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        #endregion [ addToolStripMenuItem_Click method ]

        #region [ BkmrkChanged method ]

        private void BkmrkChanged(BookmarksViewModel bkmrk, bool isInsert)
        {
            try
            {
                if (isInsert)
                {
                    bookmarkList.Add(bkmrk);
                }
                else
                {
                    var bookmarkEntity = bookmarksBusiness.Read(bkmrk.Id).Data;

                    for (int bookmarkCounter = 0; bookmarkCounter < bookmarkList.Count; bookmarkCounter++)
                    {
                        if (bookmarkList[bookmarkCounter].Id == bkmrk.Id)
                        {
                            bookmarkList[bookmarkCounter].Name = bookmarkEntity.Name;
                            bookmarkList[bookmarkCounter].Description = bookmarkEntity.Description;
                            bookmarkList[bookmarkCounter].Url = bookmarkEntity.Url;
                            bookmarkList[bookmarkCounter].UpdateTime = bookmarkEntity.UpdateTime;
                        }
                    }
                }

                SetDataSourceOfGRid();
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "BkmrkChanged");
            }
        }

        #endregion [ BkmrkChanged method ]

        #region [ btnAdd_Click method ]

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        #endregion [ btnAdd_Click method ]

        #region [ btnDelete_Click method ]

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        #endregion [ btnDelete_Click method ]

        #region [ btnUpdate_Click method ]

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateObj();
        }

        #endregion [ btnUpdate_Click method ]

        #region [ Delete method ]

        private void Delete()
        {
            try
            {
                if (grdVwBookmarks.SelectedRows.Count > 0)
                {
                    if (MessageUtil.Confirm("Bookmark will be deleted. Are you sure?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        int bkmrkId = grdVwBookmarks.SelectedRows[0].Cells["ID"].Value.ToInt();
                        BookmarksViewModel bkmrk = null;
                        for (int bookmarkCounter = 0; bookmarkCounter < bookmarkList.Count; bookmarkCounter++)
                        {
                            if (bkmrkId == bookmarkList[bookmarkCounter].Id)
                            {
                                bkmrk = bookmarkList[bookmarkCounter];
                                break;
                            }
                        }

                        if (bkmrk != null)
                        {
                            bookmarksBusiness.Delete(bkmrk);
                            bookmarkList.Remove(bkmrk);
                            SetDataSourceOfGRid();
                            MessageUtil.Message("Bookmark deleted successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "Delete");

                MessageUtil.Error("Object could not be deleted.");
            }
        }

        #endregion [ Delete method ]

        #region [ deleteToolStripMenuItem_Click method ]

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        #endregion [ deleteToolStripMenuItem_Click method ]

        #region [ FormLoad method ]

        private void FormLoad(object sender, EventArgs e)
        {
            try
            {
                bookmarkList = bookmarksBusiness.ReadAll().Data;

                SetDataSourceOfGRid();
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "FormLoad");

                MessageUtil.Error("Bookmark List could not be loaded.");
            }
        }

        #endregion [ FormLoad method ]

        #region [ SetDataSourceOfGRid method ]

        private void SetDataSourceOfGRid()
        {
            try
            {
                SetDataSourceOfGRid(bookmarkList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetDataSourceOfGRid(List<BookmarksViewModel> lstBookmarks)
        {
            try
            {
                grdVwBookmarks.AllowUserToOrderColumns = true;
                grdVwBookmarks.AllowUserToResizeColumns = true;
                grdVwBookmarks.DataSource = null;
                grdVwBookmarks.DataSource = lstBookmarks;
                grdVwBookmarks.Refresh();
                //grdVwBookmarks.ColumnHeaderTextList = "ID[i]-Name-Descriptiom-Url-Creation Time-Update Time-Table[i]-ChangeSetCount[i]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [ SetDataSourceOfGRid method ]

        #region [ UpdateObj method ]

        private void UpdateObj()
        {
            try
            {
                if (grdVwBookmarks.SelectedRows.Count > 0)
                {
                    int bkmrkId = grdVwBookmarks.SelectedRows[0].Cells["ID"].Value.ToInt();
                    FrmBookmark bkrmrk = new FrmBookmark(bkmrkId);
                    bkrmrk.BookmarkChanged += new FrmBookmark.BookmarkChange(this.BkmrkChanged);
                    bkrmrk.ShowDialog();
                }
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "Update");

                MessageUtil.Error("Object could not be updated");
            }
        }

        #endregion [ UpdateObj method ]

        #region [ updateToolStripMenuItem_Click method ]

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateObj();
        }

        #endregion [ updateToolStripMenuItem_Click method ]

        #region [ runWithToolStripMenuItem_Click method ]

        private void runWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunWithForm(true);
        }

        #endregion [ runWithToolStripMenuItem_Click method ]

        #region [ RunWithForm method ]

        public void RunWithForm(bool isRegistered)
        {
            try
            {
                string url = string.Empty;

                if (grdVwBookmarks.SelectedRows.Count > 0)
                {
                    url = grdVwBookmarks.SelectedRows[0].Cells["Url"].Value.ToStr();
                }

                if (url.IsNullOrSpace() == false)
                {
                    FrmRunWith frw = new FrmRunWith(url, isRegistered);
                    frw.ShowDialog();
                }
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "RunWithForm");

                MessageUtil.Error("Object could not be runned.");
            }
        }

        #endregion [ RunWithForm method ]

        #region [ IsDatefilterchecked method ]

        private void IsDatefilterchecked(object sender, EventArgs e)
        {
            bool isChecked = chkDateFiltering.Checked;
            IsDateFilteringChecked(isChecked);
            Search();
        }

        #endregion [ IsDatefilterchecked method ]

        #region [ IsDateFilteringChecked method ]

        private void IsDateFilteringChecked(bool isChecked)
        {
            rdbtnCreationtime.Enabled = isChecked;
            rdbtnUpdatetime.Enabled = isChecked;

            rdbtnStartsWith.Enabled = isChecked;
            rdbtnBetween.Enabled = isChecked;
            rdbtnEndsWith.Enabled = isChecked;

            dtpStartDate.Enabled = isChecked;
            dtpStartTime.Enabled = isChecked;

            dtpEndDate.Enabled = isChecked;
            dtpEndTime.Enabled = isChecked;
        }

        #endregion [ IsDateFilteringChecked method ]

        #region [ IsCreationtimeToggleChanged method ]

        private void IsCreationtimeToggleChanged(object sender, EventArgs e)
        {
            try
            {
                isCreationToggle = (sender as RadioButton).Tag.ToInt();
            }
            catch (Exception ex)

            {
                isCreationToggle = 0;
                // FreeLogger.LogMethod(ex, this.Name, "IsCreationtimeToggleChanged");
            }
            finally
            {
                Search();
            }
        }

        #endregion [ IsCreationtimeToggleChanged method ]

        #region [ IsStartToggleChanged method ]

        private void IsStartToggleChanged(object sender, EventArgs e)
        {
            try
            {
                isStartToggle = (sender as RadioButton).Tag.ToInt();
            }
            catch (Exception ex)

            {
                isStartToggle = 0;
                // FreeLogger.LogMethod(ex, this.Name, "IsStartToggleChanged");
            }
            finally
            {
                Search();
            }
        }

        #endregion [ IsStartToggleChanged method ]

        #region [ DateChanged method ]

        private void DateChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion [ DateChanged method ]

        #region [ Search method ]

        private void Search(string strSearch, string searchColumn,
            bool isDateFiltering, int isCreationTime, int dateFilterSelection)
        {
            try
            {
                List<BookmarksViewModel> searchList = new List<BookmarksViewModel>();
                searchList = bookmarkList;
                if (searchList != null)
                {
                    try
                    {
                        if (strSearch.IsNullOrSpace() == false)
                        {
                            switch (searchColumn)
                            {
                                case "Name":
                                    searchList = searchList.Where(b => b.Name.Contains(strSearch)).ToList();
                                    break;

                                case "Description":
                                    searchList = searchList.Where(b => b.Description.Contains(strSearch)).ToList();
                                    break;

                                case "Url":
                                    searchList = searchList.Where(b => b.Url.Contains(strSearch)).ToList();
                                    break;

                                default:
                                    break;
                            }
                        }

                        if (isDateFiltering)
                        {
                            DateTime dtStart = new DateTime(dtpStartDate.Value.Year,
                                dtpStartDate.Value.Month,
                                dtpStartDate.Value.Day,
                                dtpStartTime.Value.Hour,
                                dtpStartTime.Value.Minute,
                                dtpStartTime.Value.Second);

                            DateTime dtEnd = new DateTime(dtpEndDate.Value.Year,
                                dtpEndDate.Value.Month,
                                dtpEndDate.Value.Day,
                                dtpEndTime.Value.Hour,
                                dtpEndTime.Value.Minute,
                                dtpEndTime.Value.Second);

                            switch (isCreationTime)
                            {
                                case 1:
                                    switch (dateFilterSelection)
                                    {
                                        case 1:
                                            searchList = searchList.Where(b => b.CreationTime >= dtStart).ToList();
                                            break;

                                        case 2:
                                            searchList = searchList.Where(b => b.CreationTime >= dtStart && b.CreationTime <= dtEnd).ToList();
                                            break;

                                        case 3:
                                            searchList = searchList.Where(b => b.CreationTime <= dtEnd).ToList();
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                                case 2:
                                    switch (dateFilterSelection)
                                    {
                                        case 1:
                                            searchList = searchList.Where(b => b.UpdateTime >= dtStart).ToList();
                                            break;

                                        case 2:
                                            searchList = searchList.Where(b => b.UpdateTime >= dtStart && b.UpdateTime <= dtEnd).ToList();
                                            break;

                                        case 3:
                                            searchList = searchList.Where(b => b.UpdateTime <= dtEnd).ToList();
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
#pragma warning disable CS0168 // The variable 'exInner' is declared but never used
                    catch (Exception exInner)
#pragma warning restore CS0168 // The variable 'exInner' is declared but never used
                    {
                        searchList = new List<BookmarksViewModel>();
                        // FreeLogger.LogMethod(exInner, this.Name, "Search_Inner");
                    }
                }

                SetDataSourceOfGRid(searchList);
            }
#pragma warning disable CS0168 // The variable 'exOuter' is declared but never used
            catch (Exception exOuter)
#pragma warning restore CS0168 // The variable 'exOuter' is declared but never used
            {
                // FreeLogger.LogMethod(exOuter, this.Name, "Search_Outer");
            }
        }

        #endregion [ Search method ]

        #region [ txtSearch_TextChanged method ]

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion [ txtSearch_TextChanged method ]

        #region [ Search method ]

        private void Search()
        {
            Search(txtSearch.Text, cmbxSearchType.SelectedValue.ToStr(), chkDateFiltering.Checked, isCreationToggle, isStartToggle);
        }

        #endregion [ Search method ]

        #region [ cmbxSearchType_SelectedIndexChanged method ]

        private void cmbxSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion [ cmbxSearchType_SelectedIndexChanged method ]

        #region [ grdVwBookmarks_SelectionChanged method ]

        private void grdVwBookmarks_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtDetails.Text = string.Empty;
                if (grdVwBookmarks.SelectedRows.Count > 0)
                {
                    DataGridViewCellCollection rowCells = grdVwBookmarks.SelectedRows[0].Cells;
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.AppendLine(string.Format("{0} : {1}", "İsim", rowCells["Name"].Value.ToStr()));
                    strBuilder.AppendLine("-------------------");
                    strBuilder.AppendLine(string.Format("{0} : {1}", "Açıklama", rowCells["Description"].Value.ToStr()));
                    strBuilder.AppendLine("-------------------");
                    strBuilder.AppendLine(string.Format("{0} : {1}", "Adres", rowCells["Url"].Value.ToStr()));
                    txtDetails.Text = strBuilder.ToString();
                }
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "grdVwBookmarks_SelectionChanged");
            }
        }

        #endregion [ grdVwBookmarks_SelectionChanged method ]

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageUtil.Confirm("Program will be closed. Are you sure?");
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                _isFromClosing = true;
                this.Close();
            }
            return;
        }

        #region [ ExportToExcel method ]

        private void ExportToExcel()
        {
            try
            {
                //DataTable dt2Export = new DataTable();
                //using (BookmarksDL bkmrkDL = new BookmarksDL())
                //{
                //    dt2Export = bkmrkDL.GetTable(new Bookmark { });
                //}
                //exportMan.Export2Excel(dt2Export, "Bookmarks", "D:/Bookmarks.xls");
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "ExportToExcel");
                MessageUtil.Error("Export Operation failed");
            }
        }

        #endregion [ ExportToExcel method ]

        #region [ ExportToExcelSearch method ]

        private void ExportToExcelSearch()
        {
            try
            {
                DataTable dt2Export = new DataTable();
                int rowCount = grdVwBookmarks.Rows.Count;
                if (rowCount > 0)
                {
                    int colCount = grdVwBookmarks.ColumnCount;
                    for (int colCounter = 0; colCounter < colCount; colCounter++)
                    {
                        if (object.ReferenceEquals(typeof(BookmarksViewModel).GetProperty(grdVwBookmarks.Columns[colCounter].Name).PropertyType, typeof(DateTime)) == false &&
                            grdVwBookmarks.Columns[colCounter].Visible == true)
                        {
                            dt2Export.Columns.Add(grdVwBookmarks.Columns[colCounter].Name, typeof(string));
                        }
                    }
                    colCount = dt2Export.Columns.Count;
                    DataRow row;
                    for (int rowCounter = 0; rowCounter < rowCount; rowCounter++)
                    {
                        row = null;
                        row = dt2Export.NewRow();

                        for (int colcounter = 0; colcounter < colCount; colcounter++)
                        {
                            row[dt2Export.Columns[colcounter].ColumnName] = grdVwBookmarks.Rows[rowCounter].Cells[dt2Export.Columns[colcounter].ColumnName].Value.ToStr();
                        }

                        dt2Export.Rows.Add(row);
                    }
                }

                exportMan.Export2Excel(dt2Export, "Bookmarks", "D:/Bookmarks.xls");
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "ExportToExcel");
                MessageUtil.Error("Export Operation failed");
            }
        }

        #endregion [ ExportToExcelSearch method ]

        private void externalBrowserListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenExternalBrowsers();
        }

        private void externalBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunWithForm(false);
        }

        private void FrmBookmarkList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isFromClosing == false)
            {
                DialogResult dR = MessageUtil.Confirm("Program will be closed. Are you sure?");
                if (dR != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void OpenExternalBrowsers()
        {
            FrmExternalBrowserList browserList = new FrmExternalBrowserList();
            browserList.ShowDialog();
        }

        private void shortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateShortCut();
        }

        #region [ CreateShortCut method ]

        private void CreateShortCut()
        {
            CreateShortCut(Environment.SpecialFolder.Desktop, @"\BookmarkStocker.lnk");
            return;
        }

        private void CreateShortCut(Environment.SpecialFolder flder, string filePath)
        {
            try
            {
                IWshRuntimeLibrary.IWshShortcut shortCut;
                WshShell ws = new WshShell();
                shortCut = (IWshShortcut)ws.CreateShortcut(Environment.GetFolderPath(flder) + filePath);
                shortCut.TargetPath = Application.ExecutablePath;
                shortCut.Description = "This software has been made for managing all bookmarks in one program. Use for good days.\nMade By : Musty\nPath : " + Application.ExecutablePath;
                shortCut.IconLocation = Application.StartupPath + @"\monitor.ico";
                shortCut.Save();
            }
            catch (Exception ex)

            {
                MessageUtil.Error("Shortcut could not be created.");
                // FreeLogger.LogMethod(ex, this.Name, "CreateShortCut");
            }
        }

        #endregion [ CreateShortCut method ]

        private void allBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void selectedBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToExcelSearch();
        }
    }
}