using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Initials : SimpleElement
    {
        /// <summary>
        /// default constructor for Initials
        /// </summary>
        public Initials()
            : base(ContactsNameTable.InitialsElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Initials with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Initials(string initValue)
            : base(ContactsNameTable.InitialsElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
