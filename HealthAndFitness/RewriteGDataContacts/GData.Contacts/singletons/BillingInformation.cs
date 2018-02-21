using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class BillingInformation : SimpleElement
    {
        /// <summary>
        /// default constructor for BillingInformation
        /// </summary>
        public BillingInformation()
            : base(ContactsNameTable.BillingInformationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for BillingInformation with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public BillingInformation(string initValue)
            : base(ContactsNameTable.BillingInformationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
