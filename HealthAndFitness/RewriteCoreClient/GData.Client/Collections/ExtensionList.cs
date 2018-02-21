#region Using directives

#define USE_TRACING

using System;
using System.Collections.Generic;
using System.Collections;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    ///  internal list to override the add and the constructor
    /// </summary>
    /// <returns></returns>
    public class ExtensionList : IList<IExtensionElementFactory>
    {
        IVersionAware container;

        List<IExtensionElementFactory> _list = new List<IExtensionElementFactory>();

        /// <summary>
        /// Return a new collection that is not version aware.
        /// </summary>
        public static ExtensionList NotVersionAware()
        {
            return new ExtensionList(NullVersionAware.Instance);
        }

        /// <summary>
        /// returns an extensionlist that belongs to a version aware
        /// container
        /// </summary>
        /// <param name="container"></param>
        public ExtensionList(IVersionAware container)
        {
            this.container = container;
        }

        /// <summary>
        /// adds value to the extensionlist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>returns the positin in the list after the add</returns>
        public int Add(IExtensionElementFactory value)
        {
            IVersionAware target = value as IVersionAware;

            if (target != null)
            {
                target.ProtocolMajor = this.container.ProtocolMajor;
                target.ProtocolMinor = this.container.ProtocolMinor;
            }
            if (value != null)
            {
                _list.Add(value);
            }
            return _list.Count - 1;
            //return _list.IndexOf(value);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="item"></param>
        public int IndexOf(IExtensionElementFactory item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, IExtensionElementFactory item)
        {
            _list.Insert(index, item);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="index"></param>
        public IExtensionElementFactory this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="item"></param>
        void ICollection<IExtensionElementFactory>.Add(IExtensionElementFactory item)
        {
            Add(item);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="item"></param>
        public bool Contains(IExtensionElementFactory item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IExtensionElementFactory[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(IExtensionElementFactory item)
        {
            return _list.Remove(item);
        }

        /// <summary>
        /// removes a factory defined by namespace and local name
        /// </summary>
        /// <param name="ns">namespace of the factory</param>
        /// <param name="name">local name of the factory</param>
        public bool Remove(string ns, string name)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].XmlNameSpace == ns && _list[i].XmlName == name)
                {
                    _list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        public IEnumerator<IExtensionElementFactory> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// default overload, see base class
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

}
