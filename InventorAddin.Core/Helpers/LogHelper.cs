using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace InventorAddin.Core.Helpers
{
    public static class LogHelper
    {
        private static Logger _log;

        private static void InitLoggerConfiguration()
        {
            string currentUser = WindowsIdentity.GetCurrent().Name;

            _log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Users\" + currentUser + @"\AppData\Roaming\Autodesk\Globalsoft\Log\log_.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
        }

        private static void Info(string message)
        {
            if (_log == null)
            {
                InitLoggerConfiguration();
            }

            _log.Information(message);
        }

        private static void Warn(string message)
        {
            if (_log == null)
            {
                InitLoggerConfiguration();
            }

            _log.Warning(message);
        }

        private static void Error(string message)
        {
            if (_log == null)
            {
                InitLoggerConfiguration();
            }

            _log.Error(message);
        }
    }
}
