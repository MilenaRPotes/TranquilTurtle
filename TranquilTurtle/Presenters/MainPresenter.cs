using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquilTurtle.Models;
using TranquilTurtle.Views;
using TranquilTurtle.Services;
using System.Windows.Threading;

namespace TranquilTurtle.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView view;
        private readonly BlockedAppsModel model;
        private DispatcherTimer backgroundBlockerTimer;
        private DateTime blockEndTime;

        public MainPresenter(IMainView view, BlockedAppsModel model) 
        { 
            this.view = view;
            this.model = model;

            this.view.BlockAppClicked += OnBlockAppClicked;
            this.view.LoadBlockedAppsClicked += OnLoadBlockedAppsClicked;
            this.view.EditAppClicked += OnEditAppClicked;
            this.view.DeleteAppClicked += OnDeleteAppClicked;
            this.view.StartTimerClicked += OnStartTimerClicked;
        }

        private void OnBlockAppClicked(object? sender, EventArgs e)
        {
            var processName = view.ProcessNameInput.Trim();

            if (string.IsNullOrEmpty(processName))
            {
                var blockedApps = model.GetBlockedApps();

                if (blockedApps.Count == 0 )
                {
                    view.SetStatus("No apps to block. Load or add apps first.");
                    return;
                }

                int blockedCount = 0;
                foreach ( var app in blockedApps ) 
                {
                    if (AppBlockerService.KillApp(app)) 
                     blockedCount++;
                }

                view.SetStatus($"Blocked {blockedCount} of {blockedApps.Count} saved apps.");
                return;
            }


            if (AppBlockerService.KillApp(processName))
            {
                model.AddBlockedApp(processName);
                view.SetBlockedAppList(model.GetBlockedApps());
                view.SetStatus($"Blocked and saved: {processName}");
            }
            else 
            {
                view.SetStatus($"Could not find or block:{processName}");
            }


           
        }

        private void OnLoadBlockedAppsClicked(object sender, EventArgs e)
        {
            var apps = model.GetBlockedApps();
            view.SetBlockedAppList(apps);
            view.SetStatus($"Loaded{apps.Count} blocked apps.");
        }

        //Timer
        private void OnStartTimerClicked(object sender, EventArgs e) 
        { 
            TimeSpan duration = view.GetTimerDuration();
            StartBlocking(duration);
        }

        public void StartBlocking(TimeSpan duration) 
        { 
            blockEndTime = DateTime.Now.Add(duration);
            view.ShowBlockingUI(duration);

            backgroundBlockerTimer = new DispatcherTimer();
            backgroundBlockerTimer.Interval = TimeSpan.FromSeconds(2);
            backgroundBlockerTimer.Tick += (s, e) => 
            {
                if (DateTime.Now >= blockEndTime) 
                {
                    StopBlocking();
                    return;
                }

                var apps = model.GetBlockedApps();
                foreach ( var app in apps ) 
                { 
                    AppBlockerService.KillApp(app);
                }
            
            };
            backgroundBlockerTimer.Start();
        }

        public void StopBlocking() 
        { 
            backgroundBlockerTimer?.Stop();
            view.HideBlockingUI();
            view.SetStatus("Blocking ended.");
        }

        private void OnEditAppClicked(object? sender, EventArgs e) 
        { 
            var selected = view.SelectedApp;
            if (string.IsNullOrEmpty(selected)) 
            {
                view.SetStatus("Please select an app to edit.");
                return;
            }

            var dialog = new InputDialog(selected);
            if (dialog.ShowDialog() == true) 
            { 
                string newName = dialog.ResponseText.Trim();
                if (string.IsNullOrEmpty(newName)) 
                {
                    view.SetStatus("Name cannot be empty.");
                    return;
                }

                var apps = model.GetBlockedApps();
                int index = apps.IndexOf(selected);
                if (index != -1) 
                {
                    apps[index] = newName;
                    SettingsService.SaveBlockedApps(apps);
                    view.SetBlockedAppList(apps);
                    view.SetStatus($"Renamed {selected} to {newName}");
                }
            
            }

        }

        private void OnDeleteAppClicked(object? sender, EventArgs e)
        {
            var selected = view.SelectedApp;
            if (string.IsNullOrEmpty(selected)) 
            { 
                view.SetStatus("Please select an app to delete.");
                return; 
            }

            var apps = model.GetBlockedApps();
            if (apps.Contains(selected)) 
            { 
                apps.Remove(selected);
                SettingsService.SaveBlockedApps(apps);
                view.SetBlockedAppList(apps);
                view.SetStatus($"Deleted {selected}");
            }
        }


    }
}
