namespace BugFreak
{
    using System;
    using System.Collections.Generic;

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
#if !(SILVERLIGHT || WinRT)
                                        Source = exc.Source,
#endif
                                        StackTrace = exc.StackTrace,
                                        AdditionalData = GetAdditionalData(exc)
                                    };

            // Populate additional data from providers
            foreach (var errorDataProvider in GlobalConfig.ErrorDataProviders)
            {
                foreach (var keyValuePair in errorDataProvider.GetData())
                {
                    errorReport.AdditionalData.Add(keyValuePair);
                }
            }

            return errorReport;
        }

        private static List<KeyValuePair<string, string>> GetAdditionalData(Exception exc)
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach (var key in exc.Data.Keys)
            {
                result.Add(new KeyValuePair<string, string>(key.ToString(), exc.Data[key].ToString()));
            }

            return result;
        }
    }
}
