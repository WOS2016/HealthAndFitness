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
    /// <summary>
    /// the class holds username and password to replace networkcredentials
    /// </summary>
    public class GDataCredentials
    {
        private string passWord;
        private string userName;
        private string clientToken;
        private string captchaAnswer;
        private string captchaToken;
        private string accountType = GoogleAuthentication.AccountTypeDefault;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="username">the username to use</param>
        /// <param name="password">the password to use</param>
        public GDataCredentials(string username, string password)
        {
            this.userName = username;
            this.passWord = password;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="clientToken">the client login token to use</param>
        public GDataCredentials(string clientToken)
        {
            this.clientToken = clientToken;
        }

        /// <summary>the username used for authentication</summary> 
        /// <returns> </returns>
        public string Username
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        /// <summary>the type of Account used</summary> 
        /// <returns> </returns>
        public string AccountType
        {
            get { return this.accountType; }
            set { this.accountType = value; }
        }

        /// <summary>in case you need to handle catpcha responses for this account</summary> 
        /// <returns> </returns>
        public string CaptchaToken
        {
            get { return this.captchaToken; }
            set { this.captchaToken = value; }
        }

        /// <summary>in case you need to handle catpcha responses for this account</summary> 
        /// <returns> </returns>
        public string CaptchaAnswer
        {
            get { return this.captchaAnswer; }
            set { this.captchaAnswer = value; }
        }

        /// <summary>accessor method Password</summary> 
        /// <returns> </returns>
        public string Password
        {
            set { this.passWord = value; }
        }

        internal string getPassword()
        {
            return this.passWord;
        }

        /// <summary>
        /// returns the stored clienttoken
        /// </summary>
        /// <returns></returns>
        public string ClientToken
        {
            get
            {
                return this.clientToken;
            }
            set
            {
                this.clientToken = value;
            }
        }

        /// <summary>
        /// returns a windows conforming NetworkCredential 
        /// </summary>
        public ICredentials NetworkCredential
        {
            get
            {
                return new NetworkCredential(this.userName, this.passWord);
            }
        }
    }
}
