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
    public class AsyncSendData : AsyncData, IAsyncEntryData
    {
        private AtomEntry entry;
        private GDataRequestType type;
        private string contentType;
        private string slugHeader;

        private AsyncSendData(AsyncDataHandler handler, Uri uriToUse, AtomEntry entry, AtomFeed feed,
            SendOrPostCallback callback, object userData, bool parseFeed)
            : base(uriToUse, null, userData, callback, parseFeed)
        {
            this.DataHandler = handler;
            this.entry = entry;
            this.Feed = feed;
        }

        public AsyncSendData(AsyncDataHandler handler, Uri uriToUse, AtomEntry entry, SendOrPostCallback callback, object userData)
            : this(handler, uriToUse, entry, null, callback, userData, false)
        {
        }

        public AsyncSendData(AsyncDataHandler handler, AtomEntry entry, SendOrPostCallback callback, object userData)
            : this(handler, null, entry, null, callback, userData, false)
        {
        }

        public AsyncSendData(AsyncDataHandler handler, Uri uriToUse, AtomFeed feed, SendOrPostCallback callback, object userData)
            : this(handler, uriToUse, null, feed, callback, userData, false)
        {
        }

        public AsyncSendData(AsyncDataHandler handler, Uri uriToUse, Stream stream, GDataRequestType type,
            string contentType, string slugHeader, SendOrPostCallback callback, object userData, bool parseFeed)
            : this(handler, uriToUse, null, null, callback, userData, parseFeed)
        {
            this.DataStream = stream;
            this.type = type;
            this.contentType = contentType;
            this.slugHeader = slugHeader;
        }

        public AtomEntry Entry
        {
            get
            {
                return this.entry;
            }
            set
            {
                this.entry = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
        }

        public string SlugHeader
        {
            get
            {
                return this.slugHeader;
            }
        }

        public GDataRequestType Type
        {
            get
            {
                return this.type;
            }
        }
    }
}
