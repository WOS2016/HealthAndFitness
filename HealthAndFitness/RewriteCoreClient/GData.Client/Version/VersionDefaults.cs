using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// the default versions that are used. Currently, the default is still
    /// version 1 for most services implemented in this sdk.
    /// </summary>
    public static class VersionDefaults
    {
        /// <summary>
        /// version One is 1
        /// </summary>
        public const int VersionOne = 1;
        /// <summary>
        /// the default major is VersionOne
        /// </summary>
        public const int Major = VersionOne;
        /// <summary>
        /// the default Minor is 0
        /// </summary>
        public const int Minor = 0;
        /// <summary>
        /// and versionTwo is a 2
        /// </summary>
        public const int VersionTwo = 2;
        /// <summary>
        /// and versionThree is a 3
        /// </summary>
        public const int VersionThree = 3;

    }

}
