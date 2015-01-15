using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching
{
    public class LoggerAdapter : Glav.CacheAdapter.Core.Diagnostics.ILogging
    {
        // private static readonly Log.ILogger InnerLogger = Log.LogManager.Unknown;
        public void WriteErrorMessage(string message)
        {
            //InnerLogger.WriteError("Cache Manager", message);
            Common.Loger.LogerManager.Error("Cache Manager" + message);
        }

        public void WriteException(Exception ex)
        {
            //InnerLogger.WriteException(ex);
            Common.Loger.LogerManager.Warn("Cache Manager", ex);
        }

        public void WriteInfoMessage(string message)
        {
            Common.Loger.LogerManager.Info("Cache Manager" + message);
        }
    }
}
