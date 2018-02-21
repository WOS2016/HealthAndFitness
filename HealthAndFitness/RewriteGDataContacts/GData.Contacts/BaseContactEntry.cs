using System;
using System.Collections.Generic;
using System.Text;

using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using Rewrite.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions;

namespace RewriteGDataContacts.GData.Contacts
{
    public abstract class BaseContactEntry : AbstractEntry, IContainsDeleted
    {
      
        private ExtensionCollection<ExtendedProperty> xproperties;

        /// <summary>
        /// Constructs a new BaseContactEntry instance
        /// to indicate that it is an event.
        /// </summary>
        public BaseContactEntry()
            : base()
        {
            Tracing.TraceMsg("Created BaseContactEntry Entry");
            this.AddExtension(new ExtendedProperty());
            this.AddExtension(new Deleted());
        }

        /// <summary>
        /// returns the extended properties on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<ExtendedProperty> ExtendedProperties
        {
            get
            {
                if (this.xproperties == null)
                {
                    this.xproperties = new ExtensionCollection<ExtendedProperty>(this);
                }
                return this.xproperties;
            }
        }

        //public bool Deleted => throw new NotImplementedException();
        /// <summary>
        /// if this is a previously deleted contact, returns true
        /// to delete a contact, use the delete method
        /// </summary>
        public bool Deleted
        {
            get
            {
                if (FindExtension(GDataParserNameTable.XmlDeletedElement,
                    BaseNameTable.gNamespace) != null)
                {
                    return true;
                }
                return false;
            }
        }


    }
}
