namespace BugFreak.Components
{
    using System;
    using global::BugFreak.Collections;

    public interface IErrorQueue
    {
        event EventHandler<ObservableList<PendingReport>.ListChangedEventArgs> ItemAdded;

        event EventHandler<ObservableList<PendingReport>.ListChangedEventArgs> ItemRemoved;

        void Enqueue(PendingReport exception);

        PendingReport Dequeue();
    }
}