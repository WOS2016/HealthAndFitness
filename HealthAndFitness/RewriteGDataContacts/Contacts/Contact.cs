using System;
using System.Collections.Generic;
using System.Text;

using RewriteCoreClient.GData.Client;
//using Google.GData.Extensions;
using RewriteGDataContacts.GData.Contacts;
//using RewriteGDataContacts.GData;
using Rewrite.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;
using RewriteCoreClient.GData.Client.Extensions;


namespace RewriteGDataContacts.Contacts
{
    public class Contact : ContactBase
    {
        /// <summary>
        /// creates the inner contact object when needed
        /// </summary>
        /// <returns></returns>
        protected override void EnsureInnerObject()
        {
            if (this.AtomEntry == null)
            {
                this.AtomEntry = new ContactEntry();
            }
        }

        /// <summary>
        /// readonly accessor for the ContactEntry that is underneath this object.
        /// </summary>
        /// <returns></returns>
        public ContactEntry ContactEntry
        {
            get
            {
                return this.AtomEntry as ContactEntry;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary Email
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public EMail PrimaryEmail
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryEmail;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary Phonenumber
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PhoneNumber PrimaryPhonenumber
        {
            get
            {
                if (this.ContactEntry != null)
                {
                    return this.ContactEntry.PrimaryPhonenumber;
                }
                return null;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary PostalAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public StructuredPostalAddress PrimaryPostalAddress
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryPostalAddress;
            }
        }

        /// <summary>
        /// convenience accessor to find the primary IMAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public IMAddress PrimaryIMAddress
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PrimaryIMAddress;
            }
        }

        /// <summary>
        /// returns the groupmembership info on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<GroupMembership> GroupMembership
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.GroupMembership;
            }
        }

        /// <summary>
        /// getter/setter for the email extension element
        /// </summary>
        public ExtensionCollection<EMail> Emails
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.Emails;
            }
        }

        /// <summary>
        /// getter/setter for the IM extension element
        /// </summary>
        public ExtensionCollection<IMAddress> IMs
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.IMs;
            }
        }

        /// <summary>
        /// returns the phone number collection
        /// </summary>
        public ExtensionCollection<PhoneNumber> Phonenumbers
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.Phonenumbers;
            }
        }

        /// <summary>
        /// returns the postal address collection
        /// </summary>
        public ExtensionCollection<StructuredPostalAddress> PostalAddresses
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PostalAddresses;
            }
        }

        /// <summary>
        /// returns the organization collection
        /// </summary>
        public ExtensionCollection<Organization> Organizations
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.Organizations;
            }
        }

        /// <summary>
        /// returns the language collection
        /// </summary>
        public ExtensionCollection<Language> Languages
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.Languages;
            }
        }

        /// <summary>
        /// retrieves the Uri of the Photo Link. To set this, you need to create an AtomLink object
        /// and add/replace it in the atomlinks colleciton. 
        /// </summary>
        /// <returns></returns>
        public Uri PhotoUri
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PhotoUri;
            }
        }

        /// <summary>
        /// if a photo is present on this contact, it will have an etag associated with it,
        /// that needs to be used when you want to delete or update that picture.
        /// </summary>
        /// <returns>the etag value as a string</returns>
        public string PhotoEtag
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.PhotoEtag;
            }
            set
            {
                EnsureInnerObject();
                this.ContactEntry.PhotoEtag = value;
            }
        }

        /// <summary>
        /// returns the location associated with a contact
        /// </summary>
        /// <returns></returns>
        public string Location
        {
            get
            {
                EnsureInnerObject();
                return this.ContactEntry.Location;
            }
            set
            {
                EnsureInnerObject();
                this.ContactEntry.Location = value;
            }
        }
        /// <summary>
        /// the contacts name object
        /// </summary>
        public Name Name
        {
            get
            {
                EnsureInnerObject();
                if (this.ContactEntry.Name == null)
                    this.ContactEntry.Name = new Name();
                return this.ContactEntry.Name;
            }
            set
            {
                EnsureInnerObject();
                this.ContactEntry.Name = value;
            }
        }
    }
}
