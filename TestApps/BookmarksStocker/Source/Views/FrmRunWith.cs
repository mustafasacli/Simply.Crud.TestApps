using BookmarksStocker.Source.Business.Interfaces;
using BookmarksStocker.Source.Util;
using BookmarksStocker.Source.ViewModel;
using BrowsersStocker.Source.Business;
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace BookmarksStocker.Source.Views
{

    public partial class FrmRunWith : Form
    {
        string _url = string.Empty;
        bool _isRegistered = true;
        IBrowsersBusiness browsersBusiness;

        public FrmRunWith(string url, bool isRegistered)
        {
            try
            {
                InitializeComponent();
                browsersBusiness = new BrowsersBusiness();
                _isRegistered = isRegistered;
                _url = url;
                List<BrowsersViewModel> browsers = new List<BrowsersViewModel>();

                if (_isRegistered)
                {
                    browsers = GetBrowsers();
                }
                else
                {
                    browsers = browsersBusiness.ReadAll().Data;
                }

                cmbxBrowsers.DataSource = browsers;
                cmbxBrowsers.DisplayMember = "Name";
                cmbxBrowsers.ValueMember = "Path";
                cmbxBrowsers.SelectedIndex = -1;
                cmbxBrowsers.Refresh();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                // FreeLogger.LogMethod(ex, this.Name, "Ctor");

                MessageUtil.Error("Run With Form could not be opened.");
            }
        }


        private List<BrowsersViewModel> GetBrowsers()
        {
            List<BrowsersViewModel> _browsers = new List<BrowsersViewModel>();
            try
            {
                string browserListKey = @"SOFTWARE\Clients\StartMenuInternet";
                using (var clients = Registry.LocalMachine.OpenSubKey(browserListKey))
                {
                    foreach (var client in clients.GetSubKeyNames())
                    {
                        using (var clientKey = clients.OpenSubKey(client))
                        {
                            string name = (string)clientKey.GetValue(string.Empty);
                            using (var commandKey = clientKey.OpenSubKey(@"shell\open\command"))
                            {
                                string exe = (string)commandKey.GetValue(string.Empty);
                                _browsers.Add(new BrowsersViewModel() { Name = name, Path = exe });
                            }
                        }
                    }
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                // FreeLogger.LogMethod(ex, this.Name, "GetBrowsers");
                _browsers = new List<BrowsersViewModel>();
            }
            return _browsers;
        }

        private void btnRunWith_Click(object sender, EventArgs e)
        {
            RunWith();
        }

        void RunWith()
        {
            try
            {
                if (_url.IsNullOrSpace())
                {
                    MessageUtil.Message("Url can not be null!..");
                    return;
                }
                if (cmbxBrowsers.SelectedIndex == -1)
                {
                    MessageUtil.Message("Select Browser!..");
                    return;
                }
                if (cmbxBrowsers.SelectedValue == null)
                {
                    MessageUtil.Message("Select Browser!..");
                    return;
                }

                using (Process p = new Process())
                {
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = cmbxBrowsers.SelectedValue.ToString();
                    p.StartInfo.Arguments = "\"" + _url + "\"";
                    p.Start();
                }
                this.Close();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                // FreeLogger.LogMethod(ex, this.Name, "RunWith");

                MessageUtil.Error("Object could not be runned.");
            }
        }

    }
}