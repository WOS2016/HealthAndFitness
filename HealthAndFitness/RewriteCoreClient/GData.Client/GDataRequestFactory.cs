#region Using directives

#define USE_TRACING

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>base GDataRequestFactory implementation</summary> 
    public class GDataRequestFactory : IGDataRequestFactory
    {
        /// <summary>this factory's agent</summary> 
        public const string GDataAgent = "SA";

        /// <summary>
        /// the default content type for the atom envelope
        /// </summary>
        public const string DefaultContentType = "application/atom+xml; charset=UTF-8";
        /// <summary>holds the user-agent</summary> 
        private string userAgent;
        private List<string> customHeaders;
        private IWebProxy webProxy;
        // whether or not to keep the connection alive
        private bool keepAlive;
        private bool useGZip;
        // whether https should be used
        private bool useSSL = true;
        private string contentType = DefaultContentType;
        private string slugHeader;
        // set to default by default
        private int timeOut = -1;

        /// <summary>Cookie setting header, returned from server</summary>
        public const string SetCookieHeader = "Set-Cookie";
        /// <summary>Cookie client header</summary>
        public const string CookieHeader = "Cookie";
        /// <summary>Slug client header</summary>
        public const string SlugHeader = "Slug";
        /// <summary>content override header for resumable upload</summary>
        public const string ContentOverrideHeader = "X-Upload-Content-Type";
        /// <summary>content length header for resumable upload</summary>
        public const string ContentLengthOverrideHeader = "X-Upload-Content-Length";

        /// <summary>
        /// constant for the Etag header
        /// </summary>
        public const string EtagHeader = "Etag";

        /// <summary>
        /// constant for the If-Match header
        /// </summary>
        public const string IfMatch = "If-Match";

        /// <summary>
        /// constant for the if-None-match header
        /// </summary>
        public const string IfNoneMatch = "If-None-Match";

        /// <summary>
        /// constant for the ifmatch value that matches everything
        /// </summary>
        public const string IfMatchAll = "*";

        /// <summary>default constructor</summary> 
        public GDataRequestFactory(string userAgent)
        {
            this.userAgent = Utilities.ConstructUserAgent(userAgent, this.GetType().Name);
            this.keepAlive = true;
        }

        /// <summary>default constructor</summary> 
        public virtual IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget)
        {
            return new GDataRequest(type, uriTarget, this);
        }

        /// <summary>whether or not new requests should use GZip</summary>
        public bool UseGZip
        {
            get { return this.useGZip; }
            set { this.useGZip = value; }
        }

        private CookieContainer cookies;
        /// <summary>The cookie container that is used for requests.</summary> 
        /// <returns> </returns>
        public CookieContainer Cookies
        {
            get
            {
                if (this.cookies == null)
                {
                    this.cookies = new CookieContainer();
                }

                return this.cookies;
            }
            set { this.cookies = value; }
        }

        /// <summary>sets and gets the Content Type, used for binary transfers</summary> 
        /// <returns> </returns>
        public string ContentType
        {
            get { return this.contentType; }
            set { this.contentType = value; }
        }

        /// <summary>sets and gets the slug header, used for binary transfers
        /// note that the data will be converted to ASCII and URLencoded on setting it
        /// </summary> 
        /// <returns> </returns>
        public string Slug
        {
            get { return this.slugHeader; }
            set
            {
                this.slugHeader = Utilities.EncodeSlugHeader(value);
            }
        }

        /// <summary>accessor method public string UserAgent</summary> 
        /// <returns> </returns>
        public virtual string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }

        /// <summary>accessor method to the webproxy object to use</summary> 
        /// <returns> </returns>
        public IWebProxy Proxy
        {
            get { return this.webProxy; }
            set { this.webProxy = value; }
        }

        /// <summary>indicates if the connection should be kept alive, default
        /// is true</summary> 
        /// <returns> </returns>
        public bool KeepAlive
        {
            get { return this.keepAlive; }
            set { this.keepAlive = value; }
        }

        /// <summary>indicates if the connection should use https</summary>
        /// <returns> </returns>
        public bool UseSSL
        {
            get { return this.useSSL; }
            set { this.useSSL = value; }
        }

        /// <summary>gets and sets the Timeout property used for the created
        /// HTTPRequestObject in milliseconds. if you set it to -1 it will stick 
        /// with the default of the HTTPRequestObject. From MSDN:
        /// The number of milliseconds to wait before the request times out. 
        /// The default is 100,000 milliseconds (100 seconds).</summary> 
        /// <returns> </returns>
        public int Timeout
        {
            get { return this.timeOut; }
            set { this.timeOut = value; }
        }

        internal bool hasCustomHeaders
        {
            get
            {
                return this.customHeaders != null;
            }
        }

        /// <summary>accessor method public StringArray CustomHeaders</summary> 
        /// <returns> </returns>
        public List<string> CustomHeaders
        {
            get
            {
                if (this.customHeaders == null)
                {
                    this.customHeaders = new List<string>();
                }
                return this.customHeaders;
            }
        }
    }
}
