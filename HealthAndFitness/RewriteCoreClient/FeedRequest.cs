using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
////using Google.GData.Client;
//using Google.GData.Extensions;
using System.Collections.Generic;
//using Google.GData.Extensions.AppControl;
using System.Security.Cryptography;
using System.ComponentModel;
using RewriteCoreClient.GData.Client;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// base class for Request objects.
    /// </summary>
    /// <returns></returns>
    public abstract class FeedRequest<T> where T : Service
    {
        private RequestSettings settings;
        private T atomService;

        /// <summary>
        /// default constructor based on a RequestSettings object
        /// </summary>
        /// <param name="settings"></param>
        public FeedRequest(RequestSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// prepares the created service based on the settings
        /// </summary>
        protected void PrepareService()
        {
            PrepareService(this.atomService);
        }

        /// <summary>
        /// prepares the passed in service by setting the authentication credentials and the timeout settings
        /// </summary>
        /// <param name="s"></param>
        protected void PrepareService(Service s)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            if (settings.Credentials != null)
            {
                s.Credentials = settings.Credentials;
            }

            if (settings.AuthSubToken != null)
            {
                GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory(s.ServiceIdentifier, settings.Application);
                authFactory.UserAgent = authFactory.UserAgent + "--IEnumerable";
                authFactory.Token = settings.AuthSubToken;
                authFactory.PrivateKey = settings.PrivateKey;
                s.RequestFactory = authFactory;
            }
            else if (settings.ConsumerKey != null)
            {
                // let's create an OAuth factory
                GOAuthRequestFactory authFactory = new GOAuthRequestFactory(s.ServiceIdentifier, settings.Application);
                authFactory.ConsumerKey = settings.ConsumerKey;
                authFactory.ConsumerSecret = settings.ConsumerSecret;
                authFactory.Token = settings.Token;
                authFactory.TokenSecret = settings.TokenSecret;
                s.RequestFactory = authFactory;
            }
            else if (settings.OAuth2Parameters != null)
            {
                s.RequestFactory = new GOAuth2RequestFactory(s.ServiceIdentifier, settings.Application, settings.OAuth2Parameters);
            }
            else
            {
                GDataGAuthRequestFactory authFactory = s.RequestFactory as GDataGAuthRequestFactory;
                if (authFactory != null)
                {
                    authFactory.UserAgent = authFactory.UserAgent + "--IEnumerable";
                }
            }

            if (settings.Timeout != -1)
            {
                GDataRequestFactory f = s.RequestFactory as GDataRequestFactory;
                if (f != null)
                {
                    f.Timeout = settings.Timeout;
                }
            }

            s.RequestFactory.UseSSL = settings.UseSSL;
        }

        /// <summary>
        /// creates a query object and sets it up based on the settings object.
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected Y PrepareQuery<Y>(string uri) where Y : FeedQuery, new()
        {
            Y query = new Y();
            query.BaseAddress = uri;

            PrepareQuery(query);
            return query;
        }

        /// <summary>
        /// prepares the passed in query objects properties based on the settings
        /// </summary>
        /// <param name="q"></param>
        protected void PrepareQuery(FeedQuery q)
        {
            FeedQuery.PrepareQuery(q, this.settings);
        }

        /// <summary>
        /// should be used in subclasses to create URIs from strings, so that the OAuth parameters can be
        /// attached
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        protected Uri CreateUri(string location)
        {
            Uri retUri = null;

            if (this.settings.OAuthUser != null && location.IndexOf(OAuthUri.OAuthParameter) != 0)
            {
                retUri = new OAuthUri(location, this.settings.OAuthUser, this.settings.OAuthDomain);
            }
            else
            {
                retUri = new Uri(location);
            }

            return retUri;
        }

        /// <summary>
        /// creates a feed of Y object based on the query and the settings
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        protected virtual Feed<Y> PrepareFeed<Y>(FeedQuery q) where Y : Entry, new()
        {
            PrepareQuery(q);
            Feed<Y> f = CreateFeed<Y>(q);
            f.Settings = this.settings;
            f.AutoPaging = this.settings.AutoPaging;
            f.Maximum = this.settings.Maximum;
            return f;
        }

        /// <summary>
        /// the virtual creator function for feeds, so that we can create feedsubclasses in
        /// in subclasses of the request
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        protected virtual Feed<Y> CreateFeed<Y>(FeedQuery q) where Y : Entry, new()
        {
            return new Feed<Y>(this.atomService, q);
        }

        /// <summary>
        /// gets a feed object of type T
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public Feed<Y> Get<Y>(FeedQuery q) where Y : Entry, new()
        {
            return PrepareFeed<Y>(q);
        }

        /// <summary>
        /// gets a feed object of type T
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="uri">The Uri to retrieve</param>
        /// <returns></returns>
        public Feed<Y> Get<Y>(Uri uri) where Y : Entry, new()
        {
            FeedQuery q = new FeedQuery(uri.AbsoluteUri);
            return PrepareFeed<Y>(q);
        }

        /// <summary>
        /// sets the proxy on the service to be used.
        /// </summary>
        /// <returns></returns>
        public IWebProxy Proxy
        {
            get
            {
                GDataRequestFactory x = this.atomService.RequestFactory as GDataRequestFactory;
                if (x != null)
                {
                    return x.Proxy;
                }
                return null;
            }
            set
            {
                GDataRequestFactory x = this.atomService.RequestFactory as GDataRequestFactory;
                if (x != null)
                {
                    x.Proxy = value;
                    OnSetOtherProxies(value);
                }
                else
                {
                    throw new ArgumentException("Can not set a proxy on this service");
                }
            }
        }

        /// <summary>
        /// called to set additional proxies if required. Overloaded on the document service
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        protected virtual void OnSetOtherProxies(IWebProxy proxy)
        {
        }

        /// <summary></summary>
        /// <returns></returns>
        protected Service AtomService
        {
            get
            {
                return this.atomService;
            }
        }

        /// <summary>
        /// returns a new feed based on the operation passed in.  This is useful if you either do not use
        /// autopaging, or want to move to previous parts of the feed, or get a refresh of the current feed
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Get</c> method:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            YouTubeRequest f = new YouTubeRequest(settings);
        ///             Feed&lt;Playlist&gt; feed = f.GetPlaylistsFeed(null);
        ///             Feed&lt;Playlist&gt; next = f.Get&lt;Playlist&gt;(feed, FeedRequestType.Next);
        ///  </code>
        ///  </example>
        /// <param name="feed">the original feed</param>
        /// <param name="operation">an requesttype to indicate what to retrieve</param>
        /// <returns></returns>
        public Feed<Y> Get<Y>(Feed<Y> feed, FeedRequestType operation) where Y : Entry, new()
        {
            Feed<Y> f = null;
            string spec = null;

            if (feed == null)
            {
                throw new ArgumentNullException("feed was null");
            }

            if (feed.AtomFeed == null)
            {
                throw new ArgumentNullException("feed.AtomFeed was null");
            }

            switch (operation)
            {
                case FeedRequestType.Next:
                    spec = feed.AtomFeed.NextChunk;
                    break;
                case FeedRequestType.Prev:
                    spec = feed.AtomFeed.PrevChunk;
                    break;
                case FeedRequestType.Refresh:
                    spec = feed.AtomFeed.Self;
                    break;
            }

            if (!String.IsNullOrEmpty(spec))
            {
                FeedQuery q = new FeedQuery(spec);
                if (operation == FeedRequestType.Refresh)
                {
                    ISupportsEtag ise = feed.AtomFeed as ISupportsEtag;
                    if (ise != null && ise.Etag != null)
                    {
                        q.Etag = ise.Etag;
                    }
                }
                f = PrepareFeed<Y>(q);
            }

            return f;
        }

        /// <summary>
        /// takes an existing stream and creates Feed of entries out of it
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="inputStream"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public Feed<Y> Parse<Y>(Stream inputStream, Uri targetUri) where Y : Entry, new()
        {
            if (targetUri == null)
            {
                throw new ArgumentNullException("targetUri can not be null");
            }
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream can not be null");
            }

            AtomFeed feed = this.Service.CreateAndParseFeed(inputStream, targetUri);
            return new Feed<Y>(feed);
        }

        /// <summary>
        /// takes an existing stream and creates just one entry (the first in the stream)
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public Y ParseEntry<Y>(Stream inputStream, Uri targetUri) where Y : Entry, new()
        {
            Feed<Y> f = Parse<Y>(inputStream, targetUri);
            foreach (Y y in f.Entries)
            {
                return y;
            }
            return null;
        }

        /// <summary>
        /// performs a batch operation.
        /// </summary>
        /// <param name="feed">the original feed, used to find the batch endpoing </param>
        /// <param name="entries">List of entries of type Y, that are to be batched</param>
        /// <returns></returns>
        public Feed<Y> Batch<Y>(List<Y> entries, Feed<Y> feed) where Y : Entry, new()
        {
            return this.Batch(entries, feed, GDataBatchOperationType.Default);
        }

        /// <summary>
        /// performs a batch operation.
        /// </summary>
        /// <param name="feed">the original feed, used to find the batch endpoing </param>
        /// <param name="entries">List of entries of type Y, that are to be batched</param>
        /// <param name="defaultOperation">indicates the default batch operationtype</param>
        /// <returns></returns>
        public Feed<Y> Batch<Y>(List<Y> entries, Feed<Y> feed, GDataBatchOperationType defaultOperation) where Y : Entry, new()
        {
            if (feed == null || feed.AtomFeed == null)
            {
                throw new ArgumentNullException("Invalid feed passed in");
            }

            if (feed.AtomFeed.Batch == null)
            {
                throw new ArgumentException("Feed has no valid batch endpoint");
            }
            return this.Batch(entries, new Uri(feed.AtomFeed.Batch), defaultOperation);
        }

        /// <summary>
        /// performs a batch operation.
        /// </summary>
        /// <param name="batchUri">the batch endpoint of the service</param>
        /// <param name="entries">List of entries of type Y, that are to be batched</param>
        /// <param name="defaultOperation">The default operation to be used for all entries</param>
        /// <returns></returns>
        public Feed<Y> Batch<Y>(List<Y> entries, Uri batchUri, GDataBatchOperationType defaultOperation) where Y : Entry, new()
        {
            if (entries.Count > 0)
            {
                AtomFeed batchFeed = new AtomFeed(batchUri, null);
                batchFeed.BatchData = new GDataBatchFeedData();
                batchFeed.BatchData.Type = defaultOperation;
                foreach (Y e in entries)
                {
                    batchFeed.Entries.Add(e.AtomEntry);
                }

                FeedQuery q = PrepareQuery<FeedQuery>(batchUri.AbsoluteUri);

                AtomFeed resultFeed = this.Service.Batch(batchFeed, q.Uri);
                Feed<Y> f = new Feed<Y>(resultFeed);
                return f;
            }

            return null;
        }

        /// <summary>
        /// returns the service instance that is used
        /// </summary>
        public T Service
        {
            get
            {
                return this.atomService;
            }
            set
            {
                this.atomService = value;
            }
        }

        /// <summary>
        /// returns a refreshed version of the entry you passed in, by going back to the server and
        /// requesting this resource again
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Get</c> method:
        ///          <code>
        ///           YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        ///            YouTubeRequest f = new YouTubeRequest(settings);
        ///             Feed&lt;Playlist&gt; feed = f.GetPlaylistsFeed(null);
        ///             Feed&lt;Playlist&gt; next = f.Get&lt;Playlist&gt;(feed, FeedRequestType.Next);
        ///  </code>
        ///  </example>
        /// <param name="entry">the entry to get again</param>
        /// <returns></returns>
        public Y Retrieve<Y>(Y entry) where Y : Entry, new()
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry was null");
            }

            if (entry.AtomEntry == null)
            {
                throw new ArgumentNullException("entry.AtomEntry was null");
            }

            string spec = entry.AtomEntry.SelfUri.ToString();

            if (!String.IsNullOrEmpty(spec))
            {
                FeedQuery q = new FeedQuery(spec);
                ISupportsEtag ise = entry.AtomEntry as ISupportsEtag;
                if (ise != null && ise.Etag != null)
                {
                    q.Etag = ise.Etag;
                }
                return Retrieve<Y>(q);
            }
            return null;
        }

        /// <summary>
        /// returns the entry the Uri pointed to
        /// </summary>
        /// <param name="entryUri">the Uri of the entry</param>
        /// <returns></returns>
        public Y Retrieve<Y>(Uri entryUri) where Y : Entry, new()
        {
            string spec = entryUri.AbsoluteUri;
            if (!String.IsNullOrEmpty(spec))
            {
                FeedQuery q = new FeedQuery(spec);
                return Retrieve<Y>(q);
            }
            return null;
        }

        /// <summary>
        /// returns a the entry the Uri pointed to
        /// </summary>
        ///  <example>
        /// <param name="entryUri">the Uri of the entry</param>
        /// <returns></returns>
        public Y Retrieve<Y>(FeedQuery query) where Y : Entry, new()
        {
            Feed<Y> f = null;
            Y r = null;
            f = PrepareFeed<Y>(query);
            // this should be a feed of one...
            foreach (Y y in f.Entries)
            {
                r = y;
            }
            return r;
        }

        /// <summary>
        /// sends the data back to the server.
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Update<Y>(Y entry) where Y : Entry, new()
        {
            if (entry == null)
            {
                throw new ArgumentNullException("Entry was null");
            }

            if (entry.AtomEntry == null)
            {
                throw new ArgumentNullException("Entry.AtomEntry was null");
            }

            Y r = null;

            FeedQuery q = PrepareQuery<FeedQuery>(entry.AtomEntry.EditUri.ToString());
            Stream s = this.Service.EntrySend(q.Uri, entry.AtomEntry, GDataRequestType.Update, null);
            AtomEntry ae = this.Service.CreateAndParseEntry(s, new Uri(entry.AtomEntry.EditUri.ToString()));

            if (ae != null)
            {
                r = new Y();
                r.AtomEntry = ae;
            }

            return r;
        }

        /// <summary>
        /// deletes the Entry from the Server
        /// </summary>
        public void Delete<Y>(Y entry) where Y : Entry, new()
        {
            if (entry == null)
            {
                throw new ArgumentNullException("Entry was null");
            }

            if (entry.AtomEntry == null)
            {
                throw new ArgumentNullException("Entry.AtomEntry was null");
            }

            if (entry.AtomEntry.EditUri == null)
            {
                throw new ArgumentNullException("The AtomEntry has no EditUri");
            }

            FeedQuery q = PrepareQuery<FeedQuery>(entry.AtomEntry.EditUri.ToString());
            this.Service.Delete(q.Uri, entry.ETag);
        }

        public void Delete(Uri targetUrl, string eTag)
        {
            FeedQuery q = PrepareQuery<FeedQuery>(targetUrl.AbsoluteUri);
            this.Service.Delete(q.Uri, eTag);
        }

        /// <summary>
        /// takes the given Entry and inserts its into the server
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Insert<Y>(Uri address, Y entry) where Y : Entry, new()
        {
            if (entry == null)
            {
                throw new ArgumentNullException("Entry was null");
            }

            if (entry.AtomEntry == null)
            {
                throw new ArgumentNullException("Entry.AtomEntry was null");
            }

            if (address == null)
            {
                throw new ArgumentNullException("Address was null");
            }

            Y r = null;
            FeedQuery q = PrepareQuery<FeedQuery>(address.AbsoluteUri);

            AtomEntry ae = this.Service.Insert(q.Uri, entry.AtomEntry);
            if (ae != null)
            {
                r = new Y();
                r.AtomEntry = ae;
            }

            return r;
        }

        /// <summary>
        /// takes the given Entry and inserts it into the server
        /// </summary>
        /// <returns>the reflected entry from the server if any given</returns>
        public Y Insert<Y>(Feed<Y> feed, Y entry) where Y : Entry, new()
        {
            return Insert(new Uri(feed.AtomFeed.Post), entry);
        }

        /// <summary>
        /// the Settings property returns the RequestSettings object that was used to construct this FeedRequest.
        /// It can be used to alter properties like AutoPaging etc, inbetween Feed creations.
        /// </summary>
        ///  <example>
        ///         The following code illustrates a possible use of
        ///          the <c>Settings</c> property:
        ///          <code>
        ///         YouTubeRequestSettings settings = new YouTubeRequestSettings("NETUnittests", this.ytClient, this.ytDevKey, this.ytUser, this.ytPwd);
        ///         YouTubeRequest f = new YouTubeRequest(settings);
        ///         Feed&lt;Video&gt; feed = f.GetStandardFeed(YouTubeQuery.MostPopular);
        ///         foreach (Video v in feed.Entries)
        ///         {
        ///             f.Settings.PageSize = 50;
        ///             f.Settings.AutoPaging = true;
        ///             Feed&lt;Comment&gt; list = f.GetComments(v);
        ///             foreach (Comment c in list.Entries)
        ///              {
        ///                 Assert.IsTrue(v.AtomEntry != null);
        ///                  Assert.IsTrue(v.Title != null);
        ///             }
        ///           }
        ///  </code>
        ///  </example>
        /// <returns></returns>
        public RequestSettings Settings
        {
            get
            {
                return this.settings;
            }
        }
    }
}
