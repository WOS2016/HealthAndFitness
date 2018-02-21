using System;
using System.Collections.Generic;
using System.Text;

using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class Language : SimpleElement
    {
        /// <summary>
        /// the code attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeCode = "code";

        /// <summary>
        /// default constructor for Language
        /// </summary>
        public Language()
            : base(ContactsNameTable.LanguageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(AttributeCode, null);
        }

        /// <summary>
        /// default constructor for Language with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Language(string initValue)
            : base(ContactsNameTable.LanguageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(AttributeCode, null);
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

        /// <summary>A language code conforming to the IETF BCP 47 specification.</summary>
        /// <returns> </returns>
        public string Code
        {
            get
            {
                return this.Attributes[AttributeCode] as string;
            }
            set
            {
                this.Attributes[AttributeCode] = value;
            }
        }
    }
}
