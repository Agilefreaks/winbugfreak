﻿using System.Linq;
using BugFreak.Collections;

namespace BugFreak.Components
{
    using global::BugFreak.Collections;

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
