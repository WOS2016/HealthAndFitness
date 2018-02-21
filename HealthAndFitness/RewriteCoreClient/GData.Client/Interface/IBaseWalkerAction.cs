using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to create an action on an item/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IBaseWalkerAction
    {
        /// <summary>the only relevant method here</summary> 
        bool Go(AtomBase atom);
    }
}
