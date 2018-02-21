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
    /// <summary>The GDataEntryBatch object holds batch related information
    /// for an AtomEntry
    /// </summary> 
    public class GDataBatchEntryData : IExtensionElementFactory
    {
        private GDataBatchOperationType operationType;
        private string id;
        private GDataBatchStatus status;
        private GDataBatchInterrupt interrupt;

        /// <summary>
        /// constructor, sets the default for the operation type
        /// </summary>
        public GDataBatchEntryData()
        {
            this.operationType = GDataBatchOperationType.Default;
        }

        /// <summary>
        /// Constructor for the batch data
        /// </summary>
        /// <param name="type">The batch operation to be performed</param>
        public GDataBatchEntryData(GDataBatchOperationType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Constructor for batch data
        /// </summary>
        /// <param name="id">The batch ID of this entry</param>
        /// <param name="type">The batch operation to be performed</param>
        public GDataBatchEntryData(string id, GDataBatchOperationType type)
            : this(type)
        {
            this.Id = id;
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

        /// <summary>accessor method public string Id</summary> 
        /// <returns> </returns>
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>accessor for the GDataBatchInterrrupt element</summary> 
        /// <returns> </returns>
        public GDataBatchInterrupt Interrupt
        {
            get
            {
                return this.interrupt;
            }
            set
            {
                this.interrupt = value;
            }
        }

        /// <summary>accessor method public GDataBatchStatus Status</summary> 
        /// <returns> </returns>
        public GDataBatchStatus Status
        {
            get
            {
                if (this.status == null)
                {
                    this.status = new GDataBatchStatus();
                }
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataEntryBatch object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer");
            }

            if (this.Id != null)
            {
                writer.WriteElementString(BaseNameTable.XmlElementBatchId, BaseNameTable.gBatchNamespace, this.id);
            }

            if (this.Type != GDataBatchOperationType.Default)
            {
                writer.WriteStartElement(XmlPrefix, XmlName, XmlNameSpace);
                writer.WriteAttributeString(BaseNameTable.XmlAttributeType, this.operationType.ToString());
                writer.WriteEndElement();
            }

            if (this.status != null)
            {
                this.status.Save(writer);
            }
        }
        #endregion

        #region IExtensionElementFactory Members

        /// <summary>
        /// xml local name to use
        /// </summary>
        public string XmlName
        {
            get
            {
                //TODO This doesn't seem correct.
                return BaseNameTable.XmlElementBatchOperation;
            }
        }

        /// <summary>
        /// xml namespace to use
        /// </summary>
        public string XmlNameSpace
        {
            get
            {
                return BaseNameTable.gBatchNamespace;
            }
        }

        /// <summary>
        /// xml prefix to use
        /// </summary>
        public string XmlPrefix
        {
            get
            {
                return BaseNameTable.gBatchPrefix;
            }
        }

        /// <summary>
        /// creates a new GDataBatchEntryData
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            //we really don't know how to create an instance of ourself.
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
