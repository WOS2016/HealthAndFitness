using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Sensitivity : SimpleElement
    {
        /// <summary>
        /// default constructor for Sensitivity
        /// </summary>
        public Sensitivity()
            : base(ContactsNameTable.SensitivityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>
        /// default constructor for Sensitivity with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Sensitivity(string initValue)
            : base(ContactsNameTable.SensitivityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, initValue);
        }

        /// <summary>returns the relationship value</summary>
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
