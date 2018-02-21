using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// Helper object to walk the tree for the dirty flag.
    /// </summary> 
    public class BaseIsDirty : IBaseWalkerAction
    {
        /// <summary>Walker action. Just gets a property.</summary> 
        /// <param name="atom">object to set the property on</param>
        /// <returns>the value of the dirty flag</returns>
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom");
            }
            return atom.Dirty;
        }
    }
}
