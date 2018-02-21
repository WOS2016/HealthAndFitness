#region Using directives

#define USE_TRACING

using System;
using System.Globalization;
using System.ComponentModel;
using System.Xml;


#endregion

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>atomIcon object representation. 
    ///     The "atom:icon" element's content is an IRI reference [RFC3987] which identifies an image which provides 
    ///     iconic visual identification for a feed.
    ///     The image SHOULD have an aspect ratio of one (horizontal) to one (vertical), and SHOULD be suitable 
    ///     for presentation at a small size.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [TypeConverterAttribute(typeof(AtomBaseLinkConverter)), DescriptionAttribute("Expand to see the link attributes for the Icon.")]
    public class AtomIcon : AtomBaseLink
    {
        #region Persistence overloads
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlName
        {
            get { return AtomParserNameTable.XmlIconElement; }
        }
        /////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////
        #endregion


    }
    /////////////////////////////////////////////////////////////////////////////
}
