using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions
{
    public class BatchErrors : SimpleContainer
    {
        /// <summary>
        /// Error collection
        /// </summary>
        private ExtensionCollection<BatchError> errors;

        /// <summary>
        /// default constructor for gd:errors
        /// </summary>
        public BatchErrors()
            : base(BaseNameTable.gdErrors,
                BaseNameTable.gDataPrefix,
                BaseNameTable.gNamespace)
        {
            this.ExtensionFactories.Add(new BatchError());
        }

        /// <summary>
        /// Error collection.
        /// </summary>
        public ExtensionCollection<BatchError> Errors
        {
            get
            {
                if (this.errors == null)
                {
                    this.errors = new ExtensionCollection<BatchError>(this);
                }
                return this.errors;
            }
        }
    }
}
