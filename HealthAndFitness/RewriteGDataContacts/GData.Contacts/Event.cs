using System;
using System.Collections.Generic;
using System.Text;
using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using Rewrite.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Event : SimpleContainer
    {
        /// <summary>
        /// default constructor for Event
        /// </summary>
        public Event()
            : base(ContactsNameTable.EventElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.ExtensionFactories.Add(new When());
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

        /// <summary>
        /// exposes the When element for this event
        /// </summary>
        /// <returns></returns>
        public When When
        {
            get
            {
                return FindExtension(GDataParserNameTable.XmlWhenElement,
                    BaseNameTable.gNamespace) as When;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlWhenElement,
                    BaseNameTable.gNamespace,
                    value);
            }
        }
    }
}
