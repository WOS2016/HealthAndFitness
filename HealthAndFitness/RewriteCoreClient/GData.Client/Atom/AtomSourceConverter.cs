#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace RewriteCoreClient.GData.Client
{
    /// <summary>TypeConverter, so that AtomHead shows up in the property pages
    /// </summary> 
    [ComVisible(false)]
    public class AtomSourceConverter : ExpandableObjectConverter
    {
        ///<summary>Standard type converter method</summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(AtomSource) || destinationType == typeof(AtomFeed))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        ///<summary>Standard type converter method</summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            AtomSource atomSource = value as AtomSource;

            if (destinationType == typeof(System.String) && atomSource != null)
            {
                return "Feed: " + atomSource.Title;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
