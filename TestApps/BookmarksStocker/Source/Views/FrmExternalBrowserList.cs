using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Util;
using BookmarksStocker.Source.ViewModel;
using BrowsersStocker.Source.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BookmarksStocker.Source.Views
{
    public partial class FrmExternalBrowserList : Form
    {
        DataTable browserList = new DataTable();
        IBrowsersBusiness browsersBusiness;
        List<BrowsersViewModel> browsers = new List<BrowsersViewModel>();

        public FrmExternalBrowserList()
        {
            try
            {
                InitializeComponent();
                browsersBusiness = new BrowsersBusiness();
                RefreshSource();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RefreshSource()
        {
            try
            {
                browsers = browsersBusiness.ReadAll().Data;
                SetDataSourceOfGRid();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region [ SetDataSourceOfGRid method ]

        private void SetDataSourceOfGRid()
        {
            try
            {
                grdBrowsers.AllowUserToOrderColumns = true;
                grdBrowsers.AllowUserToResizeColumns = true;
                grdBrowsers.DataSource = null;
                grdBrowsers.DataSource = browserList;
                grdBrowsers.Refresh();
                //grdBrowsers.ColumnHeaderTextList = "ID[i]-Name-Descriptiom-Url-Creation Time-Update Time-Table[i]-ChangeSetCount[i]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObj();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateObj();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteObj();
        }

        void AddObj()
        {
            FrmExternalBrowser frmExtBrowser = new FrmExternalBrowser(browserList);
            frmExtBrowser.ExternalBrowserChanged += new FrmExternalBrowser.ExternalBrowserChage(this.UpdateForm);
            frmExtBrowser.ShowDialog();
        }

        void UpdateForm()
        {
            try
            {
                RefreshSource();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                // FreeLogger.LogMethod(ex, this.Name, "UpdateForm");

                MessageUtil.Error("List could not be refreshed.");
            }
        }

        void UpdateObj()
        {
            if (grdBrowsers.SelectedRows.Count > 0)
            {
                int browserId = grdBrowsers.SelectedRows[0].Cells["Id"].Value.ToInt();
                FrmExternalBrowser frmExtBrowser = new FrmExternalBrowser(browserId, browserList);
                frmExtBrowser.ExternalBrowserChanged += new FrmExternalBrowser.ExternalBrowserChage(this.UpdateForm);
                frmExtBrowser.ShowDialog();
            }
        }

        void DeleteObj()
        {
            try
            {
                if ((grdBrowsers.SelectedRows?.Count ?? 0) < 1) return;

                DialogResult dr = MessageUtil.Confirm("External Browser will be deleted. are you sure?");
                if (dr != System.Windows.Forms.DialogResult.Yes) return;

                int browserId = grdBrowsers.SelectedRows[0].Cells["Id"].Value.ToInt();
                BrowsersViewModel br = new BrowsersViewModel { Id = browserId };
                browsersBusiness.Delete(br);
                UpdateForm();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                // FreeLogger.LogMethod(ex, this.Name, "DeleteObj");

                MessageUtil.Error("Object could not be deleted.");
            }
        }

        private void addBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBrowser();
        }

        void AddBrowser()
        {
            AddObj();
        }

    }
}