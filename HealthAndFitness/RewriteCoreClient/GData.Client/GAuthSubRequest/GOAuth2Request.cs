using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// GOAuthSubRequest implementation.
    /// </summary>
    public class GOAuth2Request : GDataGAuthRequest
    {
        /// <summary>holds the factory instance</summary>
        private GOAuth2RequestFactory factory;

        /// <summary>
        /// default constructor.
        /// </summary>
        internal GOAuth2Request(GDataRequestType type, Uri uriTarget, GOAuth2RequestFactory factory)
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

            if (string.IsNullOrEmpty(this.factory.Parameters.AccessToken))
            {
                throw new GDataRequestException("An access token must be provided to use GOAuthRequestFactory");
            }

            this.Request.Headers.Remove("Authorization"); // needed?
            this.Request.Headers.Add("Authorization", String.Format(
                "{0} {1}", this.factory.Parameters.TokenType, this.factory.Parameters.AccessToken));
        }

        public override void Execute()
        {
            try
            {
                base.Execute();
            }
            catch (GDataRequestException re)
            {
                HttpWebResponse webResponse = re.Response as HttpWebResponse;
                if (webResponse != null && webResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Tracing.TraceMsg("Access token might have expired, refreshing.");
                    Reset();
                    try
                    {
                        OAuthUtil.RefreshAccessToken(this.factory.Parameters);
                    }
                    catch (WebException e)
                    {
                        Tracing.TraceMsg("Failed to refresh access token: " + e.StackTrace);
                        throw re;
                    }
                    base.Execute();
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
