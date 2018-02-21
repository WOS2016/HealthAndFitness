using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteGDataContacts.GData.Contacts
{
    public class ContactsNameTable
    {
        /// <summary>static string to specify the Contacts namespace supported</summary>
        public const string NSContacts = "http://schemas.google.com/contact/2008";

        /// <summary>static string to specify the Google Contacts prefix used</summary>
        public const string contactsPrefix = "gContact";

        /// <summary>
        /// Group Member ship info element string
        /// </summary>
        public const string GroupMembershipInfo = "groupMembershipInfo";

        /// <summary>
        /// SystemGroup element, indicating that this entry is a system group
        /// </summary>
        public const string SystemGroupElement = "systemGroup";

        /// <summary>
        /// Specifies billing information of the entity represented by the contact. The element cannot be repeated
        /// </summary>
        public const string BillingInformationElement = "billingInformation";

        /// <summary>
        /// Stores birthday date of the person represented by the contact. The element cannot be repeated
        /// </summary>
        public const string BirthdayElement = "birthday";

        /// <summary>
        /// Storage for URL of the contact's calendar. The element can be repeated
        /// </summary>
        public const string CalendarLinkElement = "calendarLink";

        /// <summary>
        /// A directory server associated with this contact. May not be repeated
        /// </summary>
        public const string DirectoryServerElement = "directoryServer";

        /// <summary>
        /// An event associated with a contact. May be repeated.
        /// </summary>
        public const string EventElement = "event";

        /// <summary>
        /// Describes an ID of the contact in an external system of some kind. This element may be repeated.
        /// </summary>
        public const string ExternalIdElement = "externalId";

        /// <summary>
        /// Specifies the gender of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string GenderElement = "gender";

        /// <summary>
        /// Specifies hobbies or interests of the person specified by the contact. The element can be repeated
        /// </summary>
        public const string HobbyElement = "hobby";

        /// <summary>
        /// Specifies the initials of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string InitialsElement = "initials";

        /// <summary>
        /// Storage for arbitrary pieces of information about the contact. Each jot has a type specified by the rel attribute and a text value. The element can be repeated.
        /// </summary>
        public const string JotElement = "jot";

        /// <summary>
        /// Specifies the preferred languages of the contact. The element can be repeated
        /// </summary>
        public const string LanguageElement = "language";

        /// <summary>
        /// Specifies maiden name of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string MaidenNameElement = "maidenName";

        /// <summary>
        /// Specifies the mileage for the entity represented by the contact. Can be used for example to document distance needed for reimbursement purposes.
        /// The value is not interpreted. The element cannot be repeated
        /// </summary>
        public const string MileageElement = "mileage";

        /// <summary>
        /// Specifies the nickname of the person represented by the contact. The element cannot be repeated
        /// </summary>
        public const string NicknameElement = "nickname";

        /// <summary>
        /// Specifies the occupation/profession of the person specified by the contact. The element cannot be repeated.
        /// </summary>
        public const string OccupationElement = "occupation";

        /// <summary>
        /// Classifies importance into 3 categories. can not be repeated
        /// </summary>
        public const string PriorityElement = "priority";

        /// <summary>
        /// Describes the relation to another entity. may be repeated
        /// </summary>
        public const string RelationElement = "relation";

        /// <summary>
        /// Classifies sensitifity of the contact
        /// </summary>
        public const string SensitivityElement = "sensitivity";

        /// <summary>
        /// Specifies short name of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string ShortNameElement = "shortName";

        /// <summary>
        /// Specifies status of the person.
        /// </summary>
        public const string StatusElement = "status";

        /// <summary>
        /// Specifies the subject of the contact. The element cannot be repeated.
        /// </summary>
        public const string SubjectElement = "subject";

        /// <summary>
        /// Represents an arbitrary key-value pair attached to the contact.
        /// </summary>
        public const string UserDefinedFieldElement = "userDefinedField";

        /// <summary>
        /// Websites associated with the contact. May be repeated
        /// </summary>
        public const string WebsiteElement = "website";

        /// <summary>
        /// rel Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeRel = "rel";

        /// <summary>
        /// label Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeLabel = "label";
    }
}
