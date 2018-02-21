#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;
//using Google.GData.Extensions;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>TypeConverter, so that AtomContentConverter shows up in the property pages
    /// </summary> 
    [ComVisible(false)]
    public class AtomContentConverter : ExpandableObjectConverter
    {
        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomContent))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            AtomContent content = value as AtomContent;
            if (destinationType == typeof(System.String) && content != null)
            {
                return "Content-type: " + content.Type;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
