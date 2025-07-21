using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquilTurtle.Services;

namespace TranquilTurtle.Models
{
    public class BlockedAppsModel
    {
        private List<string> blockedApps;

        public BlockedAppsModel() 
        { 
            blockedApps = SettingsService.LoadBlockedApps();
        }

        public List<string> GetBlockedApps() => blockedApps;

        public void AddBlockedApp(string app) 
        { 
            if(!blockedApps.Contains(app)) 
            { 
                blockedApps.Add(app);
                SettingsService.SaveBlockedApps(blockedApps);
            }
        
        
        }

    }
}
