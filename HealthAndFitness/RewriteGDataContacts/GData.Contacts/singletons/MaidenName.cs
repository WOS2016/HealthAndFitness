using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class MaidenName : SimpleElement
    {
        /// <summary>
        /// default constructor for MaidenName
        /// </summary>
        public MaidenName()
            : base(ContactsNameTable.MaidenNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for MaidenName with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MaidenName(string initValue)
            : base(ContactsNameTable.MaidenNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
