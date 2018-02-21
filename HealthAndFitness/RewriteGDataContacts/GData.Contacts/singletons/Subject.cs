using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;


namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Subject : SimpleElement
    {
        /// <summary>
        /// default constructor for Subject
        /// </summary>
        public Subject()
            : base(ContactsNameTable.SubjectElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Subject with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Subject(string initValue)
            : base(ContactsNameTable.SubjectElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
