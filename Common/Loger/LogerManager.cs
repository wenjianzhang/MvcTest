using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Loger
{
    public class LogerManager
    {
        /// <summary>
        /// To Email
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void InfoToEmail(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("SmtpAppender");
            log.Info(message, ex);
        }

        /// <summary>
        /// To Email
        /// </summary>
        /// <param name="message"></param>
        public static void InfoToEmail(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("SmtpAppender");
            log.Info(message);
        }

        public static void Info(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Info(message, ex);
        }

        public static void Info(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Info(message);
        }

        public static void Error(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Error(message, ex);
        }
        public static void Error(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Error(message);
        }

        public static void Warn(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Warn(message, ex);
        }

        public static void Warn(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Warn(message);
        }

        public static void Debug(string message, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Debug(message, ex);
        }

        public static void Debug(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            log.Debug(message);
        }
    }
}
