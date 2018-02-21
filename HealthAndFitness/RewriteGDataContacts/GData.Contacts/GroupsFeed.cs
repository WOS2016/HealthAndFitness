using System;
using RewriteCoreClient.GData.Client;

namespace RewriteGDataContacts.GData.Contacts
{

    /// <summary>
    ///      A groups feed is a private read/write feed that can be used to view and manage a user's
    ///      groups. The URI for the feed is as follows:
    ///      http://www.google.com/m8/feeds/groups/userID/base
    ///
    ///      For example, the contacts feed for user liz@gmail.com would have the following URI:
    ///      http://www.google.com/m8/feeds/groups/liz%40gmail.com/base
    ///
    ///      Since the groups feed is private, you can access it only by using an authenticated
    ///      request. That is, the request must contain an authentication token for the user whose
    ///      contacts you want to retrieve.
    /// </summary>
    public class GroupsFeed : AbstractFeed
    {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public GroupsFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        /// <summary>
        ///  default constructor with user name
        /// </summary>
        /// <param name="username">the username for the contacts feed</param>
        /// <param name="iService">the Service to use</param>
        public GroupsFeed(String username, IService iService)
            : base(new Uri(ContactsQuery.CreateGroupsUri(username)), iService)
        {
        }
        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new GroupEntry();
        }

        /// <summary>
        /// gets called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            base.HandleExtensionElements(e, parser);
        }
    }
}
