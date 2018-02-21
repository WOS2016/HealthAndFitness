#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
//using Google.GData.Extensions;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>Delegate declaration for the parsing eventhandler</summary> 
    public delegate void FeedParserEventHandler(object sender, FeedParserEventArgs e);

    /// <summary>Delegate declaration for the extension eventhandler</summary> 
    public delegate void ExtensionElementEventHandler(object sender, ExtensionElementEventArgs e);


    /// <summary>AtomEntry object, representing an item in the RSS feed
    /// </summary> 
    public abstract class BaseFeedParser
    {

        /// <summary>eventhandler, when the parser created a new feed entry</summary> 
        public event FeedParserEventHandler NewAtomEntry;
        /// <summary>eventhandler, when the parser finds an extension element</summary> 
        public event ExtensionElementEventHandler NewExtensionElement;

        /// <summary>the XmlDoc that we use to hand nodes to, in case of extensions</summary> 
        private XmlDocument doc;

        /// <summary>Read only accessor for doc</summary> 
        protected XmlDocument Document
        {
            get
            {
                if (this.doc == null)
                {
                    this.doc = new XmlDocument();
                }
                return this.doc;
            }
        }

        /// <summary>virtual, starts the parsing process</summary> 
        /// <param name="streamInput">input stream to parse </param>
        /// <param name="feed">the basefeed object that should be set</param>
        public abstract void Parse(Stream streamInput, AtomFeed feed);

        /// <summary>overloaded to make it easier to fire the event</summary>
        protected AtomEntry OnCreateNewEntry()
        {
            FeedParserEventArgs args = new FeedParserEventArgs();
            this.OnNewAtomEntry(args);
            if (args.Entry == null)
            {
                return new AtomEntry();
            }
            return args.Entry;
        }

        /// <summary>overloaded to make it easier to fire the event</summary> 
        /// <param name="newEntry">the new AtomEntry to fire </param>
        protected void OnNewAtomEntry(AtomEntry newEntry)
        {
            FeedParserEventArgs args = new FeedParserEventArgs(null, newEntry);
            this.OnNewAtomEntry(args);
        }

        /// <summary>protected void OnParsingDone()</summary> 
        /// <returns> </returns>
        protected void OnParsingDone()
        {
            FeedParserEventArgs args = new FeedParserEventArgs(null, null);
            this.OnNewAtomEntry(args);
        }

        /// <summary>overloaded to make eventfiring easier</summary> 
        /// <param name="feed"> the new feed to fire</param>
        protected void OnNewAtomEntry(AtomFeed feed)
        {
            FeedParserEventArgs args = new FeedParserEventArgs(feed, null);
            this.OnNewAtomEntry(args);
        }

        /// <summary>eventfiring helper for new extensions</summary> 
        /// <param name="node"> the new node that was found</param>
        /// <param name="baseObject"> the object this node should be added to</param>
        protected void OnNewExtensionElement(XmlNode node, AtomBase baseObject)
        {
            ExtensionElementEventArgs args = new ExtensionElementEventArgs();
            args.ExtensionElement = node;
            args.Base = baseObject;
            if (this.NewExtensionElement != null)
            {
                this.NewExtensionElement(this, args);
            }
            if (!args.DiscardEntry)
            {
                baseObject.ExtensionElements.Add(new XmlExtension(node));
            }
        }

        /// <summary>eventfiring helper for new extensions</summary> 
        /// <param name="reader"> the reader positioned on the extension</param>
        /// <param name="baseObject"> the object this node should be added to</param>
        protected void OnNewExtensionElement(XmlReader reader, AtomBase baseObject)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            Tracing.Assert(baseObject != null, "baseObject should not be null");
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Attribute)
            {
                XmlNode node = this.Document.ReadNode(reader);
                if (node != null)
                {
                    OnNewExtensionElement(node, baseObject);
                }
            }
        }

        /// <summary>protected virtual OnNewAtomEntry( FeedParserEventArgs args)</summary> 
        /// <param name="args"> FeedParserEventArgs, includes the new entry</param>
        /// <returns> </returns>
        protected virtual void OnNewAtomEntry(FeedParserEventArgs args)
        {
            if (this.NewAtomEntry != null)
            {
                this.NewAtomEntry(this, args);
            }
        }

        /// <summary>moves to the next element</summary>
        /// <param name="reader">the xmlreader to skip </param>
        static protected bool NextElement(XmlReader reader)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            while (reader.Read() && reader.NodeType != XmlNodeType.Element) ;
            return !reader.EOF;
        }

        /// <summary>starts with the parent element, and stops when it reaches the same DEPTH again</summary> 
        /// <param name="reader"> the xml reader positioned at the parent element</param>
        /// <param name="depth"> indicates the depth level of the parent where to stop</param>
        /// <returns> true while still inside this level</returns>
        static protected bool NextChildElement(XmlReader reader, ref int depth)
        {
            return Utilities.NextChildElement(reader, ref depth);
        }

        /// <summary>moves to the next element start</summary> 
        /// <param name="reader"> the xml reader positioned somewhere</param>
        /// <returns> true if found, otherwise false (indicating most likely EOF</returns>
        static protected bool MoveToStartElement(XmlReader reader)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (reader.NodeType == XmlNodeType.Attribute || reader.NodeType == XmlNodeType.Text)
            {
                reader.MoveToElement();
            }
            else
            {
                while (reader.NodeType != XmlNodeType.Element && reader.Read() && reader.EOF != true) ;
            }
            return !reader.EOF;
        }
    }
}
