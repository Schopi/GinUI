﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using GinClientApp.Dialogs;
using GinClientApp.GinService;
using GinClientApp.Properties;
using GinClientLibrary;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace GinClientApp
{
    public class GinApplicationContext : ApplicationContext, IGinServiceCallback
    {
        public GinServiceClient ServiceClient;
        private readonly NotifyIcon _trayIcon;
        private readonly UserCredentials _credentials;
        private Timer _updateIntervalTimer;

        private ProgressDisplayDlg _progressDisplayDlg;

        private void RecreateClient()
        {
            ServiceClient = new GinServiceClient(new InstanceContext(this));
            ServiceClient.InnerDuplexChannel.Faulted += InnerChannelOnFaulted;
            ServiceClient.InnerChannel.OperationTimeout = TimeSpan.MaxValue;
            ServiceClient.InnerDuplexChannel.OperationTimeout = TimeSpan.MaxValue;
        }

        public GinApplicationContext()
        {
            _trayIcon = new NotifyIcon
            {
                Visible = true,
                Icon = Resources.gin_icon_desaturated
            };

            ServiceClient = new GinServiceClient(new InstanceContext(this));
            var saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               @"\g-node\GinWindowsClient";
            if (!Directory.Exists(saveFilePath))
                Directory.CreateDirectory(saveFilePath);

            #region Read options

            if (!GlobalOptions.Load())
            {
                //TODO: Display options dialog here
            }

            if (GlobalOptions.Instance.RepositoryUpdateInterval > 0)
            {
                _updateIntervalTimer = new Timer(GlobalOptions.Instance.RepositoryUpdateInterval * 1000) {AutoReset = true};
                _updateIntervalTimer.Elapsed += (sender, args) =>
                {
                    ServiceClient.DownloadAllUpdateInfo();
                };
            }

            GlobalOptions.Save();

            #endregion

            #region Login

            if (!UserCredentials.Load())
            {
                //TODO: Ask for credentials here
            }

            if (!ServiceClient.Login(UserCredentials.Instance.Username, UserCredentials.Instance.Password))
            {
                MessageBox.Show(Resources.GinApplicationContext_Error_while_trying_to_log_in_to_GIN, Resources.GinApplicationContext_Gin_Client_Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //TODO: Ask for correct credentials, retry until correct or user exits
            }

            UserCredentials.Save();
            
            #endregion

            #region Set up repositories

            if (File.Exists(saveFilePath + @"\SavedRepositories.json"))
            {
                try
                {
                    var text = File.OpenText(saveFilePath + @"\SavedRepositories.json").ReadToEnd();
                    var repos = JsonConvert.DeserializeObject<GinRepositoryData[]>(text);

                    foreach (var repo in repos)
                    {
                        ServiceClient.AddRepository(repo.PhysicalDirectory.FullName, repo.Mountpoint.FullName, repo.Name,
                            repo.Address, GlobalOptions.Instance.RepositoryCheckoutOption == GlobalOptions.CheckoutOption.FullCheckout, false);
                    }
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                ManageRepositoriesMenuItemHandler(null, EventArgs.Empty);
            }

            #endregion

            
            _trayIcon.DoubleClick += _trayIcon_DoubleClick;
            _trayIcon.ContextMenu = new ContextMenu(BuildContextMenu());
            _trayIcon.Icon = Resources.gin_icon;
            _updateIntervalTimer?.Start();
        }

        private void InnerChannelOnFaulted(object sender1, EventArgs eventArgs)
        {
            MessageBox.Show(Resources.GinApplicationContext_Gin_Service_has_stopped_communicating_,
                Resources.GinApplicationContext_Gin_Service_Error, MessageBoxButtons.OK);
            Exit(null, EventArgs.Empty);
        }

        private MenuItem[] BuildContextMenu()
        {
            var menuitems = new List<MenuItem>();

            var repositories = JsonConvert.DeserializeObject<GinRepositoryData[]>(ServiceClient.GetRepositoryList());
            foreach (var repo in repositories)
            {
                var mitem = new MenuItem(repo.Name) {Tag = repo};
                //mitem.MenuItems.Add("Edit", EditRepoMenuItemHandler);
                mitem.MenuItems.Add(Resources.GinApplicationContext_Upload, UploadRepoMenuItemHandler);
                mitem.MenuItems.Add(Resources.GinApplicationContext_Unmount, UnmountRepoMenuItemHandler);
                mitem.MenuItems.Add(Resources.GinApplicationContext_Update, UpdateRepoMenuItemHandler);

                menuitems.Add(mitem);
            }

            if (repositories.Length != 0)
                menuitems.Add(new MenuItem("-"));

            menuitems.Add(new MenuItem(Resources.GinApplicationContext_Manage_Repositories, ManageRepositoriesMenuItemHandler));

            menuitems.Add(new MenuItem(Resources.GinApplicationContext_Options, ShowOptionsMenuItemHandler));

            menuitems.Add(new MenuItem(Resources.GinApplicationContext_Exit, Exit));

            return menuitems.ToArray();
        }

        private void UploadRepoMenuItemHandler(object sender, EventArgs e)
        {
            var repo = (GinRepositoryData)((MenuItem)sender).Parent.Tag;
            var fstatus = JsonConvert.DeserializeObject <
                          Dictionary<string, GinRepository.FileStatus>>(ServiceClient.GetRepositoryFileInfo(repo.Name));

            var alteredFiles = from kvp in fstatus
                where kvp.Value == GinRepository.FileStatus.OnDiskModified ||
                      kvp.Value == GinRepository.FileStatus.Unknown
                select kvp;

            var files = alteredFiles as KeyValuePair<string, GinRepository.FileStatus>[] ?? alteredFiles.ToArray();
            if (!files.Any())
                return; //Nothing to upload here

            var uploadfiledlg = new UploadFilesDlg(files);
            var res = uploadfiledlg.ShowDialog();

            if (res == DialogResult.Cancel) return;

            foreach (var file in files)
            {
                ServiceClient.UploadFile(repo.Name, file.Key);
            }
        }

        private void ShowOptionsMenuItemHandler(object sender, EventArgs e)
        {
            var optionsDlg = new GlobalOptionsDlg(GlobalOptions.Instance);
            var res = optionsDlg.ShowDialog();

            if (res != DialogResult.OK) return;

            if (GlobalOptions.Instance.RepositoryUpdateInterval <= 0)
            {
                _updateIntervalTimer?.Stop();
                return;
            }

            if (_updateIntervalTimer == null)
            {
                _updateIntervalTimer = new Timer(GlobalOptions.Instance.RepositoryUpdateInterval * 1000) { AutoReset = true };
                _updateIntervalTimer.Elapsed += (sender1, args) => { 
                    ServiceClient.DownloadAllUpdateInfo();
                };
            }
            _updateIntervalTimer.Stop();
            _updateIntervalTimer.Interval = GlobalOptions.Instance.RepositoryUpdateInterval * 1000;
            _updateIntervalTimer.Start();
        }

        private void UpdateRepoMenuItemHandler(object sender, EventArgs e)
        {
            var repo = (GinRepositoryData)((MenuItem) sender).Parent.Tag;

            ServiceClient.DownloadUpdateInfo(repo.Name);
        }

        private void ManageRepositoriesMenuItemHandler(object sender, EventArgs e)
        {
            var repomanager = new RepoManagementDlg(this);
            repomanager.Closed += (o, args) => { if (_trayIcon!= null) _trayIcon.ContextMenu = new ContextMenu(BuildContextMenu()); };
            repomanager.ShowDialog();
            
            ServiceClient.UnmmountAllRepositories();

            if (repomanager.Repositories.Count == 0) return;

            foreach (var repo in repomanager.Repositories)
            {
                ServiceClient.AddRepository(repo.PhysicalDirectory.FullName, repo.Mountpoint.FullName, repo.Name,
                    repo.Address,
                    GlobalOptions.Instance.RepositoryCheckoutOption ==
                    GlobalOptions.CheckoutOption.FullCheckout, repo.CreateNew);

                repo.CreateNew = false;
            }
            var saveFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                           @"\g-node\GinWindowsClient\SavedRepositories.json";

            if (!Directory.Exists(Path.GetDirectoryName(saveFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(saveFile));

            if (File.Exists(saveFile))
                File.Delete(saveFile);


            var fs = File.CreateText(saveFile);
            fs.Write(JsonConvert.SerializeObject(repomanager.Repositories));
            fs.Flush();
            fs.Close();
        }

        private void UnmountRepoMenuItemHandler(object sender, EventArgs e)
        {
            
            var mItem = (MenuItem) sender;
            var repo = (GinRepositoryData)mItem.Parent.Tag;
            if (string.CompareOrdinal(Resources.GinApplicationContext_Unmount, mItem.Text) == 0)
            {
                ServiceClient.UnmountRepository(repo.Name);

                mItem.Text = Resources.GinApplicationContext_Mount;
            }
            else
            {
                ServiceClient.MountRepository(repo.Name);
                mItem.Text = Resources.GinApplicationContext_Unmount;
            }
        }

        void IGinServiceCallback.FileOperationFinished(string filename, string repository, bool success)
        {
            _progressDisplayDlg.NestingLevel--;

            if (_progressDisplayDlg.NestingLevel == 0)
            {
                _progressDisplayDlg.Close();
                _progressDisplayDlg = null;
            }
        }

        void IGinServiceCallback.FileOperationStarted(string filename, string repository)
        {
            _trayIcon.BalloonTipTitle = Resources.GinApplicationContext_Repository_Activity;
            _trayIcon.BalloonTipText =
                string.Format(Resources.GinApplicationContext_FileOperation_Retrieving, Path.GetFileName(filename), repository);
            _trayIcon.ShowBalloonTip(5000);

            if (_progressDisplayDlg == null)
                _progressDisplayDlg = new ProgressDisplayDlg() {NestingLevel = 1};
            else
                _progressDisplayDlg.NestingLevel++;
            
            _progressDisplayDlg.Show();
        }

        void IGinServiceCallback.FileOperationProgress(string filename, string repository, int progress,
            string speed, string state)
        {
            Console.WriteLine("Filename: {0}, Repo: {1}, Progress: {2}, Speed: {3}, State: {4}", filename, repository,
                progress, speed, state);

            _progressDisplayDlg?.SetProgressBarState(filename, state, progress, speed);
        }

        void IGinServiceCallback.GinServiceError(string message)
        {
            MessageBox.Show(message, Resources.GinApplicationContext_Gin_Service_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Exit(this, EventArgs.Empty);
        }

        private void _trayIcon_DoubleClick(object sender, EventArgs e)
        {
            //TODO: Implement a management interface
        }

        private void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            if (_trayIcon != null)
                _trayIcon.Visible = false;

            if (ServiceClient != null && ServiceClient.InnerChannel.State != CommunicationState.Faulted)
            {
                ServiceClient.UnmmountAllRepositories();
                ServiceClient.Logout();
            }

            Application.Exit();
        }
    }
}