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
    /// <summary>
    /// EventArgument class for service level events during parsing
    /// </summary>
    public class ServiceEventArgs : EventArgs
    {
        private AtomFeed feedObject;
        private IService service;
        private Uri uri;

        /// <summary>
        /// constructor. Takes the URI and the service this event applies to
        /// </summary>
        /// <param name="uri">URI currently executed</param>
        /// <param name="service">service object doing the execution</param>
        public ServiceEventArgs(Uri uri, IService service)
        {
            this.service = service;
            this.uri = uri;
        }

        /// <summary>the feed to be created. If this is NULL, a service 
        /// will create a DEFAULT atomfeed</summary> 
        /// <returns> </returns>
        public AtomFeed Feed
        {
            get { return this.feedObject; }
            set { this.feedObject = value; }
        }

        /// <summary>the service to be used for the feed to be created.</summary> 
        /// <returns> </returns>
        public IService Service
        {
            get { return this.service; }
        }

        /// <summary>the Uri to be used</summary> 
        /// <returns> </returns>
        public Uri Uri
        {
            get { return this.uri; }
        }
    }
}
