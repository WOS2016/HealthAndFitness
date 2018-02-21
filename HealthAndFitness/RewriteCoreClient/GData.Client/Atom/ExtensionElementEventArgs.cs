#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
//using Google.GData.Extensions;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>extension element event class
    /// </summary> 
    public class ExtensionElementEventArgs : EventArgs
    {
        private bool discard;
        private AtomBase baseObject;
        private XmlNode node;

        /// <summary>the eventhandler can set this to discard the entry</summary> 
        public bool DiscardEntry
        {
            get { return this.discard; }
            set { this.discard = value; }
        }

        /// <summary>accessor method public XmlNode ExtensionElement</summary> 
        /// <returns> </returns>
        public XmlNode ExtensionElement
        {
            get { return this.node; }
            set { this.node = value; }
        }

        /// <summary>accessor method public AtomBase Base</summary> 
        /// <returns> </returns>
        public AtomBase Base
        {
            get { return this.baseObject; }
            set { this.baseObject = value; }
        }
    }
}
