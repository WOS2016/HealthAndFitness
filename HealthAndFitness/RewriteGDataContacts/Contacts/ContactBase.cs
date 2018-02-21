using System;
using System.Collections.Generic;
using System.Text;
//using Google.GData;
//using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;

using RewriteCoreClient.GData.Client;
using RewriteCoreClient.GData.Client.Extensions;
using Rewrite.GData.Extensions;
using RewriteGDataContacts.GData.Contacts;

namespace RewriteGDataContacts.Contacts
{
    //public abstract class ContactBase : Google.GData.Client.Entry
    public abstract class ContactBase : Entry
    {
        public bool Deleted
        {
            get
            {
                BaseContactEntry b = this.AtomEntry as BaseContactEntry;
                if (b != null)
                {
                    return b.Deleted;
                }
                return false;
            }
        }

        public ExtensionCollection<ExtendedProperty> ExtendedProperties
        {
            get
            {
                BaseContactEntry b = this.AtomEntry as BaseContactEntry;
                if (b != null)
                {
                    return b.ExtendedProperties;
                }
                return null;
            }
        }



    }
}
