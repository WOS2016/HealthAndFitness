using System;
using RewriteCoreClient.GData.Client;
using Rewrite.GData.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class ContactsFeed : AbstractFeed
    {
        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public ContactsFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        /// <summary>
        ///  default constructor with user name
        /// </summary>
        /// <param name="username">the username for the contacts feed</param>
        /// <param name="iService">the Service to use</param>
        public ContactsFeed(String username, IService iService)
            : base(new Uri(ContactsQuery.CreateContactsUri(username)), iService)
        {
        }
        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new ContactEntry();
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
