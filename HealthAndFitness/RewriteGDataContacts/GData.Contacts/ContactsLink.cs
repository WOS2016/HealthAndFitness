using System;
using System.Collections.Generic;
using System.Text;

using RewriteCoreClient.GData.Client.Extensions;
using Rewrite.GData.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    /// <summary>
    /// Storage for URL of the contact's information. The element can be repeated.
    /// </summary>
    public class ContactsLink : LinkAttributesElement
    {
        /// <summary>
        /// href Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeHref = "href";

        /// <summary>
        /// default constructor for CalendarLink
        /// </summary>
        public ContactsLink(string elementName, string elementPrefix, string elementNamespace)
            : base(elementName, elementPrefix, elementNamespace)
        {
            this.Attributes.Add(AttributeHref, null);
        }

        /// <summary>The URL of the the related link.</summary>
        /// <returns> </returns>
        public string Href
        {
            get
            {
                return this.Attributes[AttributeHref] as string;
            }
            set
            {
                this.Attributes[AttributeHref] = value;
            }
        }
    }
}
