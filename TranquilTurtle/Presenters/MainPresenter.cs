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

            view.SetStatus($"Tried to close and saved: {processName}");
        }

        private void OnLoadBlockedAppsClicked(object sender, EventArgs e)
        {
            var apps = model.GetBlockedApps();
            view.SetBlockedAppList(apps);
            view.SetStatus($"Loaded{apps.Count} blocked apps.");
        }
    }
}
