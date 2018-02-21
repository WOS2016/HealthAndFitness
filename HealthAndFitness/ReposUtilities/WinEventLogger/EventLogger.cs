using System.IO;
using System.Diagnostics;

namespace ReposUtilities.Log
{
    public class EventLogger : LogBase
    {
        public override void Log(string message)
        {
            lock (LockObj)
            {
                //var eventLog = new EventLog();
                //if (!System.Diagnostics.EventLog.SourceExists("DemoSource"))
                //{
                //    System.Diagnostics.EventLog.CreateEventSource("DemoSource", "DemoLog");
                //}
                //// configure the event log instance to use this source name
                //eventLog.Source = "DemoSource";
                //eventLog.Log = "DemoLog";
                //eventLog.WriteEntry(message);

                // directly use Application, because some of server cannot allow to create "EventSource"
                //using (EventLog eventLog = new EventLog("Application"))
                //{
                //    eventLog.Source = "Application";
                //    eventLog.WriteEntry(message, EventLogEntryType.Information, 101, 1);
                //}
            }

        }
    }

    public class DataBaseLogger : LogBase
    {
        string _connectionString = string.Empty;

        public override void Log(string message)
        {
            lock (LockObj)
            {
                //Code to log data to the database

            }
        }
    }

    public class FileLogger : LogBase
    {
        public string FilePath = @"c:\test\DemoLog.txt";

        public override void Log(string message)
        {
            lock (LockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(FilePath))
                {

                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }
    }

}
