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
    /// <summary>base GDataRequest implementation</summary> 
    public class GDataGAuthRequest : GDataRequest
    {
        /// <summary>holds the input in memory stream</summary> 
        private MemoryStream requestCopy;
        /// <summary>holds the factory instance</summary> 
        private GDataGAuthRequestFactory factory;
        private AsyncData asyncData;
        private VersionInformation responseVersion;

        /// <summary>default constructor</summary> 
        internal GDataGAuthRequest(GDataRequestType type, Uri uriTarget, GDataGAuthRequestFactory factory)
            : base(type, uriTarget, factory as GDataRequestFactory)
        {
            // need to remember the factory, so that we can pass the new authtoken back there if need be
            this.factory = factory;
        }

        /// <summary>returns the writable request stream</summary> 
        /// <returns> the stream to write into</returns>
        public override Stream GetRequestStream()
        {
            this.requestCopy = new MemoryStream();
            return this.requestCopy;
        }

        /// <summary>Read only accessor for requestCopy</summary> 
        internal Stream RequestCopy
        {
            get { return this.requestCopy; }
        }

        internal AsyncData AsyncData
        {
            set
            {
                this.asyncData = value;
            }
        }

        /// <summary>does the real disposition</summary> 
        /// <param name="disposing">indicates if dispose called it or finalize</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.requestCopy != null)
                {
                    this.requestCopy.Close();
                    this.requestCopy = null;
                }
                this.disposed = true;
            }
        }

        /// <summary>sets up the correct credentials for this call, pending 
        /// security scheme</summary> 
        protected override void EnsureCredentials()
        {
            Tracing.Assert(this.Request != null, "We should have a webrequest now");
            if (this.Request == null)
            {
                return;
            }

            // if the token is NULL, we need to get a token. 
            if (this.factory.GAuthToken == null)
            {
                // we will take the standard credentials for that
                GDataCredentials gc = this.Credentials;
                Tracing.TraceMsg(gc == null ? "No Network credentials set" : "Network credentials found");
                if (gc != null)
                {
                    // only now we have something to do... 
                    this.factory.GAuthToken = QueryAuthToken(gc);
                }
            }

            if (this.factory.GAuthToken != null && this.factory.GAuthToken.Length > 0)
            {
                // Tracing.Assert(this.factory.GAuthToken != null, "We should have a token now"); 
                Tracing.TraceMsg("Using auth token: " + this.factory.GAuthToken);
                string strHeader = GoogleAuthentication.Header + this.factory.GAuthToken;
                this.Request.Headers.Add(strHeader);
            }
        }

        /// <summary>
        /// returns the version information that the response indicated
        /// can be NULL if used against a non versioned endpoint
        /// </summary>
        internal VersionInformation ResponseVersion
        {
            get
            {
                return this.responseVersion;
            }
        }

        /// <summary>sets the redirect to false after everything else
        /// is done </summary> 
        protected override void EnsureWebRequest()
        {
            base.EnsureWebRequest();
            HttpWebRequest http = this.Request as HttpWebRequest;
            if (http != null)
            {
                http.Headers.Remove(GDataGAuthRequestFactory.GDataVersion);

                // as we are doublebuffering due to redirect issues anyhow, 
                // disallow the default buffering
                http.AllowWriteStreamBuffering = false;

                IVersionAware v = this.factory as IVersionAware;
                if (v != null)
                {
                    // need to add the version header to the request
                    http.Headers.Set(GDataGAuthRequestFactory.GDataVersion, v.ProtocolMajor.ToString() + "." + v.ProtocolMinor.ToString());
                }

                // we do not want this to autoredirect, our security header will be 
                // lost in that case
                http.AllowAutoRedirect = false;
                if (this.factory.MethodOverride &&
                    http.Method != HttpMethods.Get &&
                    http.Method != HttpMethods.Post)
                {
                    // remove it, if it is already there.
                    http.Headers.Remove(GoogleAuthentication.Override);

                    // cache the method, because Mono will complain if we try
                    // to open the request stream with a DELETE method.
                    string currentMethod = http.Method;

                    http.Headers.Set(GoogleAuthentication.Override, currentMethod);
                    http.Method = HttpMethods.Post;

                    // not put and delete, all is post
                    if (currentMethod == HttpMethods.Delete)
                    {
                        http.ContentLength = 0;
                        // .NET CF won't send the ContentLength parameter if no stream
                        // was opened. So open a dummy one, and close it right after.
                        Stream req = http.GetRequestStream();
                        req.Close();
                    }
                }
            }
        }

        /// <summary>goes to the Google auth service, and gets a new auth token</summary> 
        /// <returns>the auth token, or NULL if none received</returns>
        internal string QueryAuthToken(GDataCredentials gc)
        {
            Uri authHandler = null;

            // need to copy this to a new object to avoid that people mix and match
            // the old (factory) and the new (requestsettings) and get screwed. So
            // copy the settings from the gc passed in and mix with the settings from the factory
            GDataCredentials gdc = new GDataCredentials(gc.Username, gc.getPassword());
            gdc.CaptchaToken = this.factory.CaptchaToken;
            gdc.CaptchaAnswer = this.factory.CaptchaAnswer;
            gdc.AccountType = gc.AccountType;

            try
            {
                authHandler = new Uri(this.factory.Handler);
            }
            catch
            {
                throw new GDataRequestException("Invalid authentication handler URI given");
            }

            return Utilities.QueryClientLoginToken(
                gdc,
                this.factory.Service,
                this.factory.ApplicationName,
                this.factory.KeepAlive,
                this.factory.Proxy,
                authHandler);
        }

        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary>
        public override void Execute()
        {
            // call it the first time
            Execute(1);
        }

        /// <summary>Executes the request and prepares the response stream. Also 
        /// does error checking</summary> 
        /// <param name="retryCounter">indicates the n-th time this is run</param>
        protected void Execute(int retryCounter)
        {
            Tracing.TraceCall("GoogleAuth: Execution called");
            try
            {
                CopyRequestData();
                base.Execute();
                if (this.Response is HttpWebResponse)
                {
                    HttpWebResponse response = this.Response as HttpWebResponse;
                    this.responseVersion = new VersionInformation(response.Headers[GDataGAuthRequestFactory.GDataVersion]);
                }
            }
            catch (GDataForbiddenException)
            {
                Tracing.TraceMsg("need to reauthenticate, got a forbidden back");
                // do it again, once, reset AuthToken first and streams first
                Reset();
                this.factory.GAuthToken = null;
                CopyRequestData();
                base.Execute();
            }
            catch (GDataRedirectException re)
            {
                // we got a redirect.
                Tracing.TraceMsg("Got a redirect to: " + re.Location);
                // only reset the base, the auth cookie is still valid
                // and cookies are stored in the factory
                if (this.factory.StrictRedirect)
                {
                    HttpWebRequest http = this.Request as HttpWebRequest;
                    if (http != null)
                    {
                        // only redirect for GET, else throw
                        if (http.Method != HttpMethods.Get)
                        {
                            throw;
                        }
                    }
                }

                // verify that there is a non empty location string
                if (re.Location.Trim().Length == 0)
                {
                    throw;
                }

                Reset();
                this.TargetUri = new Uri(re.Location);
                CopyRequestData();
                base.Execute();
            }
            catch (GDataRequestException re)
            {
                HttpWebResponse webResponse = re.Response as HttpWebResponse;
                if (webResponse != null && webResponse.StatusCode != HttpStatusCode.InternalServerError)
                {
                    Tracing.TraceMsg("Not a server error. Possibly a Bad request or forbidden resource.");
                    Tracing.TraceMsg("We don't want to retry non 500 errors.");
                    throw;
                }
                if (retryCounter > this.factory.NumberOfRetries)
                {
                    Tracing.TraceMsg("Number of retries exceeded");
                    throw;
                }
                Tracing.TraceMsg("Let's retry this");
                // only reset the base, the auth cookie is still valid
                // and cookies are stored in the factory
                Reset();
                this.Execute(retryCounter + 1);
            }
            catch (Exception e)
            {
                Tracing.TraceCall("*** EXCEPTION " + e.GetType().Name + " CAUGHT ***");
                throw;
            }
            finally
            {
                if (this.requestCopy != null)
                {
                    this.requestCopy.Close();
                    this.requestCopy = null;
                }
            }
        }

        /// <summary>takes our copy of the stream, and puts it into the request stream</summary> 
        protected void CopyRequestData()
        {
            if (this.requestCopy != null)
            {
                // Since we don't use write buffering on the WebRequest object,
                // we need to ensure the Content-Length field is correctly set
                // to the length we want to set.
                EnsureWebRequest();
                this.Request.ContentLength = this.requestCopy.Length;
                // stream it into the real request stream
                Stream req = base.GetRequestStream();

                try
                {
                    const int size = 4096;
                    byte[] bytes = new byte[size];
                    int numBytes;

                    double oneLoop = 100;
                    if (requestCopy.Length > size)
                    {
                        oneLoop = (100 / ((double)this.requestCopy.Length / size));
                    }

                    // 3 lines of debug code
                    // this.requestCopy.Seek(0, SeekOrigin.Begin);

                    // StreamReader reader = new StreamReader( this.requestCopy );
                    // string text = reader.ReadToEnd();

                    this.requestCopy.Seek(0, SeekOrigin.Begin);

                    long bytesWritten = 0;
                    double current = 0;
                    while ((numBytes = this.requestCopy.Read(bytes, 0, size)) > 0)
                    {
                        req.Write(bytes, 0, numBytes);
                        bytesWritten += numBytes;
                        if (this.asyncData != null && this.asyncData.Delegate != null &&
                            this.asyncData.DataHandler != null)
                        {
                            AsyncOperationProgressEventArgs args;
                            args = new AsyncOperationProgressEventArgs(this.requestCopy.Length,
                                bytesWritten, (int)current,
                                this.Request.RequestUri,
                                this.Request.Method,
                                this.asyncData.UserData);
                            current += oneLoop;
                            if (!this.asyncData.DataHandler.SendProgressData(asyncData, args))
                                break;
                        }
                    }
                }
                finally
                {
                    req.Close();
                }
            }
            else
            {
                if (this.IsBatch)
                {
                    this.EnsureWebRequest();
                    this.ContentStore.SaveToXml(this.GetRequestStream());
                    this.CopyRequestData();
                }
            }
        }
    }
}
