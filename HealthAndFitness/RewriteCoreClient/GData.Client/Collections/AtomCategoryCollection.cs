using System;
using System.Collections.Generic;
using System.Text;

namespace RewriteCoreClient.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>standard typed collection based on 1.1 framework for AtomCategory
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AtomCategoryCollection : AtomCollectionBase<AtomCategory>
    {
        /// <summary>standard typed accessor method </summary> 
        public override void Add(AtomCategory value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            // Remove category with the same term to avoid duplication.
            AtomCategory oldCategory = Find(value.Term, value.Scheme);
            if (oldCategory != null)
            {
                Remove(oldCategory);
            }
            base.Add(value);
        }

        /// <summary>
        /// finds the first category with this term
        /// ignoring schemes
        /// </summary>
        /// <param name="term">the category term to search for</param>
        /// <returns>AtomCategory</returns>
        public AtomCategory Find(string term)
        {
            return Find(term, null);
        }

        /// <summary>
        /// finds a category with a given term and scheme
        /// </summary>
        /// <param name="term"></param>
        /// <param name="scheme"></param>
        /// <returns>AtomCategory or NULL</returns>
        public AtomCategory Find(string term, AtomUri scheme)
        {
            foreach (AtomCategory category in List)
            {
                if (scheme == null || scheme == category.Scheme)
                {
                    if (term == category.Term)
                    {
                        return category;
                    }
                }
            }
            return null;
        }

        /// <summary>standard typed accessor method </summary> 
        public override bool Contains(AtomCategory value)
        {
            if (value == null)
            {
                return (List.Contains(value));
            }
            // If value is not of type AtomCategory, this will return false.
            if (Find(value.Term, value.Scheme) != null)
            {
                return true;
            }
            return false;

        }
    }
    /////////////////////////////////////////////////////////////////////////////
}
