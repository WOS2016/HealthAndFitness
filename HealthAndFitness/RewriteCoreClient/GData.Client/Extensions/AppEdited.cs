using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;
//using Google.GData.Extensions;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions
{
    /// <summary>
    /// The "app:edited" element is a Date construct (as defined by
    /// [RFC4287]), whose content indicates the last time an Entry was
    /// edited.  If the entry has not been edited yet, the content indicates
    /// the time it was created.  Atom Entry elements in Collection Documents
    /// SHOULD contain one app:edited element, and MUST NOT contain more than
    /// one.
    /// The server SHOULD change the value of this element every time an
    /// Entry Resource or an associated Media Resource has been edited
    /// </summary>
    public class AppEdited : SimpleElement
    {
        /// <summary>
        /// creates a default app:edited element
        /// </summary>
        public AppEdited()
            : base(BaseNameTable.XmlElementPubEdited,
            BaseNameTable.gAppPublishingPrefix,
            BaseNameTable.NSAppPublishingFinal)
        { }

        /// <summary>
        /// creates a default app:edited element with the given datetime value
        /// </summary>
        public AppEdited(DateTime dateValue)
            : base(BaseNameTable.XmlElementPubEdited,
            BaseNameTable.gAppPublishingPrefix,
            BaseNameTable.NSAppPublishingFinal)
        {
            this.Value = Utilities.LocalDateTimeInUTC(dateValue);
        }

        /// <summary>
        /// creates an app:edited element with the string as it's
        /// default value. The string has to conform to RFC4287
        /// </summary>
        /// <param name="dateInUtc"></param>
        public AppEdited(string dateInUtc)
            : base(BaseNameTable.XmlElementPubEdited,
            BaseNameTable.gAppPublishingPrefix,
            BaseNameTable.NSAppPublishingFinal,
            dateInUtc)
        {
        }

        /// <summary>
        ///  Accessor Method for the value as a DateTime
        /// </summary>
        public DateTime DateValue
        {
            get
            {
                return DateTime.Parse(this.Value, CultureInfo.InvariantCulture);
            }
            set
            {
                this.Value = Utilities.LocalDateTimeInUTC(value);
            }
        }
    }
}
