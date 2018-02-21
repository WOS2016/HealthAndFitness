using System;
using RewriteCoreClient.GData.Client;

namespace RewriteGDataContacts.GData.Contacts
{
    public class ContactsService : Service
    {
        /// <summary>The Calendar service name</summary> 
        public const string GContactService = "cp";

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationName">the application name</param>
        public ContactsService(string applicationName)
            : base(GContactService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public ContactsFeed Query(ContactsQuery feedQuery)
        {
            return base.Query(feedQuery) as ContactsFeed;
        }

        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public GroupsFeed Query(GroupsQuery feedQuery)
        {
            return base.Query(feedQuery) as GroupsFeed;
        }

        /// <summary>
        /// by default all services now use version 1 for the protocol.
        /// this needs to be overridden by a service to specify otherwise. 
        /// Contacts uses version 3
        /// </summary>
        /// <returns></returns>
        protected override void InitVersionInformation()
        {
            this.ProtocolMajor = VersionDefaults.VersionThree;
        }

        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Contacts Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            if (e.Uri.AbsolutePath.IndexOf("/m8/feeds/groups/") != -1)
            {
                e.Feed = new GroupsFeed(e.Uri, e.Service);
            }
            else
            {
                e.Feed = new ContactsFeed(e.Uri, e.Service);
            }
        }
    }
}
