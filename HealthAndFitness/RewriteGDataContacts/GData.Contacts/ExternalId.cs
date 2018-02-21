using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using Rewrite.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class ExternalId : SimpleAttribute
    {
        /// <summary>
        /// default constructor for ExternalId
        /// </summary>
        public ExternalId()
            : base(ContactsNameTable.ExternalIdElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
        }

        /// <summary>
        /// default constructor for ExternalId with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public ExternalId(string initValue)
            : base(ContactsNameTable.ExternalIdElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
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

        /// <summary>User-defined calendar link type.</summary>
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
    }
}
