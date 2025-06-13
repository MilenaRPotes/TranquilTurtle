using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquilTurtle.Services
{
    public static class AppBlockerService
    {
        public static bool KillApp(string processName)
        {
            bool anyClosed = false;

            try 
            {
            var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes) 
                { 
                    process.Kill();
                    anyClosed = true;
                }
            
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine($"Error closing app: {ex.Message}");
            }
            return anyClosed;
        }
    }
}
