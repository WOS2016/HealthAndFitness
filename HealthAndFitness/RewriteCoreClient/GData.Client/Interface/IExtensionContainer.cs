using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>interface for commone extension container functionallity
    /// used for AtomBase and SimpleContainer
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IExtensionContainer
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>the list of extensions for this container
        /// the elements in that list MUST implement IExtensionElementFactory 
        /// and IExtensionElement</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        ExtensionList ExtensionElements
        {
            get;
        }

        /// <summary>
        /// Finds a specific ExtensionElement based on its local name
        /// and its namespace. If namespace is NULL, the first one where
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        IExtensionElementFactory FindExtension(string localName, string ns);

        /// <summary>
        /// all extension elements that match a namespace/localname
        /// given will be removed and the new one will be inserted
        /// </summary> 
        /// <param name="localName">the local name to find</param>
        /// <param name="ns">the namespace to match, if null, ns is ignored</param>
        /// <param name="obj">the new element to put in</param>
        void ReplaceExtension(string localName, string ns, IExtensionElementFactory obj);

        /// <summary>
        /// Finds all ExtensionElement based on its local name
        /// and its namespace. If namespace is NULL, allwhere
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// Primary use of this is to find XML nodes
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>none</returns>
        ExtensionList FindExtensions(string localName, string ns);

        /// <summary>
        /// Deletes all Extensions from the Extension list that match
        /// a localName and a Namespace. 
        /// </summary>
        /// <param name="localName">the local name to find</param>
        /// <param name="ns">the namespace to match, if null, ns is ignored</param>
        /// <returns>int - the number of deleted extensions</returns>
        int DeleteExtensions(string localName, string ns);

        //////////////////////////////////////////////////////////////////////
        /// <summary>the list of extensions for this container
        /// the elements in that list MUST implement IExtensionElementFactory 
        /// and IExtensionElement</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        ExtensionList ExtensionFactories
        {
            get;
        }
    }
}
