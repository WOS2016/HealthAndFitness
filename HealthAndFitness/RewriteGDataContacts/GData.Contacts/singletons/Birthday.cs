using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Birthday : SimpleElement
    {
        /// <summary>
        /// When Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeWhen = "when";

        /// <summary>
        /// default constructor for Birthday
        /// </summary>
        public Birthday()
            : base(ContactsNameTable.BirthdayElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(AttributeWhen, null);
        }

        /// <summary>
        /// default constructor for Birthday with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Birthday(string initValue)
            : base(ContactsNameTable.BirthdayElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(AttributeWhen, initValue);
        }

        /// <summary>Birthday date, given in format YYYY-MM-DD (with the year), or --MM-DD (without the year)</summary>
        /// <returns> </returns>
        public string When
        {
            get
            {
                return this.Attributes[AttributeWhen] as string;
            }
            set
            {
                this.Attributes[AttributeWhen] = value;
            }
        }
    }
}
