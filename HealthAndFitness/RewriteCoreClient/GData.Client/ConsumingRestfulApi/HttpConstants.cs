using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //class HttpConstants
    //{
    //}
    /// <summary>a simple static collection of HTTP method strings </summary> 
    public static class HttpMethods
    {
        /// <summary>the delete method</summary> 
        public const string Delete = "DELETE";
        /// <summary>the post method</summary> 
        public const string Post = "POST";
        /// <summary>the put method</summary> 
        public const string Put = "PUT";
        /// <summary>the get method</summary> 
        public const string Get = "GET";
    }

    /// <summary>a simple static collection of HTTP form post strings </summary> 
    public static class HttpFormPost
    {
        /// <summary>form encoding</summary> 
        public const string Encoding = "application/x-www-form-urlencoded";
        /// <summary>expected return form contenttype</summary> 
        public const string ReturnContentType = "text";
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>enum to describe the different formats that query might return
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum AlternativeFormat
    {
        /// <summary>returns an atom format</summary>
        Atom,
        /// <summary>returns RSS 2.0</summary>
        Rss,                        /// 
                                    /// <summary>returns the Open RSS 2.0s</summary>
        OpenSearchRss,
        /// <summary>parsing error</summary>
        Unknown
    }
    /////////////////////////////////////////////////////////////////////////////

}
