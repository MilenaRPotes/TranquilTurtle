using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json; 

namespace TranquilTurtle.Services
{
    public static class SettingsService
    {
        private static readonly string FilePath = "blocked_apps.json";

        public static void SaveBlockedApps(List<string> apps) 
        { 
            var json = JsonSerializer.Serialize(apps);
            File.WriteAllText(FilePath, json);
        
        }

        public static List<string> LoadBlockedApps()
        {
            if (!File.Exists(FilePath)) 
                return new List<string>();
        
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

        }

    }
}
