using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquilTurtle.Views
{
    public interface IMainView
    {
       
        event EventHandler BlockAppClicked;
        event EventHandler LoadBlockedAppsClicked;
        event EventHandler EditAppClicked;
        event EventHandler DeleteAppClicked;

        string ProcessNameInput { get; }
        string SelectedApp { get;}
        void SetBlockedAppList(List<string> apps);
        void SetStatus(string message);

    }
}
