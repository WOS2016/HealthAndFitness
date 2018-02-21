using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;

using System.Collections.Generic;
using System.Security.Cryptography;
using System.ComponentModel;

namespace RewriteCoreClient.GData.Client
{
    public class Feed<T> where T : Entry, new()
    {
        AtomFeed af;
        bool paging;
        int maximum = -1;
        int numberRetrieved = 0;
        Service service;
        FeedQuery query;
        RequestSettings settings;

        /// <summary>
        /// default constructor that takes the underlying atomfeed
        /// </summary>
        /// <param name="af"></param>
        public Feed(AtomFeed af)
        {
            this.af = af;
        }

        /// <summary>
        /// constructs a new feed object based on a service and a query
        /// </summary>
        /// <param name="service"></param>
        /// <param name="q"></param>
        public Feed(Service service, FeedQuery q)
        {
            this.service = service;
            this.query = q;
        }

        /// <summary>
        /// returns the used feed object
        /// </summary>
        /// <returns></returns>
        public AtomFeed AtomFeed
        {
            get
            {
                if (this.af == null)
                {
                    if (this.service != null && this.query != null)
                    {
                        this.af = this.service.Query(query);
                    }
                }
                return this.af;
            }
        }

        /// <summary>
        /// if set to true will cause the feed to add more data when you iterate over it's entries
        /// </summary>
        /// <returns></returns>
        public bool AutoPaging
        {
            get
            {
                return this.paging;
            }
            set
            {
                this.paging = value;
            }
        }

        /// <summary>
        /// returns the position in the real feed of the first entry in this feed
        /// </summary>
        /// <returns>an int indicating the start in the feed</returns>
        public int StartIndex
        {
            get
            {
                if (this.AtomFeed != null)
                {
                    return this.AtomFeed.StartIndex;
                }
                return -1;
            }
        }

        /// <summary>
        /// returns the setup paging size of this feed. If you set AutoPaging to true
        /// this is the size that is used to get more results
        /// </summary>
        /// <returns></returns>
        public int PageSize
        {
            get
            {
                if (this.AtomFeed != null)
                {
                    return this.AtomFeed.ItemsPerPage;
                }
                return -1;
            }
        }

        /// <summary>
        /// returns the number of entries the server believes the feed holds
        /// </summary>
        /// <returns></returns>
        public int TotalResults
        {
            get
            {
                if (this.AtomFeed != null)
                {
                    return this.AtomFeed.TotalResults;
                }
                return -1;
            }
        }

        /// <summary>
        /// the maximum number of entries to be retrieved. This is normally
        /// setup using the RequestSettings when the feed is constructed.
        /// </summary>
        /// <returns></returns>
        //public int Maximum
        //{
        //    get
        //    {
        //        return this.maximum;
        //    }
        //    set
        //    {
        //        this.maximum = value;
        //    }
        //}

        public int Maximum { get; set; }
        /// <summary>
        /// accessor for the RequestSettings used to construct the feed. Needed to
        /// construct a query that takes auth into account
        /// </summary>
        internal RequestSettings Settings
        {
            get
            {
                return this.settings;
            }
            set
            {
                this.settings = value;
            }
        }

        /// <summary>
        /// returns the initial list of entries.This page is the data
        /// you got from the Requestobject and will remain constant.
        /// Unless you set AutoPaging to true, in that case:
        /// This will go back to the server and fetch data again if
        /// needed. Example. If you pagesize is 30, you get an initial set of
        /// 30 entries. While enumerating, when reaching 30, the code will go
        /// to the server and get the next 30 rows. It will continue to do so
        /// until the server reports no more rows available.
        /// Note that you should cache the entries returned in a list of your own
        /// if you want to access them more than once, as this one does no caching on
        /// it's own.
        /// </summary>
        /// <example>
        /// The following code illustrates a possible use of
        /// the <c>Entries</c> property:
        /// <code>
        /// YouTubeRequestSettings settings = new YouTubeRequestSettings("yourApp", "yourClient", "yourKey", "username", "pwd");
        /// YouTubeRequest f = new YouTubeRequest(settings);
        /// Feed&lt;Playlist&gt; feed = f.GetPlaylistsFeed(null);
        /// foreach (Vidoe v in feed.Entries)
        /// </code>
        /// </example>
        /// <returns></returns>
        public IEnumerable<T> Entries
        {
            get
            {
                bool looping;

                if (this.AtomFeed == null)
                {
                    yield break;   ////Can think of  yield break as a return statement which does not return a value.
                }

                AtomFeed originalFeed = this.AtomFeed;

                this.numberRetrieved = 0;

                do
                {
                    looping = af.NextChunk != null && this.paging;
                    foreach (AtomEntry e in af.Entries)
                    {
                        T t = new T();
                        if (t != null)
                        {
                            t.AtomEntry = e;
                            this.numberRetrieved++;
                            yield return t;
                        }
                        if (this.Maximum > 0 && this.numberRetrieved >= this.Maximum)
                        {
                            yield break;
                        }
                    }
                    if (looping)
                    {
                        FeedQuery q = new FeedQuery(this.AtomFeed.NextChunk);
                        FeedQuery.PrepareQuery(q, this.settings);
                        this.af = this.AtomFeed.Service.Query(q);
                    }
                } while (looping);

                // we are done, reset the feed to the start
                this.af = originalFeed;
            }
        }
    }
}
