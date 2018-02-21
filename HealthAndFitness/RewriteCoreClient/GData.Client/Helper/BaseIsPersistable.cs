
#define USE_TRACING

using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>Helper class, mainly used to walk the tree for the dirty flag.</summary> 
    public class BaseIsPersistable : IBaseWalkerAction
    {
        /// <summary>Walker action. Just gets a property.</summary> 
        /// <param name="atom">object to set the property on </param>
        /// <returns>returns the value of the ShouldBePersisted() of the object</returns>
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom");
            }

            bool f = atom.ShouldBePersisted();
            return f;
        }
    }
}
