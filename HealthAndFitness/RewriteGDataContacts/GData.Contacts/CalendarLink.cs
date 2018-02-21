using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteGDataContacts.GData.Contacts
{
    /// <summary>
    /// Storage for URL of the contact's calendar. The element can be repeated.
    /// </summary>
    public class CalendarLink : ContactsLink
    {
        public CalendarLink()
            : base(ContactsNameTable.CalendarLinkElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
        }
    }
}
