using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class SystemGroup : SimpleElement
    {
        /// <summary>
        /// id attribute for the system group element
        /// </summary>
        /// <returns></returns>
        public const string XmlAttributeId = "id";

        /// <summary>
        /// default constructor
        /// </summary>
        public SystemGroup()
            : base(ContactsNameTable.SystemGroupElement, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeId, null);
        }

        /// <summary>Identifies the system group. Note that you still need
        /// to use the group entries href membership to retrieve the group
        /// </summary>
        public string Id
        {
            get
            {
                return this.Attributes[XmlAttributeId] as string;
            }
        }
    }
}
