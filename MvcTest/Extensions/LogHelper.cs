using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace MvcTest.Extensions
{
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror"); 
    }
}