using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>the one that creates GDatarequests on the service
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequestFactory
    {
        /// <summary>creation method for GDatarequests</summary> 
        IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget);
        /// <summary>set wether or not to use gzip for new requests</summary>
        bool UseGZip
        {
            get;
            set;
        }

        /// <summary>
        /// indicates that the service should use SSL exclusively
        /// </summary>
        bool UseSSL
        {
            get;
            set;
        }
    }
}
