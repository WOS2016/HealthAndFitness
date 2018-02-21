#region Using directives

#define USE_TRACING
#define USE_LOGGING

using System;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Text;

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>custom exceptions</summary> 
//////////////////////////////////////////////////////////////////////
namespace RewriteCoreClient.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>standard exception class to be used when authentication 
    /// using Google Client Login fails
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class AuthenticationException : LoggedException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public AuthenticationException() { }

        /// <summary>
        /// base constructor, takes a message text
        /// </summary> 
        /// <param name="msg"></param>
        public AuthenticationException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public AuthenticationException(String msg, Exception e) : base(msg, e) { }
    }

    /// <summary>thrown when the credentials are wrong</summary> 
    [Serializable]
    public class InvalidCredentialsException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public InvalidCredentialsException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public InvalidCredentialsException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public InvalidCredentialsException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>thrown when the account was deleted
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class AccountDeletedException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public AccountDeletedException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public AccountDeletedException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public AccountDeletedException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>thrown when the account was disabled
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class AccountDisabledException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public AccountDisabledException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public AccountDisabledException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public AccountDisabledException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>the account hoder was not verified
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class NotVerifiedException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public NotVerifiedException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public NotVerifiedException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public NotVerifiedException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>The Terms were not agreed with..
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class TermsNotAgreedException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public TermsNotAgreedException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public TermsNotAgreedException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public TermsNotAgreedException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>The service is current not available
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class ServiceUnavailableException : AuthenticationException
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public ServiceUnavailableException() { }
        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public ServiceUnavailableException(String msg) : base(msg) { }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public ServiceUnavailableException(String msg, Exception e) : base(msg, e) { }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>many unsuccessfull logins might create this...
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [Serializable]
    public class CaptchaRequiredException : AuthenticationException
    {
        private string captchaUrl;
        private string captchaToken;

        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor so that FxCop does not complain</summary> 
        //////////////////////////////////////////////////////////////////////
        public CaptchaRequiredException() { }

        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a descriptive string</summary> 
        //////////////////////////////////////////////////////////////////////
        public CaptchaRequiredException(String msg, String url, String token) : base(msg)
        {
            this.captchaUrl = url;
            this.captchaToken = token;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="msg">message for exception</param>
        /// <param name="e">inner exception</param>
        public CaptchaRequiredException(String msg, Exception e) : base(msg, e) { }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for captchaUrl</summary> 
        //////////////////////////////////////////////////////////////////////
        public string Url
        {
            get { return this.captchaUrl; }
        }
        // end of accessor for captchaUrl

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for captchaToken</summary> 
        //////////////////////////////////////////////////////////////////////
        public string Token
        {
            get { return this.captchaToken; }
        }
        // end of accessor for captchaToken
    }

} //end of file //////////////////////////////////////////////////////////////////////////