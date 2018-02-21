#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Collections.Specialized;


#endregion

namespace RewriteCoreClient.GData.Client
{
    public class AsyncDeleteData : AsyncData, IAsyncEntryData
    {
        private AtomEntry _entry;
        private readonly bool _permanentDelete;

        public AsyncDeleteData(AtomEntry entry, bool permanentDelete, object userData, SendOrPostCallback callback)
            : base(null, userData, callback)
        {
            _entry = entry;
            _permanentDelete = permanentDelete;
        }

        public bool PermanentDelete
        {
            get
            {
                return _permanentDelete;
            }
        }

        public AtomEntry Entry
        {
            get
            {
                return _entry;
            }
            set
            {
                _entry = value;
            }
        }
    }
}
