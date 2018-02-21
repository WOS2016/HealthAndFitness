#region Using directives

#define USE_TRACING

using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

#endregion
namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>base Service interface definition
    /// </summary> 
    ////////////////////////////////////////////////////////////////////
    public interface IService
    {
        /// <summary>get/set for credentials to the service calls. Gets passed through to GDatarequest</summary> 
        GDataCredentials Credentials
        {
            get;
            set;
        }
        /// <summary>get/set for the GDataRequestFactory object to use</summary> 
        IGDataRequestFactory RequestFactory
        {
            get;
            set;
        }

        /// <summary>
        /// returns the name of the service identifier, like wise for spreadsheets services
        /// </summary>
        string ServiceIdentifier
        {
            get;
        }

        /// <summary>the minimal Get OpenSearchRssDescription function</summary> 
        Stream QueryOpenSearchRssDescription(Uri serviceUri);

        /// <summary>the minimal query implementation</summary> 
        AtomFeed Query(FeedQuery feedQuery);
        /// <summary>the minimal query implementation with conditional GET</summary> 
        AtomFeed Query(FeedQuery feedQuery, DateTime ifModifiedSince);
        /// <summary>simple update for atom resources</summary> 
        AtomEntry Update(AtomEntry entry);
        /// <summary>simple insert for atom entries, based on a feed</summary> 
        AtomEntry Insert(AtomFeed feed, AtomEntry entry);
        /// <summary>delete an entry</summary> 
        void Delete(AtomEntry entry);
        /// <summary>delete an entry</summary> 
        void Delete(Uri uriTarget);
        /// <summary>batch operation, posting of a set of entries</summary>
        AtomFeed Batch(AtomFeed feed, Uri batchUri);
        /// <summary>simple update for media resources</summary> 
        AtomEntry Update(Uri uriTarget, Stream input, string contentType, string slugHeader);
        /// <summary>simple insert for media resources</summary> 
        AtomEntry Insert(Uri uriTarget, Stream input, string contentType, string slugHeader);
    }

}
