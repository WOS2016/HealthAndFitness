using System;
using System.Collections.Generic;
using System.Text;

using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;


namespace RewriteGDataContacts.GData.Contacts
{
    public class GroupMembership : SimpleElement
    {
        /// <summary>the  href attribute </summary>
        public const string XmlAttributeHRef = "href";
        /// <summary>the deleted attribute </summary>
        public const string XmlAttributeDeleted = "deleted";

        /// <summary>
        /// default constructor
        /// </summary>
        public GroupMembership()
            : base(ContactsNameTable.GroupMembershipInfo, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeHRef, null);
            this.Attributes.Add(XmlAttributeDeleted, null);
        }

        /// <summary>Identifies the group to which the contact belongs or belonged.
        /// The group is referenced by its id.</summary>
        public string HRef
        {
            get
            {
                return this.Attributes[XmlAttributeHRef] as string;
            }
            set
            {
                this.Attributes[XmlAttributeHRef] = value;
            }
        }

        /// <summary>Means that the group membership was removed for the contact.
        /// This attribute will only be included if showdeleted is specified
        /// as query parameter, otherwise groupMembershipInfo for groups a contact
        /// does not belong to anymore is simply not returned.</summary>
        public string Deleted
        {
            get
            {
                return this.Attributes[XmlAttributeDeleted] as string;
            }
        }
    }
}
