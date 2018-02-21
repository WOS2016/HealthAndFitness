using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
//using Google.GData.Client;
//using Google.GData.Extensions;
using System.Collections.Generic;
//using Google.GData.Extensions.AppControl;
using System.Security.Cryptography;
using System.ComponentModel;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// Base requestsettings class. Takes credentials, applicationsname
    /// and supports pagesizes and autopaging. This class is used to initialize a
    /// <seealso cref="FeedRequest&lt;T&gt;"/> object.
    /// </summary>
    /// <returns></returns>
    public class RequestSettings
    {

        private AuthenticationType authType = AuthenticationType.none;
        private string applicationName;
        private GDataCredentials credentials;
        private string authSubToken;
        private int pageSize = -1;
        private int max = -1;
        private bool autoPage;
        private int timeout = -1;
        private string consumerKey;
        private string consumerSecret;
        private string oAuthUser;
        private string oAuthDomain;
        private string token;
        private string tokenSecret;
        private AsymmetricAlgorithm privateKey;
        private Uri clientLoginHandler;
        private bool useSSL;

        /// <summary>
        /// an unauthenticated use case
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public RequestSettings(string applicationName)
        {
            this.applicationName = applicationName;
        }

        /// <summary>
        /// a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="userName">the user name</param>
        /// <param name="passWord">the password</param>
        /// <returns></returns>
        public RequestSettings(string applicationName, string username, string password) :
            this(applicationName, new GDataCredentials(username, password))
        {
        }

        /// <summary>
        /// a constructor for OpenAuthentication login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="consumerKey">the consumerKey to use</param>
        /// <param name="consumerSecret">the consumerSecret to use</param>
        /// <param name="user">the username to use</param>
        /// <param name="domain">the domain to use</param>
        /// <returns></returns>
        public RequestSettings(
            string applicationName,
            string consumerKey,
            string consumerSecret,
            string user,
            string domain)
            : this(applicationName)
        {
            this.authType = AuthenticationType.oAuth;
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.oAuthUser = user;
            this.oAuthDomain = domain;
        }

        /// <summary>
        /// a constructor for OpenAuthentication login use cases using 2 or 3-legged OAuth
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="consumerKey">the consumerKey to use</param>
        /// <param name="consumerSecret">the consumerSecret to use</param>
        /// <param name="token">The token to be used</param>
        /// <param name="tokenSecret">The tokenSecret to be used</param>
        /// <param name="user">the username to use</param>
        /// <param name="domain">the domain to use</param>
        /// <returns></returns>
        public RequestSettings(
            string applicationName,
            string consumerKey,
            string consumerSecret,
            string token,
            string tokenSecret,
            string user,
            string domain)
            : this(applicationName, consumerKey, consumerSecret, user, domain)
        {
            this.token = token;
            this.tokenSecret = tokenSecret;
        }

        /// <summary>
        /// a constructor for client login use cases
        /// </summary>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="credentials">the user credentials</param>
        /// <returns></returns>
        public RequestSettings(string applicationName, GDataCredentials credentials)
        {
            this.authType = AuthenticationType.clientLogin;
            this.applicationName = applicationName;
            this.credentials = credentials;
        }

        /// <summary>
        /// a constructor for a web application authentication scenario
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="authSubToken"></param>
        /// <returns></returns>
        public RequestSettings(string applicationName, string authSubToken)
            : this(applicationName)
        {
            this.authType = AuthenticationType.authSub;
            this.authSubToken = authSubToken;
        }

        /// <summary>
        /// a constructor for a web application authentication scenario
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="authSubToken"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public RequestSettings(
            string applicationName,
            string authSubToken,
            AsymmetricAlgorithm privateKey)
            : this(applicationName)
        {
            this.authType = AuthenticationType.authSub;
            this.privateKey = privateKey;
            this.authSubToken = authSubToken;
        }

        /// <summary>
        /// a constructor for a OAuth 2.0 authorization scenario.
        /// <param name="applicationName"></param>
        /// <param name="parameters">OAuth 2.0 parameters</param>
        public RequestSettings(
            string applicationName,
            OAuth2Parameters parameters)
            : this(applicationName)
        {
            this.authType = AuthenticationType.oAuth2;
            this.OAuth2Parameters = parameters;
        }

        /// <summary>
        /// returns the Credentials in case of a client login scenario
        /// </summary>
        /// <returns></returns>
        public GDataCredentials Credentials
        {
            get
            {
                return this.credentials;
            }
        }

        /// <summary>
        /// returns the authsub token to use for a webapplication scenario
        /// </summary>
        /// <returns></returns>
        public string AuthSubToken
        {
            get
            {
                return this.authSubToken;
            }
        }

        /// <summary>
        /// returns the private key used for authsub authentication
        /// </summary>
        /// <returns></returns>
        public AsymmetricAlgorithm PrivateKey
        {
            get
            {
                return this.privateKey;
            }
        }

        /// <summary>
        /// returns the application name
        /// </summary>
        /// <returns></returns>
        public string Application
        {
            get
            {
                return this.applicationName;
            }
        }

        /// <summary>
        /// returns the ConsumerKey
        /// </summary>
        /// <returns></returns>
        public string ConsumerKey
        {
            get
            {
                return this.consumerKey;
            }
        }

        /// <summary>
        /// returns the ConsumerSecret
        /// </summary>
        /// <returns></returns>
        public string ConsumerSecret
        {
            get
            {
                return this.consumerSecret;
            }
        }

        /// <summary>
        /// returns the Token for OAuth
        /// </summary>
        /// <returns></returns>
        public string Token
        {
            get
            {
                return this.token;
            }
        }

        /// <summary>
        /// returns the TokenSecret for OAuth
        /// </summary>
        /// <returns></returns>
        public string TokenSecret
        {
            get
            {
                return this.tokenSecret;
            }
        }

        /// <summary>
        /// returns the OAuth User
        /// </summary>
        /// <returns></returns>
        public string OAuthUser
        {
            get
            {
                return this.oAuthUser;
            }
        }

        /// <summary>
        /// returns the OAuth Domain
        /// </summary>
        /// <returns></returns>
        public string OAuthDomain
        {
            get
            {
                return this.oAuthDomain;
            }
        }

        /// <summary>
        /// the pagesize specifies how many entries should be retrieved per call. If not set,
        /// the server default will be used. Set it either to -1 (for default) or any value &gt; 0
        /// to set the pagesize to something the server should honor. Note, that this set's the
        /// max-results parameter on the query, and the server is free to ignore that and give you less
        /// entries than you have requested.
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>PageSize</c> property:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            settings.PageSize = 50;
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        /// <summary>
        /// AutoPaging specifies if a feed iterator should return to the server to fetch more data
        /// automatically. If set to false, a loop over feed.Entries will stop when the currently
        /// fetched set of data reaches it's end.  This is false by default. <seealso cref="RequestSettings.Maximum"/>
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>AutoPaging</c> property:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            settings.AutoPaging = true;
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public bool AutoPaging
        {
            get
            {
                return this.autoPage;
            }
            set
            {
                this.autoPage = value;
            }
        }

        /// <summary>
        /// the Maximum specifies how many entries should be retrieved in total. This works together with
        /// <seealso cref="RequestSettings.AutoPaging"/>. If set, AutoPaging of a feed will stop when the
        /// specified amount of entries was iterated over. If Maximum is smaller than PageSize (<seealso cref="RequestSettings.PageSize"/>),
        /// an exception is thrown. The default is -1 (ignored).
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Maximum</c> property:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            settings.PageSize = 50;
        ///            settings.AutoPaging = true;
        ///            settings.Maximum = 2000;
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public int Maximum
        {
            get
            {
                return this.max;
            }
            set
            {
                if (value < this.PageSize)
                {
                    throw new ArgumentException("Maximum must be greater or equal to PageSize");
                }
                this.max = value;
            }
        }

        /// <summary>gets and sets the Timeout property used for the created
        /// HTTPRequestObject in milliseconds. if you set it to -1 it will stick
        /// with the default of the HTTPRequestObject. From MSDN:
        /// The number of milliseconds to wait before the request times out.
        /// The default is 100,000 milliseconds (100 seconds).</summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Timeout</c> property:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            settings.Timout = 10000000;
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                this.timeout = value;
            }
        }

        /// <summary>gets and sets the SSL property used for the created
        /// HTTPRequestObject. If true, all requests will use https
        /// The default is false.</summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Timeout</c> property:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            settings.UseSSL = true;
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public bool UseSSL
        {
            get
            {
                return this.useSSL;
            }
            set
            {
                this.useSSL = value;
            }
        }

        /// <summary>
        /// ClientLoginHandler - this is the URI that is used to
        /// retrieve a client login authentication token
        /// </summary>
        /// <returns> </returns>
        public Uri ClientLoginHandler
        {
            get
            {
                return this.clientLoginHandler != null ?
                    this.clientLoginHandler : new Uri(GoogleAuthentication.UriHandler);
            }
            set { this.clientLoginHandler = value; }
        }

        public OAuth2Parameters OAuth2Parameters { get; set; }

        /// <summary>
        /// Creates a HttpWebRequest object that can be used against a given service.
        /// for a RequestSetting object that is using client login, this might call
        /// to get an authentication token from the service, if it is not already set.
        ///
        /// If this uses client login, and you need to use a proxy, set the application-wide
        /// proxy first using the GlobalProxySelection
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="httpMethod"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public HttpWebRequest CreateHttpWebRequest(string serviceName, string httpMethod, Uri targetUri)
        {
            if (this.UseSSL && (!targetUri.Scheme.ToLower().Equals("https")))
            {
                targetUri = new Uri("https://" + targetUri.Host + targetUri.PathAndQuery);
            }

            HttpWebRequest request = WebRequest.Create(targetUri) as HttpWebRequest;
            if (request == null)
            {
                throw new ArgumentException("targetUri does not resolve to an http request");
            }

            if (this.authType == AuthenticationType.clientLogin)
            {
                EnsureClientLoginCredentials(request, serviceName);
            }

            if (this.authType == AuthenticationType.authSub)
            {
                EnsureAuthSubCredentials(request);
            }

            if (this.authType == AuthenticationType.oAuth)
            {
                EnsureOAuthCredentials(request);
            }

            return request;
        }

        private void EnsureClientLoginCredentials(HttpWebRequest request, string serviceName)
        {
            if (String.IsNullOrEmpty(this.Credentials.ClientToken))
            {
                this.Credentials.ClientToken =
                    Utilities.QueryClientLoginToken(
                    this.Credentials,
                    serviceName,
                    this.Application,
                    false,
                    this.ClientLoginHandler);
            }

            if (!String.IsNullOrEmpty(this.Credentials.ClientToken))
            {
                string strHeader = GoogleAuthentication.Header + this.Credentials.ClientToken;
                request.Headers.Add(strHeader);
            }
        }

        private void EnsureAuthSubCredentials(HttpWebRequest request)
        {
            string header = AuthSubUtil.formAuthorizationHeader(
                this.Token,
                this.PrivateKey,
                request.RequestUri,
                request.Method);
            request.Headers.Add(header);
        }

        private void EnsureOAuthCredentials(HttpWebRequest request)
        {
            string oauthHeader = OAuthUtil.GenerateHeader(
                request.RequestUri,
                this.ConsumerKey,
                this.ConsumerSecret,
                this.Token,
                this.TokenSecret,
                request.Method);
            request.Headers.Add(oauthHeader);
        }
    }
}
