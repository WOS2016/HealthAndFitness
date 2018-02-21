#region Using directives

#define USE_TRACING

using System;
using System.Collections.Generic;
using System.Collections;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// Generic collection base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomCollectionBase<T> : IList<T>
    {
        /// <summary>
        /// the internal list object that is used
        /// </summary>
        protected List<T> List = new List<T>();
        /// <summary>standard typed accessor method </summary> 
        public virtual T this[int index]
        {
            get
            {
                return (T)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>standard typed accessor method </summary> 
        public virtual void Add(T value)
        {
            List.Add(value);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public virtual void Clear()
        {
            List.Clear();
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        public virtual void CopyTo(T[] arr, int index)
        {
            List.CopyTo(arr, index);
        }

        /// <summary>standard typed accessor method </summary> 
        /// <param name="value"></param>
        public virtual int IndexOf(T value)
        {
            return (List.IndexOf(value));
        }
        /// <summary>standard typed accessor method </summary> 
        /// <param name="index"></param>
        /// <param name="value"></param>
        public virtual void Insert(int index, T value)
        {
            List.Insert(index, value);
        }
        /// <summary>standard typed accessor method </summary> 
        /// <param name="value"></param>
        public virtual bool Remove(T value)
        {
            return List.Remove(value);
        }
        /// <summary>standard typed accessor method </summary> 
        /// <param name="value"></param>
        public virtual bool Contains(T value)
        {
            // If value is not of type AtomPerson, this will return false.
            return (List.Contains(value));
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public virtual int Count
        {
            get
            {
                return List.Count;
            }
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }



    }
}
