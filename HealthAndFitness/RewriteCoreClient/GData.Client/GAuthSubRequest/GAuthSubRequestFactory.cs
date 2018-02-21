#region Using directives

#define USE_TRACING

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// GDataAuthSubRequestFactory implementation
    /// </summary>
    public class GAuthSubRequestFactory : GDataGAuthRequestFactory
    {

        /// holds the private key that is used to sign the requests
        private AsymmetricAlgorithm privateKey;

        /// <summary>
        /// default constructor
        /// </summary>
        public GAuthSubRequestFactory(string service, string applicationName)
            : base(service, applicationName)
        {
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public override IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GAuthSubRequest(type, uriTarget, this);
        }

        /// <summary>
        /// accessor method public string Token
        /// </summary>
        /// <returns>
        /// the string token for the authsub request
        /// </returns>
        public string Token
        {
            get { return this.GAuthToken; }
            set { this.GAuthToken = value; }
        }

        /// <summary>
        /// accessor method public AsymmetricAlgorithm PrivateKey
        /// </summary>
        /// <returns>
        /// the private Key used for the authsub request
        /// </returns>
        public AsymmetricAlgorithm PrivateKey
        {
            get { return this.privateKey; }
            set { this.privateKey = value; }
        }

    }
}
