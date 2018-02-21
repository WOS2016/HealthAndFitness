#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>AtomBaselink is an intermediate object that adds the URI property
    /// used as the parent class for a lot of other objects (like atomlink, atomicon, etc)
    /// </summary> 
    public abstract class AtomBaseLink : AtomBase
    {
        /// <summary>holds the string rep</summary> 
        private AtomUri uriString;

        /// <summary>accessor method public string Uri</summary> 
        /// <returns> </returns>
        public AtomUri Uri
        {
            get { return this.uriString; }
            set { this.Dirty = true; this.uriString = value; }
        }

        /// <summary>public Uri AbsoluteUri</summary>         
        public string AbsoluteUri
        {
            get
            {
                if (this.Uri == null)
                {
                    return null;
                }
                return GetAbsoluteUri(this.Uri.ToString());
            }
        }

        #region Persistence overloads
        /// <summary>saves the inner state of the element</summary> 
        /// <param name="writer">the xmlWriter to save into </param>
        protected override void SaveInnerXml(XmlWriter writer)
        {
            base.SaveInnerXml(writer);
            WriteEncodedString(writer, this.Uri);
        }

        /// <summary>figures out if this object should be persisted</summary> 
        /// <returns> true, if it's worth saving</returns>
        public override bool ShouldBePersisted()
        {
            if (!base.ShouldBePersisted())
            {
                return Utilities.IsPersistable(this.uriString);
            }
            return true;
        }

        #endregion
    }
}
