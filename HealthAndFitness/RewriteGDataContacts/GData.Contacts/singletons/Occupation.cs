using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Occupation : SimpleElement
    {
        /// <summary>
        /// default constructor for Occupation
        /// </summary>
        public Occupation()
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Occupation with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Occupation(string initValue)
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
