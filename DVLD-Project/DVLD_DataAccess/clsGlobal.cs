using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsGlobal
    {
        public static void ExceptionHandling(string message,EventLogEntryType Type)
        {
            string sourceName = "DVLD";
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
            EventLog.WriteEntry(sourceName, message, Type);
        }
    }
}
