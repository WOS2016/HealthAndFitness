using System;
using System.Collections.Generic;
using System.Text;
using RewriteCoreClient.GData.Client;

namespace RewriteGDataContacts.GData.Contacts
{
    public class GroupEntry : BaseContactEntry
    {
        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string GroupTerm = "http://schemas.google.com/contact/2008#group";

        /// <summary>
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory GROUP_CATEGORY =
            new AtomCategory(GroupEntry.GroupTerm, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public GroupEntry()
            : base()
        {
            Tracing.TraceMsg("Created Group Entry");
            this.AddExtension(new SystemGroup());
            Categories.Add(GROUP_CATEGORY);
        }

        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new GroupEntry Update()
        {
            return base.Update() as GroupEntry;
        }

        /// <summary>
        /// returns the systemgroup id, if this groupentry represents 
        /// a system group. 
        /// The values of the system group ids corresponding to these 
        /// groups can be found in the Reference Guide for the Contacts Data API.
        /// Currently the values can be Contacts, Friends, Family and Coworkers
        /// </summary>
        /// <returns></returns>
        public string SystemGroup
        {
            get
            {
                SystemGroup sg = FindExtension(ContactsNameTable.SystemGroupElement,
                    ContactsNameTable.NSContacts) as SystemGroup;

                if (sg != null)
                {
                    return sg.Id;
                }
                return null;
            }
        }
    }
}
