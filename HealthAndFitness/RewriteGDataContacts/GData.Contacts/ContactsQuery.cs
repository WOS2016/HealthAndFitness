using System;
using System.Globalization;
using System.Text;
using RewriteCoreClient.GData.Client;

namespace RewriteGDataContacts.GData.Contacts
{
    /// <summary>
    /// A subclass of GroupsQuery, to create an Contacts query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The ContactsQuery supports the following GData parameters:
    /// Name              Description
    /// alt               The type of feed to return, such as atom (the default), rss, or json.
    /// max-results       The maximum number of entries to return. If you want to receive all of
    ///                   the contacts, rather than only the default maximum, you can specify a very 
    ///                   large number for max-results.
    /// start-index       The 1-based index of the first result to be retrieved (for paging).
    /// updated-min       The lower bound on entry update dates.
    /// 
    /// For more information about the standard parameters, see the Google Data APIs protocol reference document.
    /// In addition to the standard query parameters, the Contacts Data API supports the following parameters:
    /// 
    /// Name              Description
    /// orderby           Sorting criterion. The only supported value is lastmodified.
    /// showdeleted       Include deleted contacts in the returned contacts feed. 
    ///                   Deleted contacts are shown as entries that contain nothing but an 
    ///                   atom:id element and a gd:deleted element. 
    ///                   (Google retains placeholders for deleted contacts for 30 days after 
    ///                   deletion; during that time, you can request the placeholders 
    ///                   using the showdeleted query parameter.) Valid values are true or false.
    /// sortorder         Sorting order direction. Can be either ascending or descending.
    /// group	          Constrains the results to only the contacts belonging to the group specified. 
    ///                   Value of this parameter specifies group ID (see also: gContact:groupMembershipInfo).
    /// </summary> 
    public class ContactsQuery : GroupsQuery
    {
        /// <summary>
        /// contacts base URI 
        /// </summary>
        public const string contactsBaseUri = "https://www.google.com/m8/feeds/contacts/";

        private string group;

        /// <summary>
        /// base constructor
        /// </summary>
        public ContactsQuery()
            : base()
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public ContactsQuery(string queryUri)
            : base(queryUri)
        {
        }

        /// <summary>
        /// convenience method to create an URI based on a userID for a contacts feed
        /// this returns a FULL projection by default
        /// </summary>
        /// <param name="userID">if the parameter is NULL, uses the default user</param>
        /// <returns>string</returns>
        public static string CreateContactsUri(string userID)
        {
            return CreateContactsUri(userID, ContactsQuery.fullProjection);
        }

        /// <summary>
        /// convenience method to create an URI based on a userID for a contacts feed
        /// this returns a FULL projection by default
        /// </summary>
        /// <param name="userID">if the parameter is NULL, uses the default user</param>
        /// <param name="projection">the projection to use</param>
        /// <returns>string</returns>
        public static string CreateContactsUri(string userID, string projection)
        {
            return ContactsQuery.contactsBaseUri + UserString(userID) + projection;
        }

        /// <summary>Constrains the results to only the contacts belonging to the 
        /// group specified. Value of this parameter specifies group ID</summary> 
        /// <returns> </returns>
        public string Group
        {
            get { return this.group; }
            set { this.group = value; }
        }

        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] deli = { '?', '&' };

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, deli);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '=' };
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                            case "group":
                                this.Group = parameters[1];
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }

        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            if (this.Group != null && this.Group.Length > 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "group={0}", Utilities.UriEncodeReserved(this.Group));
                paramInsertion = '&';
            }
            return newPath.ToString();
        }
    }
}
