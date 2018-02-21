using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// this interface indicates that an element is aware of Core and Service
    /// specific version changes. 
    /// </summary>
    /// <returns></returns>
    public interface IVersionAware
    {
        /// <summary>
        /// returns the major version of the protocol this element is using
        /// </summary>
        int ProtocolMajor
        {
            set;
            get;
        }
        /// <summary>
        /// returns the minor version of the protocol this element is using
        /// </summary>
        int ProtocolMinor
        {
            set;
            get;
        }
    }
}
