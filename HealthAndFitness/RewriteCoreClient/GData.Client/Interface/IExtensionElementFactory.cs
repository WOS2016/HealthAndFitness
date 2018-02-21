using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;


namespace RewriteCoreClient.GData.Client
{
    /// <summary>
    /// Wrapper interface used to replace the ExtensionList.
    /// </summary>
    public interface IExtensionElementFactory
    {
        /// <summary>
        /// returns the XML local name that is used
        /// </summary>
        string XmlName
        {
            get;
        }
        /// <summary>
        /// returns the XML namespace that is processed
        /// </summary>
        string XmlNameSpace
        {
            get;
        }
        /// <summary>
        /// returns the xml prefix used 
        /// </summary>
        string XmlPrefix
        {
            get;
        }
        /// <summary>
        /// instantiates the correct extension element
        /// </summary>
        /// <param name="node">the xmlnode to parse</param>
        /// <param name="parser">the atomfeedparser to use if deep parsing of subelements is required</param>
        /// <returns></returns>
        IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser);

        /// <summary>the only relevant method here</summary> 
        void Save(XmlWriter writer);
    }
}
