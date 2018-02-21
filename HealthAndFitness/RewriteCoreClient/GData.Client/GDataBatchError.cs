#region Using directives

#define USE_TRACING

using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// represents the Error element in the GDataBatch response
    /// </summary>
    public class GDataBatchError : IExtensionElementFactory
    {
        private string errorType;
        private string errorReason;
        private string field;

        /// <summary>accessor method Type</summary> 
        /// <returns> </returns>
        public string Type
        {
            get
            {
                return this.errorType;
            }
            set
            {
                this.errorType = value;
            }
        }

        /// <summary>accessor method public string Field</summary> 
        /// <returns> </returns>
        public string Field
        {
            get
            {
                return this.field;
            }
            set
            {
                this.field = value;
            }
        }

        /// <summary>accessor method public string Reason</summary> 
        /// <returns> </returns>
        public string Reason
        {
            get
            {
                return this.errorReason;
            }
            set
            {
                this.errorReason = value;
            }
        }

        #region Persistence overloads

        /// <summary>
        /// Persistence method for the GDataBatchError object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
        }

        #endregion

        #region IExtensionElementFactory Members

        /// <summary>
        /// parses a list of errors
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <param name="status">the batch status element to add the errors tohe</param>
        /// <param name="parser">the feedparser to be used</param>
        public static void ParseBatchErrors(XmlReader reader, AtomFeedParser parser, GDataBatchStatus status)
        {
            if (reader == null)
            {
                throw new System.ArgumentNullException("reader");
            }

            object localname = reader.LocalName;
            if (localname.Equals(parser.Nametable.BatchErrors))
            {
                int lvl = -1;
                while (Utilities.NextChildElement(reader, ref lvl))
                {
                    localname = reader.LocalName;
                    if (localname.Equals(parser.Nametable.BatchError))
                    {
                        status.Errors.Add(ParseBatchError(reader, parser));
                    }
                }
            }
            return;
        }

        /// <summary>
        /// parses a single error element
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <param name="parser">the feedparser to be used</param>
        /// <returns>GDataBatchError</returns>
        public static GDataBatchError ParseBatchError(XmlReader reader, AtomFeedParser parser)
        {
            if (reader == null)
            {
                throw new System.ArgumentNullException("reader");
            }

            object localname = reader.LocalName;
            GDataBatchError error = null;
            if (localname.Equals(parser.Nametable.BatchError))
            {
                error = new GDataBatchError();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(parser.Nametable.BatchReason))
                        {
                            error.Reason = Utilities.DecodedValue(reader.Value);
                        }
                        else if (localname.Equals(parser.Nametable.Type))
                        {
                            error.Type = Utilities.DecodedValue(reader.Value);
                        }
                        else if (localname.Equals(parser.Nametable.BatchField))
                        {
                            error.Field = Utilities.DecodedValue(reader.Value);
                        }
                    }
                }
            }
            return error;
        }

        /// <summary>
        /// the name to use
        /// </summary>
        public string XmlName
        {
            get
            {
                return BaseNameTable.XmlElementBatchError;
            }
        }

        /// <summary>
        /// the namespace to use
        /// </summary>
        public string XmlNameSpace
        {
            get
            {
                return BaseNameTable.gBatchNamespace;
            }
        }

        /// <summary>
        /// the preferred prefix
        /// </summary>
        public string XmlPrefix
        {
            get
            {
                return BaseNameTable.gBatchPrefix;
            }
        }

        /// <summary>
        /// creates a GDataBatchError element 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            return ParseBatchError(new XmlNodeReader(node), parser);
        }

        #endregion
    }
}
