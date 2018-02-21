using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{ 
    //////////////////////////////////////////////////////////////////////
    /// <summary>enum to describe the different operations on the GDataRequest
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum GDataRequestType
    {
        /// <summary>The request is used for query</summary>
        Query,
        /// <summary>The request is used for an insert</summary>
        Insert,
        /// <summary>The request is used for an update</summary>
        Update,
        /// <summary>The request is used for a delete</summary>
        Delete,
        /// <summary>This request is used for a batch operation</summary>
        Batch
    }
}
