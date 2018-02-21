using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// Helper object to walk the tree for the dirty flag.
    /// </summary> 
    public class BaseMarkDirty : IBaseWalkerAction
    {
        /// <summary>Holds if set/unset to dirty.</summary> 
        private bool flag;

        /// <summary>Constructor.</summary> 
        /// <param name="flag">indicates the value to pass </param>
        internal BaseMarkDirty(bool flag)
        {
            this.flag = flag;
        }

        /// <summary>Walker action. Just sets a property.</summary> 
        /// <param name="atom">object to set the property on </param>
        /// <returns> always false, indicating to walk the whole tree</returns>
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom");
            }
            atom.Dirty = this.flag;
            return false;
        }
    }
}
