using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Hobby : SimpleElement
    {
        /// <summary>
        /// default constructor for Hobby
        /// </summary>
        public Hobby()
            : base(ContactsNameTable.HobbyElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Hobby with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Hobby(string initValue)
            : base(ContactsNameTable.HobbyElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
