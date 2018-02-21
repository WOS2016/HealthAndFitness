#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// holds the batch status information
    /// </summary>
    public class GDataBatchStatus : IExtensionElementFactory
    {
        private int code;
        private string reason;
        private string contentType;
        private List<GDataBatchError> errorList;

        /// <summary>default value for the status code</summary>
        public const int CodeDefault = -1;

        /// <summary>
        /// sets the defaults for code
        /// </summary>
        public GDataBatchStatus()
        {
            this.Code = CodeDefault;
        }

        /// <summary>returns the status code of the operation</summary> 
        /// <returns> </returns>
        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        /// <summary>accessor method public string Reason</summary> 
        /// <returns> </returns>
        public string Reason
        {
            get
            {
                return this.reason;
            }
            set
            {
                this.reason = value;
            }
        }

        /// <summary>accessor method public string ContentType</summary> 
        /// <returns> </returns>
        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

        /// <summary>the error list</summary> 
        /// <returns> </returns>
        public List<GDataBatchError> Errors
        {
            get
            {
                if (this.errorList == null)
                {
                    this.errorList = new List<GDataBatchError>();
                }
                return this.errorList;
            }
        }

        #region Persistence overloads

        /// <summary>
        /// Persistence method for the GDataBatchStatus object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer");
            }

            writer.WriteStartElement(BaseNameTable.gBatchPrefix, BaseNameTable.XmlElementBatchStatus, BaseNameTable.gBatchPrefix);

            if (this.Code != GDataBatchStatus.CodeDefault)
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchStatusCode, this.Code.ToString(CultureInfo.InvariantCulture));
            }

            if (Utilities.IsPersistable(this.ContentType))
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchContentType, this.ContentType);
            }

            if (Utilities.IsPersistable(this.Reason))
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchReason, this.Reason);
            }

            writer.WriteEndElement();
        }

        #endregion

        #region IExtensionElementFactory Members

        /// <summary>
        /// reads the current positioned reader and creates a batchstatus element
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <param name="parser">The Feedparser to be used</param>
        /// <returns>GDataBatchStatus</returns>
        public static GDataBatchStatus ParseBatchStatus(XmlReader reader, AtomFeedParser parser)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            GDataBatchStatus status = null;

            object localname = reader.LocalName;
            if (localname.Equals(parser.Nametable.BatchStatus))
            {
                status = new GDataBatchStatus();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(parser.Nametable.BatchReason))
                        {
                            status.Reason = Utilities.DecodedValue(reader.Value);
                        }
                        else if (localname.Equals(parser.Nametable.BatchContentType))
                        {
                            status.ContentType = Utilities.DecodedValue(reader.Value);
                        }
                        else if (localname.Equals(parser.Nametable.BatchStatusCode))
                        {
                            status.Code = int.Parse(Utilities.DecodedValue(reader.Value), CultureInfo.InvariantCulture);
                        }
                    }
                }

                reader.MoveToElement();

                // FIX: THIS CODE SEEMS TO MAKE AN INFINITE LOOP WITH NextChildElement()

                int lvl = -1;
                // status can have one child element, errors
                while (Utilities.NextChildElement(reader, ref lvl))
                {
                    localname = reader.LocalName;

                    if (localname.Equals(parser.Nametable.BatchErrors))
                    {
                        GDataBatchError.ParseBatchErrors(reader, parser, status);
                    }
                }
            }
            return status;
        }

        /// <summary>
        /// the xmlname of the element
        /// </summary>
        public string XmlName
        {
            get
            {
                return BaseNameTable.XmlElementBatchStatus;
            }
        }

        /// <summary>
        ///  the xmlnamespace for a batchstatus
        /// </summary>
        public string XmlNameSpace
        {
            get
            {
                return BaseNameTable.gBatchNamespace;
            }
        }

        /// <summary>
        /// the preferred xmlprefix to use
        /// </summary>
        public string XmlPrefix
        {
            get
            {
                return BaseNameTable.gBatchPrefix;
            }
        }

        /// <summary>
        /// creates a new batchstatus element
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return ParseBatchStatus(new XmlNodeReader(node), parser);
        }

        #endregion
    }

}
