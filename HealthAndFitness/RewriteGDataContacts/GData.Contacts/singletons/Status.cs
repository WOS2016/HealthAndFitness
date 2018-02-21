using System;
using System.Collections.Generic;
using System.Text;
using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts.singletons
{
    public class Status : SimpleElement
    {
        /// <summary>
        /// indexed attribute for the status element
        /// </summary>
        /// <returns></returns>
        public const String XmlAttributeIndexed = "indexed";

        /// <summary>
        /// default constructor for Status
        /// </summary>
        public Status()
            : base(ContactsNameTable.StatusElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeIndexed, null);
        }

        /// <summary>
        /// default constructor for Status with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Status(bool initValue)
            : base(ContactsNameTable.StatusElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts)
        {
            this.Attributes.Add(XmlAttributeIndexed, initValue ? Utilities.XSDTrue : Utilities.XSDFalse);
        }

        /// <summary>Indexed attribute.</summary>
        /// <returns> </returns>
        public bool Indexed
        {
            get
            {
                bool result;
                if (!Boolean.TryParse(this.Attributes[XmlAttributeIndexed] as string, out result))
                {
                    result = false;
                }
                return result;
            }
            set
            {
                this.Attributes[XmlAttributeIndexed] = value ? Utilities.XSDTrue : Utilities.XSDFalse;
            }
        }
    }
}
