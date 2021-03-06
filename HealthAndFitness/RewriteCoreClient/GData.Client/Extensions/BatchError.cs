﻿using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;
////using Google.GData.Extensions;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions
{

    public class BatchError : SimpleContainer
    {
        /// <summary>
        /// default constructor for gd:error
        /// </summary>
        public BatchError()
            : base(BaseNameTable.gdError,
                BaseNameTable.gDataPrefix,
                BaseNameTable.gNamespace)
        {
            this.ExtensionFactories.Add(new BatchErrorDomain());
            this.ExtensionFactories.Add(new BatchErrorCode());
            this.ExtensionFactories.Add(new BatchErrorLocation());
            this.ExtensionFactories.Add(new BatchErrorInternalReason());
            this.ExtensionFactories.Add(new BatchErrorId());
        }

        /// <summary>
        /// returns the gd:domain
        /// </summary>
        public string Domain
        {
            get
            {
                return GetStringValue<BatchErrorDomain>(BaseNameTable.gdDomain,
                    BaseNameTable.gNamespace);
            }
            set
            {
                SetStringValue<BatchErrorDomain>(value.ToString(),
                    BaseNameTable.gdDomain,
                    BaseNameTable.gNamespace);
            }
        }

        /// <summary>
        /// returns the gd:code
        /// </summary>
        public string Code
        {
            get
            {
                return GetStringValue<BatchErrorCode>(BaseNameTable.gdCode,
                    BaseNameTable.gNamespace);
            }
            set
            {
                SetStringValue<BatchErrorCode>(value.ToString(),
                    BaseNameTable.gdCode,
                    BaseNameTable.gNamespace);
            }
        }

        /// <summary>
        /// returns the gd:location
        /// </summary>
        public BatchErrorLocation Location
        {
            get
            {
                return FindExtension(BaseNameTable.gdLocation,
                   BaseNameTable.gNamespace) as BatchErrorLocation;
            }
            set
            {
                ReplaceExtension(BaseNameTable.gdLocation,
                    BaseNameTable.gNamespace,
                    value);
            }
        }

        /// <summary>
        /// returns the gd:internalReason
        /// </summary>
        public string InternalReason
        {
            get
            {
                return GetStringValue<BatchErrorInternalReason>(BaseNameTable.gdInternalReason,
                    BaseNameTable.gNamespace);
            }
            set
            {
                SetStringValue<BatchErrorInternalReason>(value.ToString(),
                    BaseNameTable.gdInternalReason,
                    BaseNameTable.gNamespace);
            }
        }

        /// <summary>
        /// returns the id
        /// </summary>
        public string Id
        {
            get
            {
                return GetStringValue<BatchErrorId>(BaseNameTable.XmlElementBatchId,
                    BaseNameTable.NSAtom);
            }
            set
            {
                SetStringValue<BatchErrorId>(value.ToString(),
                    BaseNameTable.XmlElementBatchId,
                    BaseNameTable.NSAtom);
            }
        }
    }

    public class BatchErrorDomain : SimpleElement
    {
        /// <summary>
        /// default constructor for gd:domain
        /// </summary>
        public BatchErrorDomain()
            : base(BaseNameTable.gdDomain,
            BaseNameTable.gDataPrefix,
            BaseNameTable.gNamespace)
        {
        }
    }

    public class BatchErrorCode : SimpleElement
    {
        /// <summary>
        /// default constructor for gd:code
        /// </summary>
        public BatchErrorCode()
            : base(BaseNameTable.gdCode,
            BaseNameTable.gDataPrefix,
            BaseNameTable.gNamespace)
        {
        }
    }

    public class BatchErrorLocation : SimpleElement
    {
        /// <summary>
        /// default constructor for gd:location
        /// </summary>
        public BatchErrorLocation()
            : base(BaseNameTable.gdLocation,
            BaseNameTable.gDataPrefix,
            BaseNameTable.gNamespace)
        {
        }

        /// <summary>
        /// Type property accessor
        /// </summary>
        public string Type
        {
            get
            {
                return Convert.ToString(Attributes[BaseNameTable.XmlAttributeType]);
            }
            set
            {
                Attributes[BaseNameTable.XmlAttributeType] = value;
            }
        }
    }

    public class BatchErrorInternalReason : SimpleElement
    {
        /// <summary>
        /// default constructor for gd:internalReason
        /// </summary>
        public BatchErrorInternalReason()
            : base(BaseNameTable.gdInternalReason,
            BaseNameTable.gDataPrefix,
            BaseNameTable.gNamespace)
        {
        }
    }

    public class BatchErrorId : SimpleElement
    {
        /// <summary>
        /// default constructor for id
        /// </summary>
        public BatchErrorId()
            : base(BaseNameTable.XmlElementBatchId,
            "",
            BaseNameTable.NSAtom)
        {
        }
    }
}
