using System;
using System.Collections;
using System.Collections.Generic;

namespace Klocman.Collections
{
    /// <summary>
    ///     Generic list with an ListChanged event that fires whenever items get added or removed (not modified).
    /// </summary>
    //[Serializable]
    public class ObservedList<T> : IList<T>
    {
        private readonly List<T> _itemList = new List<T>();

        public virtual void Add(T item)
        {
            OnListChangedEvent();
            _itemList.Add(item);
        }

        public virtual bool Remove(T item)
        {
            OnListChangedEvent();
            return _itemList.Remove(item);
        }

        public virtual void RemoveAt(int index)
        {
            OnListChangedEvent();
            _itemList.RemoveAt(index);
        }

        public virtual void Insert(int index, T item)
        {
            OnListChangedEvent();
            _itemList.Insert(index, item);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return _itemList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Clear()
        {
            OnListChangedEvent();
            _itemList.Clear();
        }

        public virtual bool Contains(T item)
        {
            return _itemList.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            _itemList.CopyTo(array, arrayIndex);
        }

        public virtual int Count => _itemList.Count;

        public virtual bool IsReadOnly => false;

        public virtual T this[int index]
        {
            get { return _itemList[index]; }
            set { _itemList[index] = value; }
        }

        public virtual int IndexOf(T item)
        {
            return _itemList.IndexOf(item);
        }

        public event Action ListChanged;

        public void OnListChangedEvent()
        {
            var handler = ListChanged;
            handler?.Invoke();
        }

        public void AddRange(IEnumerable<T> items)
        {
            var any = false;

            foreach (var item in items)
            {
                _itemList.Add(item);
                any = true;
            }

            if(any)
                OnListChangedEvent();
        }
    }
}