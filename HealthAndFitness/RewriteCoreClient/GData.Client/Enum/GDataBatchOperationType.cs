using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>enum to define the GDataBatchOperationType...</summary> 
    public enum GDataBatchOperationType
    {
        /// <summary>this is an insert operatoin</summary> 
        insert,
        /// <summary>this is an update operation</summary> 
        update,
        /// <summary>this is a delete operation</summary> 
        delete,
        /// <summary>this is a query operation</summary>
        query,
        /// <summary>the default (a no-op)</summary>
        Default
    }
}
