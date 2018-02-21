using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Mileage : SimpleElement
    {
        /// <summary>
        /// default constructor for Mileage
        /// </summary>
        public Mileage()
            : base(ContactsNameTable.MileageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Mileage with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Mileage(string initValue)
            : base(ContactsNameTable.MileageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
