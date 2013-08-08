using System;
using System.Text;

namespace BugFreak.Components
{
    public class FormErrorReportSerializer : IErrorReportSerializer
    {
        private const string ContentType = "application/x-www-form-urlencoded";
        private const string Format = "{0}={1}";
        private const string AdditionalDataFormat = "additionalData[{0}]={1}";
        private const string Separator = "&";

        public string GetContentType()
        {
            return ContentType;
        }

        public string Serialize(ErrorReport report)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format(Format, "message", Encode(report.Message)));
            stringBuilder.Append(Separator);
            stringBuilder.Append(string.Format(Format, "source", Encode(report.Source)));
            stringBuilder.Append(Separator);
            stringBuilder.Append(string.Format(Format, "stackTrace", Encode(report.StackTrace)));

            foreach (var data in report.AdditionalData)
            {
                stringBuilder.Append(Separator);
                stringBuilder.Append(string.Format(AdditionalDataFormat, data.Key, data.Value));
            }

            return stringBuilder.ToString();
        }

        private string Encode(string source)
        {
            return string.IsNullOrEmpty(source)
                       ? string.Empty
                       : Uri.EscapeDataString(source);
        }
    }
}