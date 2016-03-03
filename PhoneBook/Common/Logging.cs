using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace PhoneBook.Common
{
    public static class Logging
    {
        public static Logger InfoLogger { get; }

        static Logging()
        {
            InfoLogger = LogManager.GetLogger("InfoLogger");
        }

        public static void Info(string message)
        {
            InfoLogger.Info(message);
        }

        public static void Debug(string message)
        {
            InfoLogger.Debug(message);
        }

        public static void Error(string message)
        {
            InfoLogger.Error(message);
        }
    }
}