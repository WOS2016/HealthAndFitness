#region Using directives

#define USE_TRACING

using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

#endregion

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to abstract the request/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequest
    {
        /// <summary>get/set for credentials to the service calls. Gets passed through to GDatarequest</summary> 
        GDataCredentials Credentials
        {
            get;
            set;
        }
        /// <summary>set wether or not to use gzip for this request</summary>
        bool UseGZip
        {
            get;
            set;
        }
        /// <summary>set a timestamp for conditional GET</summary>
        DateTime IfModifiedSince
        {
            get;
            set;
        }

        /// <summary>persist the content so if reset</summary>
        AtomBase ContentStore
        {
            get;
            set;
        }

        /// <summary>denotes if it's a batch request</summary>
        bool IsBatch
        {
            get;
            set;
        }

        /// <summary>gets the request stream to write into</summary> 
        Stream GetRequestStream();
        /// <summary>Executes the request</summary> 
        void Execute();
        /// <summary>gets the response stream to read from</summary> 
        Stream GetResponseStream();
    }
}
