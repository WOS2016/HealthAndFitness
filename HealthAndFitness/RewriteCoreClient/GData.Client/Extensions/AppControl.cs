using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;
//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions;
using RewriteCoreClient.GData.Client.Extensions.AppControl;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions.AppControl
{
    /// <summary>
    /// app:control schema extension
    /// </summary>
    public class AppControl : SimpleContainer
    {
        /// <summary>
        /// default constructor for app:control
        /// </summary>
        public AppControl()
            : this(BaseNameTable.AppPublishingNamespace(null))
        {
        }

        /// <summary>
        /// app:control constructor with namespace as parameter
        /// </summary>
        public AppControl(string ns) :
            base(BaseNameTable.XmlElementPubControl,
            BaseNameTable.gAppPublishingPrefix,
            ns)
        {
            this.ExtensionFactories.Add(new AppDraft());
        }

        /// <summary>
        /// returns the app:draft element
        /// </summary>
        public AppDraft Draft
        {
            get
            {
                return FindExtension(BaseNameTable.XmlElementPubDraft,
                    BaseNameTable.AppPublishingNamespace(this)) as AppDraft;
            }
            set
            {
                ReplaceExtension(BaseNameTable.XmlElementPubDraft,
                    BaseNameTable.AppPublishingNamespace(this),
                    value);
            }
        }

        /// <summary>
        /// need so setup the namespace based on the version information     
        /// </summary>
        protected override void VersionInfoChanged()
        {
            base.VersionInfoChanged();
            this.SetXmlNamespace(BaseNameTable.AppPublishingNamespace(this));
        }
    }
}
