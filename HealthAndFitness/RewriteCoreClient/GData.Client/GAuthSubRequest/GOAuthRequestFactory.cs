using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// A request factory to generate an authorization header suitable for use
    /// with OAuth.
    /// </summary>
    public class GOAuthRequestFactory : GDataGAuthRequestFactory
    {
        /// <summary>this factory's agent</summary> 
        public const string GDataGAuthSubAgent = "GOAuthRequestFactory-CS/1.0.0";

        private string tokenSecret;
        private string token;
        private string consumerSecret;
        private string consumerKey;

        /// <summary>
        /// default constructor.
        /// </summary>
        public GOAuthRequestFactory(string service, string applicationName)
            : base(service, applicationName)
        {
        }

        /// <summary>
        /// overloaded constructor that sets parameters from an OAuthParameter instance.
        /// </summary>
        public GOAuthRequestFactory(string service, string applicationName, OAuthParameters parameters)
            : base(service, applicationName)
        {
            if (parameters.ConsumerKey != null)
            {
                this.ConsumerKey = parameters.ConsumerKey;
            }
            if (parameters.ConsumerSecret != null)
            {
                this.ConsumerSecret = parameters.ConsumerSecret;
            }
            if (parameters.Token != null)
            {
                this.Token = parameters.Token;
            }
            if (parameters.TokenSecret != null)
            {
                this.TokenSecret = parameters.TokenSecret;
            }
        }

        /// <summary>
        /// default constructor.
        /// </summary>
        public override IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GOAuthRequest(type, uriTarget, this);
        }

        public string ConsumerSecret
        {
            get { return this.consumerSecret; }
            set { this.consumerSecret = value; }
        }

        public string ConsumerKey
        {
            get { return this.consumerKey; }
            set { this.consumerKey = value; }
        }

        public string TokenSecret
        {
            get { return this.tokenSecret; }
            set { this.tokenSecret = value; }
        }

        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }
    }
}
