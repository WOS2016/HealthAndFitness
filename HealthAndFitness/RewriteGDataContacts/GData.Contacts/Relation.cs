using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Relation : SimpleElement
    {
        /// <summary>
        /// default constructor for Relation
        /// </summary>
        public Relation()
            : base(ContactsNameTable.RelationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>A freeform name of a language. Must not be empty or all whitespace.</summary>
        /// <returns> </returns>
        public string Label
        {
            get
            {
                return this.Attributes[ContactsNameTable.AttributeLabel] as string;
            }
            set
            {
                this.Attributes[ContactsNameTable.AttributeLabel] = value;
            }
        }

        /// <summary>defines the link type.</summary>
        /// <returns> </returns>
        public string Rel
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
