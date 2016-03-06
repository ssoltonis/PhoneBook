using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using NLog;

namespace PhoneBook.Common
{
    public enum LogLevel
    {
        Debug,
        Info,
        Error    
    }

    public static class Logging
    {
        public static Logger InfoLogger { get; }

        static Logging()
        {
            InfoLogger = LogManager.GetLogger("InfoLogger");
        }

        public static void Info(string message)
        {
            var prefix = SetMessageInfo(LogLevel.Info);
            InfoLogger.Info($"{prefix}: {message}");
        }

        public static void Debug(string message)
        {
            var prefix = SetMessageInfo(LogLevel.Debug);
            InfoLogger.Debug($"{prefix}: {message}");
        }

        public static void Error(string message)
        {
            var prefix = SetMessageInfo(LogLevel.Error);
            InfoLogger.Error($"{prefix}: {message}");
        }

        public static string SetMessageInfo(LogLevel level)
        {
            var sessionId = HttpContext.Current.Session.SessionID;
            var userId = HttpContext.Current.Session["UserId"].ToString();

            if (string.IsNullOrEmpty(sessionId))
                sessionId = $"{"-",24}";

            if (string.IsNullOrEmpty(userId))
                userId = "NULL";

            return $"| {sessionId} | {userId} | [{level.ToString().ToUpper()}]";
        }
    }
}