using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>standard typed collection based on 1.1 framework for AtomLinks
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AtomLinkCollection : AtomCollectionBase<AtomLink>
    {

        //////////////////////////////////////////////////////////////////////
        /// <summary>public AtomLink FindService(string service,string type)
        ///   Retrieves the first link with the supplied 'rel' and/or 'type' value.
        ///   If either parameter is null, the corresponding match isn't needed.
        /// </summary> 
        /// <param name="service">the service entry to find</param>
        /// <param name="type">the link type to find</param>
        /// <returns>the found link or NULL </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomLink FindService(string service, string type)
        {
            foreach (AtomLink link in List)
            {
                string linkRel = link.Rel;
                string linkType = link.Type;

                if ((service == null || (linkRel != null && linkRel == service)) &&
                    (type == null || (linkType != null && linkType.StartsWith(type))))
                {

                    return link;
                }
            }
            return null;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>public AtomLink FindService(string service,string type)
        ///   Retrieves the first link with the supplied 'rel' and/or 'type' value.
        ///   If either parameter is null, the corresponding match isn't needed.
        /// </summary> 
        /// <param name="service">the service entry to find</param>
        /// <param name="type">the link type to find</param>
        /// <returns>the found link or NULL </returns>
        //////////////////////////////////////////////////////////////////////
        public List<AtomLink> FindServiceList(string service, string type)
        {
            List<AtomLink> foundLinks = new List<AtomLink>();

            foreach (AtomLink link in List)
            {
                string linkRel = link.Rel;
                string linkType = link.Type;

                if ((service == null || (linkRel != null && linkRel == service)) &&
                    (type == null || (linkType != null && linkType == type)))
                {
                    foundLinks.Add(link);
                }
            }
            return foundLinks;
        }
        /////////////////////////////////////////////////////////////////////////////
    }
    /////////////////////////////////////////////////////////////////////////////
}
