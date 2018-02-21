using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class DirectoryServer : SimpleElement
    {
        /// <summary>
        /// default constructor for DirectoryServer
        /// </summary>
        public DirectoryServer()
            : base(ContactsNameTable.DirectoryServerElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for DirectoryServer with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public DirectoryServer(string initValue)
            : base(ContactsNameTable.DirectoryServerElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
