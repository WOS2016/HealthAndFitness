using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// interface to indicate that an element supports an Etag. Currently implemented on AbstractEntry,
    /// AbstractFeed and GDataRequest
    /// </summary>
    public interface ISupportsEtag
    {
        /// <summary>set the etag for updates</summary>
        string Etag
        {
            get;
            set;
        }

    }
}
