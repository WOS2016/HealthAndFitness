#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;
//using Google.GData.Extensions;
using RewriteCoreClient.GData.Client.Extensions;
#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>atom:content object representation
    /// </summary> 
    [TypeConverterAttribute(typeof(AtomContentConverter)), DescriptionAttribute("Expand to see the content objectfor the entry.")]
    public class AtomContent : AtomBase
    {
        /// <summary>holds the type attribute</summary> 
        private string type;
        /// <summary>holds the src URI attribute</summary> 
        private AtomUri src;
        /// <summary>holds the content</summary> 
        private string content;

        /// <summary>default constructor. Sets the content type to text.</summary> 
        public AtomContent()
        {
            this.type = "text";
            this.AddExtension(new BatchErrors());
        }

        #region overloaded for persistence

        /// <summary>Returns the constant representing this XML element.</summary> 
        public override string XmlName
        {
            get { return AtomParserNameTable.XmlContentElement; }
        }

        /// <summary>figures out if this object should be persisted</summary> 
        /// <returns> true, if it's worth saving</returns>
        public override bool ShouldBePersisted()
        {
            if (!base.ShouldBePersisted())
            {
                return Utilities.IsPersistable(this.src) || Utilities.IsPersistable(this.type) || Utilities.IsPersistable(this.content);
            }
            return true;
        }

        /// <summary>overridden to save attributes for this(XmlWriter writer)</summary> 
        /// <param name="writer">the xmlwriter to save into</param>
        protected override void SaveXmlAttributes(XmlWriter writer)
        {
            WriteEncodedAttributeString(writer, AtomParserNameTable.XmlAttributeSrc, this.Src);
            WriteEncodedAttributeString(writer, AtomParserNameTable.XmlAttributeType, this.Type);
            // call base later, as base will save out extension elements that might create subelements
            base.SaveXmlAttributes(writer);
        }

        /// <summary>saves the inner state of the element. Note that if the 
        /// content type is xhtml, no encoding will be done by this object</summary> 
        /// <param name="writer">the xmlWriter to save into</param>
        protected override void SaveInnerXml(XmlWriter writer)
        {
            base.SaveInnerXml(writer);
            if (Utilities.IsPersistable(this.content))
            {
                if (this.type == "html" || this.type.StartsWith("text"))
                {
                    // per spec, text/html should be encoded. 
                    // but what do we do if we get it encoded? we would now double encode
                    // the string
                    // hence we decode once first, and then encode again
                    String buffer = HttpUtility.HtmlDecode(this.content);
                    WriteEncodedString(writer, buffer);

                }
                else
                {
                    // in this case we are not going to encode the inner content. 
                    // Developer has to take care of this
                    writer.WriteRaw(this.content);
                }
            }
        }

        #endregion

        /// <summary>accessor method public string Type</summary> 
        /// <returns> </returns>
        public string Type
        {
            get { return this.type; }
            set { this.Dirty = true; this.type = value; }
        }

        /// <summary>accessor method public Uri Src</summary> 
        /// <returns> </returns>
        public AtomUri Src
        {
            get { return this.src; }
            set { this.Dirty = true; this.src = value; }
        }

        /// <summary>public Uri AbsoluteUri</summary> 
        public string AbsoluteUri
        {
            get
            {
                return GetAbsoluteUri(this.Src.ToString());
            }
        }

        /// <summary>accessor method public string Content</summary> 
        /// <returns> </returns>
        public string Content
        {
            get { return this.content; }
            set { this.Dirty = true; this.content = value; }
        }

        /// <summary>
        /// gd:errors element
        /// </summary>
        /// <returns></returns>
        public BatchErrors BatchErrors
        {
            get
            {
                return this.FindExtension(BaseNameTable.gdErrors,
                    BaseNameTable.gNamespace) as BatchErrors;
            }
            set
            {
                ReplaceExtension(BaseNameTable.gdErrors,
                    BaseNameTable.gNamespace,
                    value);
            }
        }

        //private void ReplaceExtension(string gdErrors, string gNamespace, BatchErrors value)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
