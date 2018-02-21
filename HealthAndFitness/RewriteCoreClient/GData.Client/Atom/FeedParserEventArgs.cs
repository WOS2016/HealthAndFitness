using System;
using System.Xml;
using System.IO;
//using Google.GData.Extensions;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>Parsing event class...
    /// </summary> 
    public class FeedParserEventArgs : EventArgs
    {
        private bool discard;
        private bool creatingEntry;
        private AtomEntry feedEntry;
        private AtomFeed feed;
        private bool done;

        /// <summary>constructor for the feedParser events - this one is only used
        /// to ask for a new entry</summary> 
        public FeedParserEventArgs()
        {
            this.creatingEntry = true;
        }

        /// <summary>constructor for the feedParser events</summary> 
        /// <param name="feed">the feed to use </param>
        /// <param name="entry">the feedentry to use </param> 
        public FeedParserEventArgs(AtomFeed feed, AtomEntry entry)
        {
            this.feedEntry = entry;
            this.feed = feed;
            if (feed == null && entry == null)
            {
                this.done = true;
            }
        }

        /// <summary>the eventhandler can set this to discard the entry</summary>
        public bool DiscardEntry
        {
            get { return this.discard; }
            set { this.discard = value; }
        }

        /// <summary>Read only accessor for done</summary> 
        public bool DoneParsing
        {
            get { return this.done; }
        }

        /// <summary>Read only accessor for creating an entry</summary> 
        public bool CreatingEntry
        {
            get { return this.creatingEntry; }
        }

        /// <summary>the newly created entry obect</summary> 
        /// <returns> </returns>
        public AtomEntry Entry
        {
            get { return this.feedEntry; }
            set { this.feedEntry = value; }
        }

        /// <summary>accessor method public Feed Feed</summary> 
        /// <returns> </returns>
        public AtomFeed Feed
        {
            get { return this.feed; }
        }
    }
}
