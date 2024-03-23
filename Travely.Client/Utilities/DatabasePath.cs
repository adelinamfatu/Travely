using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.Client.Resources;

namespace Travely.Client.Utilities
{
    public static class DatabasePath
    {
        public static string GetDatabasePath()
        {
            var databaseName = SharedResources.DatabaseFileName;

            string databasePath = string.Empty;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                    databaseName);
            }
            else if(DeviceInfo.Platform == DevicePlatform.iOS)
            {
                databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "..", 
                    "Library",
                    databaseName);
            }

            return databasePath;
        }
    }
}
