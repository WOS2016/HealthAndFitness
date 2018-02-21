#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Globalization;
using System.ComponentModel;


#endregion

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>atomLogo object representation. 
    ///         The "atom:logo" element's content is an IRI reference [RFC3987] which '+
    ///         identifies an image which provides visual identification for a feed.
    ///         The image SHOULD have an aspect ratio of 2 (horizontal) to 1 (vertical).
    ///         implemented currently by reusing atomBaseLink. 
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [TypeConverterAttribute(typeof(AtomBaseLinkConverter)), DescriptionAttribute("Expand to see the link attributes for the Logo.")]
    public class AtomLogo : AtomBaseLink
    {
        #region Persistence overloads
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlName
        {
            get { return AtomParserNameTable.XmlLogoElement; }
        }
        /////////////////////////////////////////////////////////////////////////////
        #endregion


    }
    /////////////////////////////////////////////////////////////////////////////
}
