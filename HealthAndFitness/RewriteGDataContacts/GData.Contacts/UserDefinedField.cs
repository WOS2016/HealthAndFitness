using System;
using System.Collections.Generic;
using System.Text;

//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public class UserDefinedField : SimpleAttribute
    {
        /// <summary>
        /// key attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeKey = "key";

        /// <summary>
        /// default constructor for UserDefinedField
        /// </summary>
        public UserDefinedField()
            : base(ContactsNameTable.UserDefinedFieldElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(AttributeKey, null);
        }

        /// <summary>
        /// default constructor for UserDefinedField with an initial value
        /// </summary>
        /// <param name="initValue"/>
        /// <param name="initKey"/>
        public UserDefinedField(string initValue, string initKey)
            : base(ContactsNameTable.UserDefinedFieldElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue)
        {
            this.Attributes.Add(AttributeKey, initKey);
        }

        /// <summary>A simple string value used to name this field. Case-sensitive</summary>
        /// <returns> </returns>
        public string Key
        {
            get
            {
                return this.Attributes[AttributeKey] as string;
            }
            set
            {
                this.Attributes[AttributeKey] = value;
            }
        }
    }
}
