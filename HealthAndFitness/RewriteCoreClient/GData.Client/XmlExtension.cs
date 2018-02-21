using System;
using System.Collections;
using System.Text;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;
using System.Collections.Generic;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// placeholder object for an unknown XML extension element
    /// </summary>
    public class XmlExtension : IExtensionElementFactory
    {
        private XmlNode unknownChild;

        #region overloaded for persistence

        /// <summary>
        /// Default constructor for an XmlExtension, just takes
        /// the xmlnode that should be used in the extension element
        /// </summary>
        /// <param name="node"></param>
        public XmlExtension(XmlNode node)
        {
            this.unknownChild = node;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public XmlNode Node</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public XmlNode Node
        {
            get { return this.unknownChild; }
            set { this.unknownChild = value; }
        }
        // end of accessor public XmlNode Node

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlName
        {
            get
            {
                return this.unknownChild == null ? null : this.unknownChild.LocalName;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlNameSpace
        {
            get
            {
                return this.unknownChild == null ? null : this.unknownChild.NamespaceURI;
            }

        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string XmlPrefix
        {
            get
            {
                return this.unknownChild == null ? null : this.unknownChild.Prefix;
            }
        }

        /// <summary>
        /// debugging helper
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + " for: " + XmlNameSpace + "- " + XmlName;
        }


        /// <summary>
        /// Allows an XmlExtension to be cast directly into an xmlnode
        /// this should avoid or at least ease code breakage for clients relying on XmlNodes
        /// in the extensionelements
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator XmlNode(XmlExtension x)
        {
            return x.Node;
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses an xml node to create an instance of this  object.</summary> 
        /// <param name="node">the xml parses node, can be NULL</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created IExtensionElement object</returns>
        //////////////////////////////////////////////////////////////////////
        public virtual IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();
            return new XmlExtension(node);
        }

        /// <summary>
        /// Persistence method for the XmlExtension
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public virtual void Save(XmlWriter writer)
        {
            if (this.Node != null)
            {
                this.Node.WriteTo(writer);
            }
        }
        #endregion
    }
}
