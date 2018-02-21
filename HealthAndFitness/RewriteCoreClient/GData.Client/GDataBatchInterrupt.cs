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
    public class GDataBatchInterrupt : IExtensionElementFactory
    {
        private string reason;
        private int success;
        private int failures;
        private int parsed;
        private int unprocessed;

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

        /// <summary>accessor method public int Successes</summary> 
        /// <returns> </returns>
        public int Successes
        {
            get
            {
                return this.success;
            }
            set
            {
                this.success = value;
            }
        }

        /// <summary>accessor method public int Failures</summary> 
        /// <returns> </returns>
        public int Failures
        {
            get
            {
                return this.failures;
            }
            set
            {
                this.failures = value;
            }
        }

        /// <summary>accessor method public int Unprocessed</summary> 
        /// <returns> </returns>
        public int Unprocessed
        {
            get
            {
                return this.unprocessed;
            }
            set
            {
                this.unprocessed = value;
            }
        }

        /// <summary>accessor method public int Parsed</summary> 
        /// <returns> </returns>
        public int Parsed
        {
            get
            {
                return this.parsed;
            }
            set
            {
                this.parsed = value;
            }
        }

        #region Persistence overloads

        /// <summary>
        /// Persistence method for the GDataBatchInterrupt object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
        }

        #endregion

        #region IExtensionElementFactory Members

        /// <summary>
        /// parses a batchinterrupt element from a correctly positioned reader
        /// </summary>
        /// <param name="reader">XmlReader at the start of the element</param>
        /// <param name="parser">the feedparser to be used</param>
        /// <returns>GDataBatchInterrupt</returns>
        public static GDataBatchInterrupt ParseBatchInterrupt(XmlReader reader, AtomFeedParser parser)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            object localname = reader.LocalName;
            GDataBatchInterrupt interrupt = null;
            if (localname.Equals(parser.Nametable.BatchInterrupt))
            {
                interrupt = new GDataBatchInterrupt();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(parser.Nametable.BatchReason))
                        {
                            interrupt.Reason = Utilities.DecodedValue(reader.Value);
                        }
                        else if (localname.Equals(parser.Nametable.BatchSuccessCount))
                        {
                            interrupt.Successes = int.Parse(Utilities.DecodedValue(reader.Value), CultureInfo.InvariantCulture);
                        }
                        else if (localname.Equals(parser.Nametable.BatchFailureCount))
                        {
                            interrupt.Failures = int.Parse(Utilities.DecodedValue(reader.Value), CultureInfo.InvariantCulture);
                        }
                        else if (localname.Equals(parser.Nametable.BatchParsedCount))
                        {
                            interrupt.Parsed = int.Parse(Utilities.DecodedValue(reader.Value), CultureInfo.InvariantCulture);
                        }
                        else if (localname.Equals(parser.Nametable.BatchUnprocessed))
                        {
                            interrupt.Unprocessed = int.Parse(Utilities.DecodedValue(reader.Value), CultureInfo.InvariantCulture);
                        }

                    }
                }
            }
            return interrupt;
        }

        /// <summary>
        /// returns the xmlname to sue
        /// </summary>
        public string XmlName
        {
            get
            {
                return BaseNameTable.XmlElementBatchInterrupt;
            }
        }

        /// <summary>
        /// returns the xmlnamespace
        /// </summary>
        public string XmlNameSpace
        {
            get
            {
                return BaseNameTable.gBatchNamespace;
            }
        }

        /// <summary>
        /// the xmlprefix
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
            return ParseBatchInterrupt(new XmlNodeReader(node), parser);
        }

        #endregion
    }
}
