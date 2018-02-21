using System;
using System.Collections.Generic;
using System.Text;

using RewriteGDataContacts.GData.Contacts;

namespace RewriteGDataContacts.Contacts
{
    public class Group : ContactBase
    {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new GroupEntry();
            }
        }

        /// <summary>
        /// readonly accessor for the YouTubeEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public GroupEntry GroupEntry
        {
            get
            {
                return this.AtomEntry as GroupEntry;
            }
        }

        /// <summary>
        /// returns the systemgroup id, if this groupentry represents 
        /// a system group. 
        /// The values of the system group ids corresponding to these 
        /// groups can be found in the Reference Guide for the Contacts Data API.
        /// Currently the values can be Contacts, Friends, Family and Coworkers
        /// </summary>
        /// <returns>the system group or null</returns>
        public string SystemGroup
        {
            get
            {
                EnsureInnerObject();
                return this.GroupEntry.SystemGroup;
            }
        }

    }
}
