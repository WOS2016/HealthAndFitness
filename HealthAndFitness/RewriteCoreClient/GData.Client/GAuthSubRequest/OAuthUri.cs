using System;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// An implementation of Uri that will create an uri with an 
    /// xoauth_requestor_id query string.
    /// </summary>
    [Obsolete("this is going to be removed in the future and replaced with OAuthAuthenticator")]
    public class OAuthUri : Uri
    {
        public static string OAuthParameter = "xoauth_requestor_id";
        /// <summary>
        /// Creates a Uri with a xoauth_requestor_id query string.
        /// </summary>
        /// <param name="uriString">The base Uri</param>
        /// <param name="userName">The username for the xoauth_requestor_id</param>
        /// <param name="domain">The domain for the xoauth_requestor_id</param>
        public OAuthUri(String uriString, String userName, String domain)
            : base(uriString + "?" + OAuthParameter + "=" + userName + "%40" + domain)
        {
        }
    }
}
