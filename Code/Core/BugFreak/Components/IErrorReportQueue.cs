using System;
using BugFreak.Collections;

namespace BugFreak.Components
{
    public interface IErrorReportQueue
    {
        event EventHandler<ObservableList<ErrorReport>.ListChangedEventArgs> ItemAdded;

        event EventHandler<ObservableList<ErrorReport>.ListChangedEventArgs> ItemRemoved;

        void Enqueue(ErrorReport errorReport);

        ErrorReport Dequeue();
    }
}