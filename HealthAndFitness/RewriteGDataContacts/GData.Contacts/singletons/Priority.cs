using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Priority : SimpleElement
    {
        /// <summary>
        /// default constructor for Priority
        /// </summary>
        public Priority()
            : base(ContactsNameTable.PriorityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>
        /// default constructor for Priority with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Priority(string initValue)
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, initValue);
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
