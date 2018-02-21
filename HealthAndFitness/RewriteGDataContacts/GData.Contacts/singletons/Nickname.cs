using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Nickname : SimpleElement
    {
        /// <summary>
        /// default constructor for Nickname
        /// </summary>
        public Nickname()
            : base(ContactsNameTable.NicknameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }

        /// <summary>
        /// default constructor for Nickname with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Nickname(string initValue)
            : base(ContactsNameTable.NicknameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
        }
    }
}
