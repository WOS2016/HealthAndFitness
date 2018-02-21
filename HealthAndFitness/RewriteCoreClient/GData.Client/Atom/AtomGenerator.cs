#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Represents the Generator element /feed/generator in Atom. In RSS, only the name property is used.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [TypeConverterAttribute(typeof(AtomGeneratorConverter)), DescriptionAttribute("Expand to see the feed generator object.")]
    public class AtomGenerator : AtomBase
    {
        /// <summary>text part of the Generator element</summary> 
        private string text;
        /// <summary>Uri attribute of the Generator element</summary> 
        private AtomUri uri;
        /// <summary>version attribute of the Generator element</summary> 
        private string version;


        //////////////////////////////////////////////////////////////////////
        /// <summary>standard constructor, not used right now
        /// atomGenerator = element atom:generator {
        ///    atomCommonAttributes,
        ///    attribute url { atomUri }?,
        ///    attribute version { text }?,
        ///    text
        /// }
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        public AtomGenerator()
        {
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>public AtomGenerator(string text)</summary> 
        /// <param name="text">the human readable representation of the generator</param>
        //////////////////////////////////////////////////////////////////////
        public AtomGenerator(string text)
        {
            this.Text = text;
        }
        /////////////////////////////////////////////////////////////////////////////


        #region Persistence overloads
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlName
        {
            get { return AtomParserNameTable.XmlGeneratorElement; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>overridden to save attributes for this(XmlWriter writer)</summary> 
        /// <param name="writer">the xmlwriter to save into </param>
        //////////////////////////////////////////////////////////////////////
        protected override void SaveXmlAttributes(XmlWriter writer)
        {
            WriteEncodedAttributeString(writer, AtomParserNameTable.XmlUriElement, this.Uri);
            WriteEncodedAttributeString(writer, AtomParserNameTable.XmlAttributeVersion, this.Version);
            // call base later as base takes care of writing out extension elements that might close the attribute list
            base.SaveXmlAttributes(writer);
        }
        /////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////
        /// <summary>saves the inner state of the element</summary> 
        /// <param name="writer">the xmlWriter to save into </param>
        //////////////////////////////////////////////////////////////////////
        protected override void SaveInnerXml(XmlWriter writer)
        {
            base.SaveInnerXml(writer);
            WriteEncodedString(writer, this.text);
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>figures out if this object should be persisted</summary> 
        /// <returns> true, if it's worth saving</returns>
        //////////////////////////////////////////////////////////////////////
        public override bool ShouldBePersisted()
        {
            if (!base.ShouldBePersisted())
            {
                if (this.uri != null && Utilities.IsPersistable(this.uri.ToString()))
                {
                    return true;
                }
                if (Utilities.IsPersistable(this.version) || Utilities.IsPersistable(this.text))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////

        #endregion


        #region property accessors

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Text</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Text
        {
            get { return this.text; }
            set { this.Dirty = true; this.text = value; }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public Uri Uri</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri Uri
        {
            get { return this.uri; }
            set { this.Dirty = true; this.uri = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Version</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Version
        {
            get { return this.version; }
            set { this.Dirty = true; this.version = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        #endregion end of property accessors

    }
    /////////////////////////////////////////////////////////////////////////////
}
