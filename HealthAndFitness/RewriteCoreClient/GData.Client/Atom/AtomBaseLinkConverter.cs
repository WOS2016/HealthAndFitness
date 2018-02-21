#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>TypeConverter, so that AtomBaseLink shows up in the property pages
    /// </summary> 
    [ComVisible(false)]
    public class AtomBaseLinkConverter : ExpandableObjectConverter
    {

        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomBaseLink)
                || destinationType == typeof(AtomId)
                || destinationType == typeof(AtomIcon)
                || destinationType == typeof(AtomLogo))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>standard ConvertTo typeconverter code</summary> 
        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            AtomBaseLink link = value as AtomBaseLink;

            if (destinationType == typeof(System.String) && link != null)
            {
                return "Uri: " + link.Uri;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
