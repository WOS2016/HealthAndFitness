using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// the enum used for Get of T requests
    /// </summary>
    public enum FeedRequestType
    {
        /// <summary>
        /// returns the next feed chunk if there is more data
        /// </summary>
        Next,
        /// <summary>
        /// returns the previous feed chunk if there is data before
        /// </summary>
        Prev,
        /// <summary>
        /// refreshes the actual feed chunk by going to the server and retrieving it again
        /// </summary>
        Refresh
    }
}
