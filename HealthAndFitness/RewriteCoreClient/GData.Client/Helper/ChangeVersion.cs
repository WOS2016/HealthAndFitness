using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>Helper to walk the tree and set the versioninformation</summary> 
    internal class ChangeVersion : IBaseWalkerAction
    {
        private VersionInformation v;

        /// <summary>Constructor.</summary> 
        /// <param name="v">the versioninformation to pass </param>
        internal ChangeVersion(IVersionAware v)
        {
            this.v = new VersionInformation(v);
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
            atom.SetVersionInfo(v);
            return false;
        }
    }
}
