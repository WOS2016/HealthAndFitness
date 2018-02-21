#region Using directives

#define USE_TRACING

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.ComponentModel;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>base GDataRequestFactory implementation</summary> 
    public class GDataGAuthRequestFactory : GDataRequestFactory, IVersionAware
    {
        /// <summary>
        /// the header used to indicate version requests
        /// </summary>
        public const string GDataVersion = "GData-Version";

        private string gAuthToken;   // we want to remember the token here
        private string handler;      // so the handler is useroverridable, good for testing
        private string gService;         // the service we pass to Gaia for token creation
        private string applicationName;  // the application name we pass to Gaia and append to the user-agent
        private bool fMethodOverride;    // to override using post, or to use PUT/DELETE
        private int numberOfRetries;        // holds the number of retries the request will undertake
        private bool fStrictRedirect;       // indicates if redirects should be handled strictly
        private string accountType;         // indicates the accountType to use
        private string captchaAnswer;       // indicates the captcha Answer in a challenge
        private string captchaToken;        // indicates the captchaToken in a challenge

        private const int RetryCount = 3;	// default retry count for failed requests       

        /// <summary>default constructor</summary> 
        public GDataGAuthRequestFactory(string service, string applicationName)
            : this(service, applicationName, RetryCount)
        {
        }

        /// <summary>overloaded constructor</summary> 
        public GDataGAuthRequestFactory(string service, string applicationName, int numberOfRetries)
            : base(applicationName)
        {
            this.Service = service;
            this.ApplicationName = applicationName;
            this.NumberOfRetries = numberOfRetries;
        }

        /// <summary>default constructor</summary>
        public override IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataGAuthRequest(type, uriTarget, this);
        }

        /// <summary>Get/Set accessor for gAuthToken</summary> 
        public string GAuthToken
        {
            get { return this.gAuthToken; }
            set
            {
                Tracing.TraceMsg("set token called with: " + value);
                this.gAuthToken = value;
            }
        }

        /// <summary>Gets an authentication token for the current credentials</summary> 
        public string QueryAuthToken(GDataCredentials gc)
        {
            GDataGAuthRequest request = new GDataGAuthRequest(GDataRequestType.Query, null, this);
            return request.QueryAuthToken(gc);
        }

        /// <summary>accessor method public string UserAgent, with GFE support</summary> 
        /// <remarks>GFE will enable gzip support ONLY for browser that have the string
        /// "gzip" in their user agent (IE or Mozilla), since lot of browsers have a
        /// broken gzip support.</remarks>
        public override string UserAgent
        {
            get { return (base.UserAgent + (this.UseGZip ? " (gzip)" : "")); }
            set { base.UserAgent = value; }
        }

        /// <summary>Get/Set accessor for the application name</summary> 
        public string ApplicationName
        {
            get { return this.applicationName == null ? "" : this.applicationName; }
            set { this.applicationName = value; }
        }

        /// <summary>returns the service string</summary> 
        public string Service
        {
            get { return this.gService; }
            set { this.gService = value; }
        }

        /// <summary>Let's assume you are behind a corporate firewall that does not 
        /// allow all HTTP verbs (as you know, the atom protocol uses GET, 
        /// POST, PUT and DELETE). If you set MethodOverride to true,
        /// PUT and DELETE will be simulated using HTTP Post. It will
        /// add an X-Method-Override header to the request that 
        /// indicates the "real" method we wanted to send. 
        /// </summary> 
        /// <returns> </returns>
        public bool MethodOverride
        {
            get { return this.fMethodOverride; }
            set { this.fMethodOverride = value; }
        }

        /// <summary>indicates if a redirect should be followed on not HTTPGet</summary> 
        /// <returns> </returns>
        public bool StrictRedirect
        {
            get { return this.fStrictRedirect; }
            set { this.fStrictRedirect = value; }
        }

        /// <summary>
        /// property accessor to adjust how often a request of this factory should retry
        /// </summary>
        public int NumberOfRetries
        {
            get { return this.numberOfRetries; }
            set { this.numberOfRetries = value; }
        }

        /// <summary>
        /// property accessor to set the account type that is used during
        /// authentication. Defaults, if not set, to HOSTED_OR_GOOGLE
        /// </summary>
        public string AccountType
        {
            get { return this.accountType; }
            set { this.accountType = value; }
        }

        /// <summary>property to hold the Answer for a challenge</summary> 
        /// <returns> </returns>
        public string CaptchaAnswer
        {
            get { return this.captchaAnswer; }
            set { this.captchaAnswer = value; }
        }

        /// <summary>property to hold the token for a challenge</summary> 
        /// <returns> </returns>
        public string CaptchaToken
        {
            get { return this.captchaToken; }
            set { this.captchaToken = value; }
        }

        /// <summary>accessor method public string Handler</summary> 
        /// <returns> </returns>
        public string Handler
        {
            get
            {
                return this.handler != null ? this.handler : GoogleAuthentication.UriHandler;
            }
            set { this.handler = value; }
        }

        private VersionInformation versionInfo = new VersionInformation();

        /// <summary>
        /// returns the major protocol version number this element 
        /// is working against. 
        /// </summary>
        /// <returns></returns>
        public int ProtocolMajor
        {
            get
            {
                return this.versionInfo.ProtocolMajor;
            }
            set
            {
                this.versionInfo.ProtocolMajor = value;
            }
        }

        /// <summary>
        /// returns the minor protocol version number this element 
        /// is working against. 
        /// </summary>
        /// <returns></returns>
        public int ProtocolMinor
        {
            get
            {
                return this.versionInfo.ProtocolMinor;
            }
            set
            {
                this.versionInfo.ProtocolMinor = value;
            }
        }
    }
}
