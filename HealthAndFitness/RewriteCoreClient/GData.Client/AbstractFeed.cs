using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    public abstract class AbstractFeed : AtomFeed, ISupportsEtag
    {

        /// <summary>
        /// Constructor, set's up extension handlers
        /// </summary>
        /// <param name="uriBase">The uri for this cells feed.</param>
        /// <param name="service">The Spreadsheets service.</param>
        protected AbstractFeed(Uri uriBase, IService service) : base(uriBase, service)
        {
            NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewAbstractEntry);
        }


        /// <summary>extension feeds most likely add the GData namespace, so let's 
        /// have a default implementation that does this</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        protected override void AddOtherNamespaces(XmlWriter writer)
        {
            base.AddOtherNamespaces(writer);
            Utilities.EnsureGDataNamespace(writer);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>checks if this is a namespace 
        /// decl that we already added. As the abstract feed adds
        /// the GData namespace, check that one</summary> 
        /// <param name="node">XmlNode to check</param>
        /// <returns>true if this node should be skipped </returns>
        //////////////////////////////////////////////////////////////////////
        protected override bool SkipNode(XmlNode node)
        {
            if (base.SkipNode(node))
            {
                return true;
            }

            return (node.NodeType == XmlNodeType.Attribute &&
                    node.Name.StartsWith("xmlns") &&
                   (String.Compare(node.Value, BaseNameTable.gNamespace) == 0));
        }


        /// <summary>
        /// Eventhandling. Called when a new entry is parsed.
        /// </summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        protected void OnParsedNewAbstractEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry)
            {
                e.Entry = CreateFeedEntry();
            }
        }

        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public abstract AtomEntry CreateFeedEntry();

        private string eTag;
        /////////////////////////////////////////////////////////////////////
        /// <summary>returns this entries etag, if any
        /// This is a protocol version 2 feature
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        public string Etag
        {
            get
            {
                return eTag;
            }
            set
            {
                eTag = value;
            }
        }



    }
}
