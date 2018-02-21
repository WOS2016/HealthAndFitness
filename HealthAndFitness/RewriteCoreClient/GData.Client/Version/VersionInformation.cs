
#define USE_TRACING
#region Using directives
using System;
using System.Globalization;

#endregion
namespace RewriteCoreClient.GData.Client
{
    internal class VersionInformation : IVersionAware
    {
        private int majorVersion = VersionDefaults.Major;
        private int minorVersion = VersionDefaults.Minor;

        /// <summary>
        /// construct a versioninformation object based
        /// on a versionaware object
        /// </summary>
        /// <param name="v">the versioned object to copy the data from</param>
        /// <returns></returns>
        public VersionInformation(IVersionAware v)
        {
            this.majorVersion = v.ProtocolMajor;
            this.minorVersion = v.ProtocolMinor;
        }

        /// <summary>
        /// construct a versioninformation object based 
        /// on the header string of the http request. The string
        /// has the form {major}.{minor}
        /// </summary>
        /// <param name="headerValue">if null creates default version</param>
        /// <returns></returns>
        public VersionInformation(string headerValue)
        {
            if (headerValue != null)
            {
                string[] arr = headerValue.Split('.');
                if (arr.Length == 2)
                {
                    this.majorVersion = int.Parse(arr[0], CultureInfo.InvariantCulture);
                    this.minorVersion = int.Parse(arr[1], CultureInfo.InvariantCulture);
                }
            }
        }

        public VersionInformation()
        {
        }

        /// <summary>
        /// returns the major protocol version number this element 
        /// is working against. 
        /// </summary>
        /// <returns></returns>
        public int ProtocolMajor
        {
            get
            {
                return this.majorVersion;
            }
            set
            {
                this.majorVersion = value;
            }
        }

        /// <summary>
        /// returns the minor protocol version number this element 
        /// is working against. 
        /// </summary>
        /// <returns></returns>
        public int ProtocolMinor
        {
            get
            {
                return this.minorVersion;
            }
            set
            {
                this.minorVersion = value;
            }
        }


        /// <summary>
        /// takes an object and set's the version number to the 
        /// same as this instance
        /// </summary>
        /// <param name="v"></param>
        public void ImprintVersion(IVersionAware v)
        {
            v.ProtocolMajor = this.majorVersion;
            v.ProtocolMinor = this.minorVersion;
        }

        /// <summary>
        /// takes an object and set's the version number to the 
        /// same as this instance
        /// </summary>
        /// <param name="arr">The array of objects the version should be applied to</param>
        public void ImprintVersion(ExtensionList arr)
        {
            if (arr == null)
                return;
            foreach (Object o in arr)
            {
                IVersionAware v = o as IVersionAware;
                if (v != null)
                {
                    ImprintVersion(v);
                }
            }
        }

    }
}
