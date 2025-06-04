using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquilTurtle.Models;
using TranquilTurtle.Views;
using TranquilTurtle.Services;

namespace TranquilTurtle.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView view;
        private readonly BlockedAppsModel model;

        public MainPresenter(IMainView view, BlockedAppsModel model) 
        { 
            this.view = view;
            this.model = model;

            this.view.BlockAppClicked += OnBlockAppClicked;
            this.view.LoadBlockedAppsClicked += OnLoadBlockedAppsClicked;
            this.view.EditAppClicked += OnEditAppClicked;
            this.view.DeleteAppClicked += OnDeleteAppClicked;
        }

        private void OnBlockAppClicked(object? sender, EventArgs e)
        {
            var processName = view.ProcessNameInput.Trim();

            if (string.IsNullOrEmpty(processName))
            {
                view.SetStatus("Please enter a valid process name.");
                return;
            }

            AppBlockerService.KillApp(processName);
            model.AddBlockedApp(processName);
            view.SetBlockedAppList(model.GetBlockedApps());
            view.SetStatus($"Blocked and saved: {processName}");
        }

        private void OnLoadBlockedAppsClicked(object sender, EventArgs e)
        {
            var apps = model.GetBlockedApps();
            view.SetBlockedAppList(apps);
            view.SetStatus($"Loaded{apps.Count} blocked apps.");
        }

        private void OnEditAppClicked(object? sender, EventArgs e) 
        { 
            var selected = view.SelectedApp;
            if (string.IsNullOrEmpty(selected)) 
            {
                view.SetStatus("Please select an app to edit.");
                return;
            }

            //var dialog = new InputDialog(selected);

        }

        private void 


    }
}
