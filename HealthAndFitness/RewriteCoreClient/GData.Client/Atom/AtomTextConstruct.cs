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
    /// <summary>AtomTextConstruct object representation
    /// A Text construct contains human-readable text, usually in small quantities. 
    /// The content of Text constructs is Language-Sensitive.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    [TypeConverterAttribute(typeof(AtomTextConstructConverter)), DescriptionAttribute("Expand to see details for this object.")]
    public class AtomTextConstruct : AtomBase
    {
        /// <summary>holds the type of the text</summary> 
        private AtomTextConstructType type;
        /// <summary>holds the text as string</summary> 
        private string text;
        /// <summary>holds the element type</summary> 
        private AtomTextConstructElementType elementType;

        /// <summary>the public constructor only exists for the pleasure of property pages</summary> 
        public AtomTextConstruct()
        {

        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor indicating the elementtype</summary> 
        /// <param name="elementType">holds the xml elementype</param>
        //////////////////////////////////////////////////////////////////////
        public AtomTextConstruct(AtomTextConstructElementType elementType)
        {
            this.elementType = elementType;
            this.type = AtomTextConstructType.text; // set the default to text
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor indicating the elementtype</summary> 
        /// <param name="elementType">holds the xml elementype</param>
        /// <param name="text">holds the text string</param>
        //////////////////////////////////////////////////////////////////////
        public AtomTextConstruct(AtomTextConstructElementType elementType, string text) : this(elementType)
        {
            this.text = text;
        }






        /////////////////////////////////////////////////////////////////////////////
        // 
        // 
        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public AtomTextConstructType Type</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomTextConstructType Type
        {
            get { return this.type; }
            set { this.Dirty = true; this.type = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

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


        #region Persistence overloads
        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the constant representing this XML element.</summary> 
        //////////////////////////////////////////////////////////////////////
        public override string XmlName
        {
            get
            {
                switch (this.elementType)
                {
                    case AtomTextConstructElementType.Rights:
                        return AtomParserNameTable.XmlRightsElement;
                    case AtomTextConstructElementType.Subtitle:
                        return AtomParserNameTable.XmlSubtitleElement;
                    case AtomTextConstructElementType.Title:
                        return AtomParserNameTable.XmlTitleElement;
                    case AtomTextConstructElementType.Summary:
                        return AtomParserNameTable.XmlSummaryElement;

                }

                return null;
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>overridden to save attributes for this(XmlWriter writer)</summary> 
        /// <param name="writer">the xmlwriter to save into </param>
        //////////////////////////////////////////////////////////////////////
        protected override void SaveXmlAttributes(XmlWriter writer)
        {
            WriteEncodedAttributeString(writer, AtomParserNameTable.XmlAttributeType, this.Type.ToString());
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
            if (this.Type == AtomTextConstructType.xhtml)
            {
                if (Utilities.IsPersistable(this.text))
                {
                    writer.WriteRaw(this.text);
                }
            }
            else
            {
                WriteEncodedString(writer, this.text);
            }
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
                return Utilities.IsPersistable(this.text);
            }
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////


        #endregion
    }
    /////////////////////////////////////////////////////////////////////////////
}
