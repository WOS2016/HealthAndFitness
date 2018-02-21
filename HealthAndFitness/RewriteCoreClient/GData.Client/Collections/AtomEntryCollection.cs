using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>standard typed collection based on 1.1 framework for FeedEntries
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AtomEntryCollection : AtomCollectionBase<AtomEntry>
    {
        /// <summary>holds the owning feed</summary> 
        private AtomFeed feed;

        /// <summary>private default constructor</summary> 
        private AtomEntryCollection()
        {
        }
        /// <summary>constructor</summary> 
        public AtomEntryCollection(AtomFeed feed)
            : base()
        {
            this.feed = feed;
        }

        /// <summary>Fins an atomEntry in the collection 
        /// based on it's ID. </summary> 
        /// <param name="value">The atomId to look for</param> 
        /// <returns>Null if not found, otherwise the entry</returns>
        public AtomEntry FindById(AtomId value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            foreach (AtomEntry entry in List)
            {
                if (entry.Id.AbsoluteUri == value.AbsoluteUri)
                {
                    return entry;
                }
            }
            return null;
        }


        /// <summary>standard typed accessor method </summary> 
        public override AtomEntry this[int index]
        {
            get
            {
                return ((AtomEntry)List[index]);
            }
            set
            {
                if (value != null)
                {
                    if (value.Feed == null || value.Feed != this.feed)
                    {
                        value.setFeed(this.feed);
                    }
                }
                List[index] = value;
            }
        }

        /// <summary>standard typed add method </summary> 
        public override void Add(AtomEntry value)
        {
            if (value != null)
            {
                if (this.feed != null && value.Feed == this.feed)
                {
                    // same object, already in here. 
                    throw new ArgumentException("The entry is already part of this collection");
                }
                value.setFeed(this.feed);
                // old code
                /*
                // now we need to see if this is the same feed. If not, copy
                if (AtomFeed.IsFeedIdentical(value.Feed, this.feed) == false)
                {
                    AtomEntry newEntry = AtomEntry.ImportFromFeed(value);
                    newEntry.setFeed(this.feed);
                    value = newEntry;
                }
                */
                // from now on, we will only ADD the entry to this collection and change it's 
                // ownership. No more auto-souce creation. There is an explicit method for this
                value.ProtocolMajor = this.feed.ProtocolMajor;
                value.ProtocolMinor = this.feed.ProtocolMinor;
            }
            base.Add(value);
        }

        /// <summary>
        /// takes an existing atomentry object and either copies it into this feed collection,
        /// or moves it by creating a source element and copying it in here if the value is actually
        /// already part of a collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AtomEntry CopyOrMove(AtomEntry value)
        {
            if (value != null)
            {
                if (value.Feed == null)
                {
                    value.setFeed(this.feed);
                }
                else
                {
                    if (this.feed != null && value.Feed == this.feed)
                    {
                        // same object, already in here. 
                        throw new ArgumentException("The entry is already part of this collection");
                    }
                    // now we need to see if this is the same feed. If not, copy
                    if (!AtomFeed.IsFeedIdentical(value.Feed, this.feed))
                    {
                        AtomEntry newEntry = AtomEntry.ImportFromFeed(value);
                        newEntry.setFeed(this.feed);
                        value = newEntry;
                    }
                }
                value.ProtocolMajor = this.feed.ProtocolMajor;
                value.ProtocolMinor = this.feed.ProtocolMinor;
            }
            base.Add(value);
            return value;
        }
    }
    /////////////////////////////////////////////////////////////////////////////
}
