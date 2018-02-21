using System;
using System.Xml;
//using Google.GData.Client;
using System.Globalization;

//namespace RewriteCoreClient.GData.Client
namespace RewriteCoreClient.GData.Client.Extensions
{
    /// <summary>
    /// Extensible enum type used in many places.
    /// compared to the base class, this one
    /// adds a default value which is the text content inside the 
    /// element node.
    /// </summary>
    public abstract class SimpleElement : ExtensionBase
    {
        private string value;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleElement(string name, string prefix, string ns)
            : base(name, prefix, ns)
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        /// <param name="value">the intial value</param>
        protected SimpleElement(string name, string prefix, string ns, string value)
            : base(name, prefix, ns)
        {
            this.value = value;
        }

        /// <summary>
        /// Accessor Method for the value as string
        /// </summary>
        public virtual string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Accessor Method for the value as integer
        /// </summary>
        public int IntegerValue
        {
            get { return Convert.ToInt32(this.Value, CultureInfo.InvariantCulture); }
            set { this.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Accessor Method for the value as unsigned integer
        /// </summary>
        [CLSCompliant(false)]
        public uint UnsignedIntegerValue
        {
            get { return Convert.ToUInt32(this.Value, CultureInfo.InvariantCulture); }
            set { this.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Accessor Method for the value as unsigned long
        /// </summary>
        [CLSCompliant(false)]
        public ulong UnsignedLongValue
        {
            get { return Convert.ToUInt64(this.Value, CultureInfo.InvariantCulture); }
            set { this.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Accessor Method for the value as float
        /// </summary>
        public double FloatValue
        {
            get { return Convert.ToDouble(this.Value, CultureInfo.InvariantCulture); }
            set { this.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Accessor Method for the value as boolean
        /// </summary>
        public virtual bool BooleanValue
        {
            get { return Utilities.XSDTrue == this.Value; }
            set { this.Value = value ? Utilities.XSDTrue : Utilities.XSDFalse; }
        }

        #region overloaded for persistence

        /// <summary>
        /// debugging helper
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + " for: " + XmlNameSpace + " - " + XmlName;
        }

        /// <summary>Parses an xml node to create an instance object.</summary> 
        /// <param name="node">the parsed xml node, can be NULL</param>
        /// <param name="parser">the xml parser to use if we need to dive deeper</param>
        /// <returns>the created SimpleElement object</returns>
        public override IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Tracing.TraceCall();

            SimpleElement e = null;

            if (node != null)
            {
                object localname = node.LocalName;
                if (!localname.Equals(this.XmlName) ||
                    !node.NamespaceURI.Equals(this.XmlNameSpace))
                {
                    return null;
                }
            }

            // memberwise close is fine here, as everything is identical besides the value
            e = this.MemberwiseClone() as SimpleElement;
            e.InitInstance(this);

            if (node != null)
            {
                e.ProcessAttributes(node);
                if (node.HasChildNodes)
                {
                    XmlNode n = node.ChildNodes[0];
                    if (n.NodeType == XmlNodeType.Text && node.ChildNodes.Count == 1)
                    {
                        e.Value = node.InnerText;
                    }
                    else
                    {
                        e.ProcessChildNodes(node, parser);
                    }
                }
            }

            return e;
        }

        /// <summary>
        /// saves the value, is called from Save
        /// </summary>
        /// <param name="writer"></param>
        public override void SaveInnerXml(XmlWriter writer)
        {
            if (this.value != null)
            {
                writer.WriteString(this.value);
            }
        }
        #endregion
    }

    /// <summary>
    /// a simple element with one attribute, called value, that exposes 
    /// the given value as the Value property
    /// </summary>
    public abstract class SimpleAttribute : SimpleElement
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleAttribute(string name, string prefix, string ns)
            : base(name, prefix, ns)
        {
            this.Attributes.Add(BaseNameTable.XmlValue, null);
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        /// <param name="value">the initial value</param>
        protected SimpleAttribute(string name, string prefix, string ns, string value)
            : base(name, prefix, ns)
        {
            this.Attributes.Add(BaseNameTable.XmlValue, value);
        }

        /// <summary>Accessor for "value" attribute.</summary> 
        /// <returns> </returns>
        public override string Value
        {
            get
            {
                return this.Attributes[BaseNameTable.XmlValue] as string;
            }
            set
            {
                this.Attributes[BaseNameTable.XmlValue] = value;
            }
        }
    }

    /// <summary>
    /// a simple element with two attributes, called value and name, that exposes 
    /// the given value as the Value property
    /// </summary>
    public abstract class SimpleNameValueAttribute : SimpleAttribute
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">the xml name</param>
        /// <param name="prefix">the xml prefix</param>
        /// <param name="ns">the xml namespace</param>
        protected SimpleNameValueAttribute(string localName, string prefix, string ns)
            : base(localName, prefix, ns)
        {
            this.Attributes.Add(BaseNameTable.XmlName, null);
        }

        /// <summary>Accessor for "name" attribute.</summary> 
        /// <returns> </returns>
        public string Name
        {
            get
            {
                return this.Attributes[BaseNameTable.XmlName] as string;
            }
            set
            {
                this.Attributes[BaseNameTable.XmlName] = value;
            }
        }
    }
}
