#region Using directives

#define USE_TRACING

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

#endregion

namespace RewriteCoreClient.GData.Client
{ 
    /// <summary>
    /// base GDataRequest implementation
    /// </summary>
public class GAuthSubRequest : GDataGAuthRequest
{
    /// <summary>holds the factory instance</summary> 
    private GAuthSubRequestFactory factory;

    /// <summary>
    /// default constructor
    /// </summary>
    internal GAuthSubRequest(GDataRequestType type, Uri uriTarget, GAuthSubRequestFactory factory) :
        base(type, uriTarget, factory)
    {
        this.factory = factory;
    }

    /// <summary>
    /// sets up the correct credentials for this call, pending 
    /// security scheme
    /// </summary>
    protected override void EnsureCredentials()
    {
        HttpWebRequest http = this.Request as HttpWebRequest;

        string header = AuthSubUtil.formAuthorizationHeader(this.factory.Token,
            this.factory.PrivateKey,
            http.RequestUri,
            http.Method);
        this.Request.Headers.Add(header);
    }
}
}
