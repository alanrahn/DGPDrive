
using System;
using System.Diagnostics;
using System.Configuration;


namespace ApiUtil
{
    public class EventLogger
    {
        static string _eventSrc = ConfigurationManager.AppSettings["EventSource"].ToString();
        static int _eventID = Convert.ToInt32(ConfigurationManager.AppSettings["EventID"].ToString());

        public static void LogInfo(string errSrc, string eventMsg, string eventID)
        {
            int evtid = 0;
            if (eventID != null && eventID != "")
            {
                bool OK = int.TryParse(eventID, out evtid);
            }

            if (evtid == 0) evtid = _eventID;

            // log message to the event viewer
            string msg = errSrc + " Information\n" + eventMsg;
            EventLog.WriteEntry(_eventSrc, msg, EventLogEntryType.Information, evtid);
        }

        public static void LogError(string errSrc, string eventMsg, string eventID)
        {
            int evtid = 0;
            if (eventID != null && eventID != "")
            {
                bool OK = int.TryParse(eventID, out evtid);
            }

            if (evtid == 0) evtid = _eventID;

            // log message to the event viewer
            string msg = errSrc + " Error\n" + eventMsg;
            EventLog.WriteEntry(_eventSrc, msg, EventLogEntryType.Warning, evtid);
        }

        public static void LogException(string excSrc, Exception exc, string eventID)
        {
            int evtid = 0;
            if (eventID != null && eventID != "")
            {
                bool OK = int.TryParse(eventID, out evtid);
            }

            if (evtid == 0) evtid = _eventID;

            // get info from exception
            string eventMsg = "Message: " + exc.Message + "\n";
            if (exc.InnerException != null) eventMsg += "Inner Message: " + exc.InnerException.Message + "\n";
            eventMsg += "Source: " + exc.Source + "\n" +
                       "Target: " + exc.TargetSite + "\n" +
                       "StackTrace: " + exc.StackTrace + "\n";

            // log to event viewer
            string msg = excSrc + " Exception\n" + eventMsg;
            EventLog.WriteEntry(_eventSrc, msg, EventLogEntryType.Error, evtid);
        }
    }
}
