using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //TODO determine if this is the correct approach.
    /// <summary>
    /// Class used as a null version aware seed for the collections
    /// </summary>
    public class NullVersionAware : IVersionAware
    {
        private static object synclock = new object();
        private static IVersionAware _instance;
        /// <summary>
        /// IVersionAware instance property
        /// </summary>
        public static IVersionAware Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (synclock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullVersionAware();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// returns the major version of the protocol used
        /// </summary>
        public int ProtocolMajor
        {
            get
            {
                return 0;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// returns the minor version of the protocol used
        /// </summary>
        public int ProtocolMinor
        {
            get
            {
                return 0;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

    }
}
