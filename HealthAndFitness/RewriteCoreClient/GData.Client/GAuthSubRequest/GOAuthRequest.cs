using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// GOAuthSubRequest implementation.
    /// </summary>
    public class GOAuthRequest : GDataGAuthRequest
    {
        /// <summary>holds the factory instance</summary> 
        private GOAuthRequestFactory factory;

        /// <summary>
        /// default constructor.
        /// </summary>
        internal GOAuthRequest(GDataRequestType type, Uri uriTarget, GOAuthRequestFactory factory)
            : base(type, uriTarget, factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// sets up the correct credentials for this call.
        /// </summary>
        protected override void EnsureCredentials()
        {
            HttpWebRequest http = this.Request as HttpWebRequest;

            if (string.IsNullOrEmpty(this.factory.ConsumerKey) || string.IsNullOrEmpty(this.factory.ConsumerSecret))
            {
                throw new GDataRequestException("ConsumerKey and ConsumerSecret must be provided to use GOAuthRequestFactory");
            }

            string oauthHeader = OAuthUtil.GenerateHeader(
                http.RequestUri,
                this.factory.ConsumerKey,
                this.factory.ConsumerSecret,
                this.factory.Token,
                this.factory.TokenSecret,
                http.Method);
            this.Request.Headers.Remove("Authorization"); // needed?
            this.Request.Headers.Add(oauthHeader);
        }
    }
}
