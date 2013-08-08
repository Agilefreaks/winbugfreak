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
#if !SILVERLIGHT
                                        Source = exc.Source,
#endif
                                        StackTrace = exc.StackTrace,
                                        AdditionalData = new List<KeyValuePair<string, string>>()
                                    };

            // Populate additional data
            foreach (var errorDataProvider in GlobalConfig.ErrorDataProviders)
            {
                foreach (var keyValuePair in errorDataProvider.GetData())
                {
                    errorReport.AdditionalData.Add(keyValuePair);
                }
            }

            return errorReport;
        }
    }
}
