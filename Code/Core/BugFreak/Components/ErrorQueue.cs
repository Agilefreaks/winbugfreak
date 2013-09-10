namespace BugFreak.Components
{
    using System.Linq;
    using global::BugFreak.Collections;

    public class ErrorQueue : ObservableList<PendingReport>, IErrorQueue
    {
        private readonly object _lock = new object();

        public void Enqueue(PendingReport pendingReport)
        {
            lock (_lock)
            {
                Add(pendingReport);
            }
        }

        public PendingReport Dequeue()
        {
            PendingReport item;

            lock (_lock)
            {
                item = this.FirstOrDefault();

                if (item != null)
                {
                    Remove(item);
                }
            }

            return item;
        }
    }
}
