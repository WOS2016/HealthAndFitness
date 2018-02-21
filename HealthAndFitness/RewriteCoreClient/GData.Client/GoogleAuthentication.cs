using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>constants for the authentication handler
    /// </summary> 
    public static class GoogleAuthentication
    {
        /// <summary>account prefix path </summary>
        public const string AccountPrefix = "/accounts";

        /// <summary>protocol</summary>
        public const string DefaultProtocol = "https";

        /// <summary>
        /// default authentication domain
        /// </summary>
        public const string DefaultDomain = "www.google.com";

        /// <summary>Google client authentication handler</summary>
        public const string UriHandler = "https://www.google.com/accounts/ClientLogin";
        /// <summary>Google client authentication email</summary>
        public const string Email = "Email";
        /// <summary>Google client authentication password</summary>
        public const string Password = "Passwd";
        /// <summary>Google client authentication source constant</summary>
        public const string Source = "source";
        /// <summary>Google client authentication default service constant</summary>
        public const string Service = "service";
        /// <summary>Google client authentication LSID</summary>
        public const string Lsid = "LSID";
        /// <summary>Google client authentication SSID</summary>
        public const string Ssid = "SSID";
        /// <summary>Google client authentication Token</summary>
        public const string AuthToken = "Auth";
        /// <summary>Google authSub authentication Token</summary>
        public const string AuthSubToken = "Token";
        /// <summary>Google client header</summary>
        public const string Header = "Authorization: GoogleLogin auth=";
        /// <summary>Google method override header</summary>
        public const string Override = "X-HTTP-Method-Override";
        /// <summary>Google webkey identifier</summary>
        public const string WebKey = "X-Google-Key: key=";
        /// <summary>Google YouTube client identifier</summary>
        public const string YouTubeClientId = "X-GData-Client:";
        /// <summary>Google YouTube developer identifier</summary>
        public const string YouTubeDevKey = "X-GData-Key: key=";
        /// <summary>Google webkey identifier</summary>
        public const string AccountType = "accountType=";
        /// <summary>default value for the account type</summary>
        public const string AccountTypeDefault = "HOSTED_OR_GOOGLE";
        /// <summary>captcha url token</summary>
        public const string CaptchaAnswer = "logincaptcha";
        /// <summary>default value for the account type</summary>
        public const string CaptchaToken = "logintoken";
    }
}
