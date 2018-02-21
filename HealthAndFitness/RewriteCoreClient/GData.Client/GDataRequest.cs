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
    /// <summary>base GDataRequest implementation</summary> 
    public class GDataRequest : IGDataRequest, IDisposable, ISupportsEtag
    {
        /// <summary>holds the webRequest object</summary> 
        private WebRequest webRequest;
        /// <summary>holds the webresponse object</summary> 
        private WebResponse webResponse;
        /// <summary>holds the target Uri</summary> 
        private Uri targetUri;
        /// <summary>holds request type</summary> 
        private GDataRequestType type;
        /// <summary>holds the credential information</summary> 
        private GDataCredentials credentials;
        /// <summary>holds the request if a stream is open</summary> 
        private Stream requestStream;
        private GDataRequestFactory factory; // holds the factory to use
        /// <summary>holds if we are disposed</summary> 
        protected bool disposed;
        /// <summary>set whether or not this request should use GZip</summary>
        private bool useGZip;
        /// <summary>holds the timestamp for conditional GET</summary>
        private DateTime ifModifiedSince = DateTime.MinValue;
        /// <summary>stream from the response</summary>
        private Stream responseStream;
        /// <summary>holds the contenttype to use if overridden</summary>
        private string contentType;
        /// <summary>holds the slugheader to use if overridden</summary>
        private string slugHeader;
        // holds the returned contentlength
        private long contentLength;
        private string eTag;
        // used to refresh request for batching
        private AtomBase contentStore;
        private bool isBatch;

        /// <summary>default constructor</summary> 
        internal GDataRequest(GDataRequestType type, Uri uriTarget, GDataRequestFactory factory)
        {
            this.type = type;
            this.targetUri = uriTarget;
            this.factory = factory;
            this.useGZip = this.factory.UseGZip; // use gzip setting from factory
        }

        /// <summary>implements the disposable interface</summary> 
        public void Dispose()
        {
            if (this.responseStream != null)
            {
                this.responseStream.Close();
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// exposing the private targetUri so that subclasses can override
        /// the value for redirect handling
        /// </summary>
        protected Uri TargetUri
        {
            get
            {
                return this.targetUri;
            }
            set
            {
                this.targetUri = value;
            }
        }

        /// <summary>set whether or not this request should use GZip</summary>
        public bool UseGZip
        {
            get { return (this.useGZip); }
            set { this.useGZip = value; }
        }

        /// <summary>set a timestamp for conditional GET</summary>
        public DateTime IfModifiedSince
        {
            get { return (this.ifModifiedSince); }
            set { this.ifModifiedSince = value; }
        }

        /// <summary>does the real disposition</summary> 
        /// <param name="disposing">indicates if dispose called it or finalize</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Tracing.TraceMsg("disposing of request");
                Reset();
                this.disposed = true;
            }
        }

        /// <summary>accessor method for the GDataCredentials used</summary> 
        /// <returns> </returns>
        public GDataCredentials Credentials
        {
            get { return this.credentials; }
            set { this.credentials = value; }
        }

        /// <summary>sets and gets the content Type, used for binary transfers</summary> 
        /// <returns> </returns>
        public string ContentType
        {
            get { return this.contentType == null ? this.factory.ContentType : this.contentType; }
            set { this.contentType = value; }
        }

        /// <summary>sets and gets the etag header value, used for concurrency</summary> 
        /// <returns> </returns>
        public string Etag
        {
            get { return this.eTag; }
            set { this.eTag = value; }
        }

        /// <summary>copy of batch request content</summary>
        public AtomBase ContentStore
        {
            get { return this.contentStore; }
            set { this.contentStore = value; }
        }

        /// <summary>denotes if it's a batch request</summary>
        public bool IsBatch
        {
            get { return this.isBatch; }
            set { this.isBatch = value; }
        }

        /// <summary>sets and gets the slugHeader, used for binary transfers
        /// will encode to ascii and urlencode the string on setting it. 
        /// </summary>
        /// <returns> </returns>
        public string Slug
        {
            get { return this.slugHeader == null ? this.factory.Slug : this.slugHeader; }
            set
            {
                this.slugHeader = Utilities.EncodeSlugHeader(value);
            }
        }

        /// <summary>
        /// returnes the content-length of the response, -1 if none was given
        /// </summary>
        /// <returns></returns>
        public long ContentLength
        {
            get
            {
                return this.contentLength;
            }
        }

        /// <summary>accessor method protected WebRequest Request</summary> 
        /// <returns> </returns>
        protected WebRequest Request
        {
            get { return this.webRequest; }
            set { this.webRequest = value; }
        }

        /// <summary>accessor method protected WebResponse Response</summary> 
        /// <returns> </returns>
        protected WebResponse Response
        {
            get { return this.webResponse; }
            set { this.webResponse = value; }
        }

        /// <summary>resets the object's state</summary> 
        protected virtual void Reset()
        {
            this.requestStream = null;
            this.webRequest = null;
            if (this.webResponse != null)
            {
                this.webResponse.Close();
            }
            this.webResponse = null;
        }

        /// <summary>returns the writable request stream</summary> 
        /// <returns> the stream to write into</returns>
        public virtual Stream GetRequestStream()
        {
            EnsureWebRequest();
            this.requestStream = this.webRequest.GetRequestStream();
            return this.requestStream;
        }

        /// <summary>ensures that the correct HTTP verb is set on the stream</summary> 
        protected virtual void EnsureWebRequest()
        {
            if (this.webRequest == null && this.targetUri != null)
            {
                if (this.factory.UseSSL && !this.targetUri.Scheme.ToLower().Equals("https"))
                {
                    this.targetUri = new Uri("https://" + this.targetUri.Host + this.targetUri.PathAndQuery);
                }

                this.webRequest = WebRequest.Create(this.targetUri);

                this.webResponse = null;
                if (this.webRequest == null)
                {
                    throw new OutOfMemoryException("Could not create a new Webrequest");
                }

                HttpWebRequest web = this.webRequest as HttpWebRequest;

                if (web != null)
                {
                    switch (this.type)
                    {
                        case GDataRequestType.Delete:
                            web.Method = HttpMethods.Delete;
                            break;
                        case GDataRequestType.Update:
                            web.Method = HttpMethods.Put;
                            break;
                        case GDataRequestType.Batch:
                        case GDataRequestType.Insert:
                            web.Method = HttpMethods.Post;
                            break;
                        case GDataRequestType.Query:
                            web.Method = HttpMethods.Get;
                            break;
                    }

                    if (this.useGZip)
                    {
                        web.Headers.Set("Accept-Encoding", "gzip");
                    }

                    if (this.Etag != null)
                    {
                        if (this.Etag != GDataRequestFactory.IfMatchAll)
                        {
                            web.Headers.Set(GDataRequestFactory.EtagHeader, this.Etag);
                        }
                        switch (this.type)
                        {
                            case GDataRequestType.Update:
                            case GDataRequestType.Delete:
                                if (!Utilities.IsWeakETag(this))
                                {
                                    web.Headers.Set(GDataRequestFactory.IfMatch, this.Etag);
                                }
                                break;
                            case GDataRequestType.Query:
                                web.Headers.Set(GDataRequestFactory.IfNoneMatch, this.Etag);
                                break;
                        }
                    }

                    if (this.IfModifiedSince != DateTime.MinValue)
                    {
                        web.IfModifiedSince = this.IfModifiedSince;
                    }

                    web.ContentType = this.ContentType;
                    web.UserAgent = this.factory.UserAgent;
                    web.KeepAlive = this.factory.KeepAlive;
                    web.CookieContainer = this.factory.Cookies;

                    // add all custom headers
                    if (this.factory.hasCustomHeaders)
                    {
                        foreach (string s in this.factory.CustomHeaders)
                        {
                            this.Request.Headers.Add(s);
                        }
                    }

                    if (this.factory.Timeout != -1)
                    {
                        web.Timeout = this.factory.Timeout;
                    }

                    if (this.Slug != null)
                    {
                        this.Request.Headers.Set(GDataRequestFactory.SlugHeader, this.Slug);
                    }

                    if (this.factory.Proxy != null)
                    {
                        this.Request.Proxy = this.factory.Proxy;
                    }
                }

                EnsureCredentials();
            }
        }

        /// <summary>sets up the correct credentials for this call, pending 
        /// security scheme</summary> 
        protected virtual void EnsureCredentials()
        {
            if (this.Credentials != null)
            {
                this.webRequest.Credentials = this.Credentials.NetworkCredential;
            }
        }

        /// <summary>Logs the request object if overridden in subclass</summary>
        /// <param name="request">the request to log</param> 
        protected virtual void LogRequest(WebRequest request)
        {
        }

        /// <summary>Logs the response object if overridden in subclass</summary>
        /// <param name="response">the response to log</param>
        protected virtual void LogResponse(WebResponse response)
        {
        }

        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary> 
        public virtual void Execute()
        {
            try
            {
                EnsureWebRequest();
                // if we ever handed out a stream, we want to close it before doing the real excecution
                if (this.requestStream != null)
                {
                    this.requestStream.Close();
                }

                Tracing.TraceCall("calling the real execution over the webresponse");
                LogRequest(this.webRequest);
                this.webResponse = this.webRequest.GetResponse();
            }
            catch (WebException e)
            {
                Tracing.TraceCall("GDataRequest::Execute failed: " + this.targetUri.ToString());
                GDataRequestException gde = new GDataRequestException("Execution of request failed: " + this.targetUri.ToString(), e);
                throw gde;
            }

            if (this.webResponse != null)
            {
                this.responseStream = this.webResponse.GetResponseStream();
            }

            LogResponse(this.webResponse);
            if (this.webResponse is HttpWebResponse)
            {
                HttpWebResponse response = this.webResponse as HttpWebResponse;
                HttpWebRequest request = this.webRequest as HttpWebRequest;

                this.useGZip = (string.Compare(response.ContentEncoding, "gzip", true, CultureInfo.InvariantCulture) == 0);
                if (this.useGZip)
                {
                    this.responseStream = new GZipStream(this.responseStream, CompressionMode.Decompress);
                }

                Tracing.Assert(response != null, "The response should not be NULL");
                Tracing.Assert(request != null, "The request should not be NULL");

                int code = (int)response.StatusCode;

                Tracing.TraceMsg("Returned ContentType is: " + (response.ContentType == null ? "None" : response.ContentType) + " from URI : " + request.RequestUri.ToString()); ;
                Tracing.TraceMsg("Returned StatusCode is: " + response.StatusCode + code);

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    // that could imply that we need to reauthenticate
                    Tracing.TraceMsg("need to reauthenticate");
                    throw new GDataForbiddenException("Execution of request returned HttpStatusCode.Forbidden: " +
                        this.targetUri.ToString() + response.StatusCode.ToString(), this.webResponse);
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // a put went bad due to a version conflict
                    throw new GDataVersionConflictException("Execution of request returned HttpStatusCode.Conflict: " +
                        this.targetUri.ToString() + response.StatusCode.ToString(), this.webResponse);
                }

                if ((this.IfModifiedSince != DateTime.MinValue || this.Etag != null)
                    && response.StatusCode == HttpStatusCode.NotModified)
                {
                    // Throw an exception for conditional GET
                    throw new GDataNotModifiedException("Content not modified: " + this.targetUri.ToString(), this.webResponse);
                }

                if (response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.Found ||
                    response.StatusCode == HttpStatusCode.RedirectKeepVerb)
                {
                    Tracing.TraceMsg("throwing for redirect");
                    throw new GDataRedirectException("Execution resulted in a redirect from " + this.targetUri.ToString(), this.webResponse);
                }

                if (code > 299)
                {
                    // treat everything else over 300 as errors
                    throw new GDataRequestException("Execution of request returned unexpected result: " + this.targetUri.ToString() +
                        response.StatusCode.ToString(), this.webResponse);
                }

                this.contentLength = response.ContentLength;

                // if we got an etag back, remember it
                this.eTag = response.Headers[GDataRequestFactory.EtagHeader];

                response = null;
                request = null;
            }
        }

        /// <summary>gets the readable response stream</summary> 
        /// <returns> the response stream</returns>
        public virtual Stream GetResponseStream()
        {
            return (this.responseStream);
        }

        /// <summary>returns a valid web request with the correct credentials</summary> 
        /// <returns>the HTTP web request</returns>
        public HttpWebRequest GetFinalizedRequest()
        {
            this.EnsureWebRequest();
            this.EnsureCredentials();
            return this.Request as HttpWebRequest;
        }
    }
}
