using System.Linq;
using Bugfreak.Collections;

namespace Bugfreak.Components
{
    public class ErrorReportQueue : ObservableList<ErrorReport>, IErrorReportQueue
    {
        public void Enqueue(ErrorReport errorReport)
        {
            Add(errorReport);
        }

        public ErrorReport Dequeue()
        {
            var item = this.FirstOrDefault();

            if (item != null)
            {
                Remove(item);
            }

            return item;
        }
    }
}
