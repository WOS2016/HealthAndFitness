using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Jot : SimpleElement
    {
        /// <summary>
        /// default constructor for Jot
        /// </summary>
        public Jot()
            : base(ContactsNameTable.JotElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>Predefined calendar link type. Can be one of work, home or free-busy</summary>
        /// <returns> </returns>
        public string Relation
        {
            get
            {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set
            {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }
    }
}
