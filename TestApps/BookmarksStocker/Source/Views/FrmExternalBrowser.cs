using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Util;
using BookmarksStocker.Source.ViewModel;
using BrowsersStocker.Source.Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace BookmarksStocker.Source.Views
{
    public partial class FrmExternalBrowser : Form
    {
        private int _browserId = -1;

        public delegate void ExternalBrowserChage();

        public ExternalBrowserChage ExternalBrowserChanged;

        private BrowsersViewModel browser = null;
        private DataTable dtBrowserList = null;
        private bool isFormLoaded = false;
        private OpenFileDialog opFileDialog = null;
        private IBrowsersBusiness browsersBusiness;

        public FrmExternalBrowser(DataTable dtBrowsers) : this(-1, dtBrowsers)
        {
        }

        public FrmExternalBrowser(int browserId, DataTable dtBrowsers)
        {
            try
            {
                InitializeComponent();
                _browserId = browserId;
                dtBrowserList = dtBrowsers;
                browsersBusiness = new BrowsersBusiness();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            try
            {
                isFormLoaded = false;
                if (_browserId != -1)
                {
                    if (dtBrowserList != null)
                    {
                        for (int rowCounter = 0; rowCounter < dtBrowserList.Rows.Count; rowCounter++)
                        {
                            if (dtBrowserList.Rows[rowCounter]["Id"].ToInt() == _browserId)
                            {
                                txtName.Text = dtBrowserList.Rows[rowCounter]["Name"].ToStr();
                                txtPath.Text = dtBrowserList.Rows[rowCounter]["Path"].ToStr();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "FormLoad");

                MessageUtil.Error("Browser List could not be loaded.");
            }
            finally
            {
                isFormLoaded = true;

                browser = new BrowsersViewModel();
                if (_browserId != -1)
                    browser.Id = _browserId;
            }
        }

        private void TxtNameChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
                browser.Name = txtName.Text;
        }

        private void TxtPathChanged(object sender, EventArgs e)
        {
            if (isFormLoaded)
                browser.Path = txtPath.Text;
        }

        private void Save()
        {
            try
            {
                if (browser.Name.Replace(" ", "").Length == 0)
                {
                    MessageUtil.Message("Browser name can not be empty.");
                    return;
                }

                if (browser.Path.Replace(" ", "").Length == 0)
                {
                    MessageUtil.Message("Browser path can not be empty.");
                    return;
                }

                if (_browserId == -1)
                {
                    var response = browsersBusiness.Create(browser);
                    if (response.ResponseCode < 0)
                    {
                        MessageUtil.Message("Hata: " + response.ResponseMessage);
                    }
                    else
                    {
                        txtName.ResetText();
                        txtPath.ResetText();

                        browser = new BrowsersViewModel();
                    }
                }
                else
                {
                    var response = browsersBusiness.Update(browser);

                    if (response.ResponseCode > 0)
                    {
                        btnSave.Enabled = false;
                        btnClose.Text = "Close";
                    }
                    else
                    {
                        MessageUtil.Message("Hata: " + response.ResponseMessage);
                    }
                }

                //this.Close();
                if (ExternalBrowserChanged != null)
                    ExternalBrowserChanged();
            }
            catch (Exception ex)

            {
                // FreeLogger.LogMethod(ex, this.Name, "Save");

                MessageUtil.Error("Browsers could not be saved.");
            }
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                opFileDialog = new OpenFileDialog();
                opFileDialog.FileName = string.Empty;
                opFileDialog.Title = "Please Select Application Path";
                opFileDialog.Filter = "(*.exe)|*.exe|(*.lnk)|*.lnk";
                opFileDialog.FilterIndex = 1; // varsayılan olarak jpg uzantıları göster
                opFileDialog.InitialDirectory = Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory);//,Environment.SpecialFolderOption.None);
                opFileDialog.ShowDialog();

                if (string.IsNullOrWhiteSpace(opFileDialog.FileName) == false)
                {
                    txtPath.Text = opFileDialog.FileName;
                }
                return;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                opFileDialog = null;
            }
        }
    }
}