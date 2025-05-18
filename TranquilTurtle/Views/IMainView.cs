using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquilTurtle.Views
{
    public interface IMainView
    {
        string ProcessNameInput {  get; }
        void SetStatus(string message);
        void SetBlockedAppList(List<string> apps);
        event EventHandler BlockAppClicked;
        event EventHandler LoadBlockedAppsClicked;


    }
}
