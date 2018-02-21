using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Website : ContactsLink
    {
        /// <summary>
        /// default constructor for WebSite
        /// </summary>
        public Website()
            : base(ContactsNameTable.WebsiteElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }
    }
}
