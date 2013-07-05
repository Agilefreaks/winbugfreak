using System;
using System.Collections.Generic;
using System.Linq;

namespace BugFreak
{
    public class ErrorReport
    {
        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public List<KeyValuePair<string, string>> AdditionalData { get; set; }

        public ErrorReport()
        {
            AdditionalData = new List<KeyValuePair<string, string>>();
        }

        public static ErrorReport FromException(Exception exc)
        {
            if (exc == null)
            {
                return null;
            }

            var errorReport = new ErrorReport
                                    {
                                        Message = exc.Message,
                                        Source = exc.Source,
                                        StackTrace = exc.StackTrace,
                                        AdditionalData = new List<KeyValuePair<string, string>>()
                                    };

            foreach (var key in exc.Data.Keys.Cast<object>().Where(key => exc.Data[key] != null))
            {
                errorReport.AdditionalData.Add(new KeyValuePair<string, string>(key.ToString(), exc.Data[key].ToString()));
            }

            return errorReport;
        }
    }
}
