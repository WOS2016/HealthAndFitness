using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class ShortName : SimpleElement
    {
        /// <summary>
        /// default constructor for ShortName
        /// </summary>
        public ShortName()
            : base(ContactsNameTable.ShortNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for ShortName with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public ShortName(string initValue)
            : base(ContactsNameTable.ShortNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
