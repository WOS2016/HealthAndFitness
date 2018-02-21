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
    /// <summary>The GDataFeedBatch object holds batch related information
    /// for the AtomFeed
    /// </summary> 
    public class GDataBatchFeedData : IExtensionElementFactory
    {
        private GDataBatchOperationType operationType;
        /// <summary>
        /// constructor, set's the default for the operation type
        /// </summary>
        public GDataBatchFeedData()
        {
            this.operationType = GDataBatchOperationType.Default;
        }

        /// <summary>accessor method public GDataBatchOperationType Type</summary> 
        /// <returns> </returns>
        public GDataBatchOperationType Type
        {
            get
            {
                return this.operationType;
            }
            set
            {
                this.operationType = value;
            }
        }

        #region Persistence overloads

        /// <summary>
        /// Persistence method for the GDataBatch object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer");
            }

            if (this.Type != GDataBatchOperationType.Default)
            {
                writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
                writer.WriteAttributeString(BaseNameTable.XmlAttributeType, this.operationType.ToString());
                writer.WriteEndElement();
            }
        }

        #endregion

        #region IExtensionElementFactory Members

        /// <summary>
        /// the xmlname to use
        /// </summary>
        public string XmlName
        {
            get
            {
                return BaseNameTable.XmlElementBatchOperation;
            }
        }

        /// <summary>
        /// the xml namespace to use
        /// </summary>
        public string XmlNameSpace
        {
            get
            {
                return BaseNameTable.gBatchNamespace;
            }
        }

        /// <summary>
        /// the xmlprefix to use
        /// </summary>
        public string XmlPrefix
        {
            get
            {
                return BaseNameTable.gBatchPrefix;
            }
        }

        /// <summary>
        /// factory method to create an instance of a batchinterrupt during parsing
        /// </summary>
        /// <param name="node">the xmlnode that is going to be parsed</param>
        /// <param name="parser">the feedparser that is used right now</param>
        /// <returns></returns>
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
