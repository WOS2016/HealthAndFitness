using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;
//using Google.GData.Extensions;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions
{
    /// <summary>
    /// app:draft schema extension describing that an entry is in draft mode
    /// it's a child of app:control
    /// </summary>
    public class AppDraft : SimpleElement
    {
        /// <summary>
        /// default constructor for app:draft
        /// </summary>
        public AppDraft()
            : base(BaseNameTable.XmlElementPubDraft,
            BaseNameTable.gAppPublishingPrefix,
            BaseNameTable.AppPublishingNamespace(null))
        { }

        /// <summary>
        /// default constructor for app:draft
        /// </summary>
        public AppDraft(bool isDraft)
            : base(BaseNameTable.XmlElementPubDraft,
            BaseNameTable.gAppPublishingPrefix,
            BaseNameTable.AppPublishingNamespace(null),
            isDraft ? "yes" : "no")
        { }

        /// <summary>
        ///  Accessor Method for the value as integer
        /// </summary>
        public override bool BooleanValue
        {
            get { return this.Value == "yes" ? true : false; }
            set { this.Value = value ? "yes" : "no"; }
        }

        /// <summary>
        /// need so setup the namespace based on the version information
        /// changes
        /// </summary>
        protected override void VersionInfoChanged()
        {
            base.VersionInfoChanged();
            this.SetXmlNamespace(BaseNameTable.AppPublishingNamespace(this));
        }
    }
}
