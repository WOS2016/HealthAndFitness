using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// A request factory to generate an authorization header suitable for use
    /// with OAuth 2.0.
    /// </summary>
    public class GOAuth2RequestFactory : GDataGAuthRequestFactory
    {
        /// <summary>this factory's agent</summary>
        public const string GDataGAuthSubAgent = "GOAuth2RequestFactory-CS/1.0.0";

        /// <summary>
        /// Constructor.
        /// </summary>
        public GOAuth2RequestFactory(string service, string applicationName, OAuth2Parameters parameters)
            : base(service, applicationName)
        {
            this.Parameters = parameters;
        }

        /// <summary>
        /// default constructor.
        /// </summary>
        public override IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GOAuth2Request(type, uriTarget, this);
        }

        public OAuth2Parameters Parameters { get; set; }
    }
}
