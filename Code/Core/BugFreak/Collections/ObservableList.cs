using System;
using System.Collections.Generic;
using System.Collections;

namespace Bugfreak.Collections
{
    public class ObservableList<T> : IList<T>
    {
        private readonly IList<T> _internalList;

        public class ListChangedEventArgs : EventArgs
        {
            public int Index { get; private set; }

            public T Item { get; private set; }

            public ListChangedEventArgs(int index, T item)
            {
                Index = index;
                Item = item;
            }
        }
        
        /// <summary>
        /// Fired whenever list item has been changed, added or removed or when list has been cleared
        /// </summary>
        public event EventHandler<ListChangedEventArgs> ListChanged;

        /// <summary>
        /// Fired when list item has been removed from the list
        /// </summary>
        public event EventHandler<ListChangedEventArgs> ItemRemoved;
        /// <summary>
        /// Fired when item has been added to the list
        /// </summary>
        public event EventHandler<ListChangedEventArgs> ItemAdded;
        /// <summary>
        /// Fired when list is cleared
        /// </summary>
        public event EventHandler<EventArgs> ListCleared;

        public ObservableList()
        {
            _internalList = new List<T>();
        }

        public ObservableList(IList<T> list)
        {
            _internalList = list;
        }

        public ObservableList(IEnumerable<T> collection)
        {
            _internalList = new List<T>(collection);
        }

        protected virtual void OnItemAdded(ListChangedEventArgs e)
        {
            if (ItemAdded != null)
                ItemAdded(this, e);
        }

        protected virtual void OnItemRemoved(ListChangedEventArgs e)
        {
            if (ItemRemoved != null)
                ItemRemoved(this, e);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (ListChanged != null)
                ListChanged(this, e);
        }

        protected virtual void OnListCleared(EventArgs e)
        {
            if (ListCleared != null)
                ListCleared(this, e);
        }

        public int IndexOf(T item)
        {
            return _internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _internalList.Insert(index, item);
            OnListChanged(new ListChangedEventArgs(index, item));
        }

        public void RemoveAt(int index)
        {
            T item = _internalList[index];
            _internalList.Remove(item);
            OnListChanged(new ListChangedEventArgs(index, item));
            OnItemRemoved(new ListChangedEventArgs(index, item));
        }

        public T this[int index]
        {
            get { return _internalList[index]; }
            set
            {
                _internalList[index] = value;
                OnListChanged(new ListChangedEventArgs(index, value));
            }
        }

        public void Add(T item)
        {
            _internalList.Add(item);
            OnListChanged(new ListChangedEventArgs(_internalList.IndexOf(item), item));
            OnItemAdded(new ListChangedEventArgs(_internalList.IndexOf(item), item));
        }

        public void Clear()
        {
            _internalList.Clear();
            OnListCleared(new EventArgs());
        }

        public bool Contains(T item)
        {
            return _internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return IsReadOnly; }
        }

        public bool Remove(T item)
        {
            lock (this)
            {
                var index = _internalList.IndexOf(item);
                if (!_internalList.Remove(item))
                {
                    return false;
                }

                OnListChanged(new ListChangedEventArgs(index, item));
                OnItemRemoved(new ListChangedEventArgs(index, item));
                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_internalList).GetEnumerator();
        }
    }
}