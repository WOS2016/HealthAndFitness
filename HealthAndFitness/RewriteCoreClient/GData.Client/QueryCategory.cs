using System;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Diagnostics;


namespace RewriteCoreClient.GData.Client
{
    /// <summary>Base class to hold an Atom category plus the boolean
    /// to create the query category.
    /// </summary> 
    public class QueryCategory
    {
        /// <summary>AtomCategory holder.</summary> 
        private AtomCategory category;
        /// <summary>Boolean operator (can be OR or AND).</summary> 
        private QueryCategoryOperator categoryOperator;
        /// <summary>Boolean negator (can be true or false).</summary> 
        private bool isExcluded;

        /// <summary>Constructor, given a category.</summary>
        public QueryCategory(AtomCategory category)
        {
            this.category = category;
            this.categoryOperator = QueryCategoryOperator.AND;
        }

        /// <summary>Constructor, given a category as a string from the URI.</summary>
        public QueryCategory(string strCategory, QueryCategoryOperator op)
        {
            Tracing.TraceMsg("Depersisting category from: " + strCategory);
            this.categoryOperator = op;
            strCategory = FeedQuery.CleanPart(strCategory);

            // let's parse the string
            if (strCategory[0] == '-')
            {
                // negator
                this.isExcluded = true;
                // remove him
                strCategory = strCategory.Substring(1, strCategory.Length - 1);
            }

            // let's extract the scheme if there is one...
            int iStart = strCategory.IndexOf('{');
            int iEnd = strCategory.IndexOf('}');
            AtomUri scheme = null;
            if (iStart != -1 && iEnd != -1)
            {
                iEnd++;
                iStart++;
                scheme = new AtomUri(strCategory.Substring(iStart, iEnd - iStart - 1));
                // the rest is then
                strCategory = strCategory.Substring(iEnd, strCategory.Length - iEnd);
            }

            Tracing.TraceMsg("Category found: " + strCategory + " - scheme: " + scheme);

            this.category = new AtomCategory(strCategory, scheme);
        }

        /// <summary>Accessor method public AtomCategory Category</summary> 
        /// <returns></returns>
        public AtomCategory Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        /// <summary>Accessor method public QueryCategoryOperator Operator</summary> 
        /// <returns> </returns>
        public QueryCategoryOperator Operator
        {
            get { return this.categoryOperator; }
            set { this.categoryOperator = value; }
        }

        /// <summary>Accessor method public bool Excluded</summary> 
        /// <returns> </returns>
        public bool Excluded
        {
            get { return this.isExcluded; }
            set { this.isExcluded = value; }
        }
    }
}
